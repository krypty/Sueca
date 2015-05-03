using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;

namespace SuecaContracts
{
    class GameInfoServer
    {
        List<Card> listCard;
        CardColor asset;
        Dictionary<string, Player> dictPlayers;
        //0-3
        private int numberPlayerTurn;
        //1-4
        private int numberPlayersHavePlayed;

        private LinkedList<Card> listCardsTurn;
        //Dictionnary to known who's player have this card on a turn
        private Dictionary<Card, string> dictCardPlayerForTurn;

        public Dictionary<string, GameInfo> DictGameInfoPlayer { get; set; }

        public GameInfoServer()
        {
            listCard = new List<Card>();
            dictPlayers = new Dictionary<string, Player>();
            DictGameInfoPlayer = new Dictionary<string, GameInfo>();

            List<CardColor> listColor = new List<CardColor>();
            
            //List not static to permit the mix
            foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
            {
                foreach(CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    if(color != CardColor.None & value != CardValue.None)
                        listCard.Add(new Card(color,value));
                }
                if(color != CardColor.None)
                    listColor.Add(color);
            }
            mix(listCard);

            //Choose the 
            Random rand = new Random();
            asset = listColor[rand.Next(4)];
        }


        public GameInfoServer(List<Player> listPlayers) : this()
        {
            foreach(Player p in listPlayers)
            {
                dictPlayers.Add(p.Token,p);
            }

            //Define the first player to play and the number of players who have played
            numberPlayerTurn = 0; //Player to the right of the first player
            numberPlayersHavePlayed = 0;
        }

        public void distributeCardsForEachPlayer()
        {
            foreach(Player p in dictPlayers.Values)
            {
                //Distribute ten cards to a player (start game)
                for (int i = 0; i < 10; i++)
                {
                    p.ListCardsHolding.Add(listCard[i]);
                }

                listCard.RemoveRange(0, 10);
            }

            //Initialize the list to contain all cards play in a turn 
            listCardsTurn = new LinkedList<Card>();
        }

        public void PlayCard(string playerToken, CardColor color, CardValue value)
        {
            Player player;
            if(dictPlayers.TryGetValue(playerToken, out player))
            {
                //If it's the turn of this player
                if(player.NumberTurn == numberPlayerTurn)
                {
                    //Card played
                    Card card = new Card(color, value);
                    if(listCardsTurn.Count > 0)
                    {
                        //If it's the right color to play
                        Card firstCard = (listCardsTurn.First).Value;
                        if(color == firstCard.Color)
                        {
                            PlayCardForPlayer(player, card);
                        }
                        //Else if the player has no card of this color, he has the right to put a card of another color
                        else if(player.ListCardsHolding.Count<Card>(c => c.Color == firstCard.Color) <= 0)
                        {
                            PlayCardForPlayer(player, card);
                        }
                        else
                        {
                            Console.WriteLine("[server] the false user attempt to play");
                        }
                    }
                    else
                    {
                        //First card on the turn
                        PlayCardForPlayer(player, card);
                    }
                }
                else
                {
                    Console.WriteLine("[server] the false user attempt to play");
                }
                
            }
            else
            {
                Console.WriteLine("[server] an unknown user attempt to play");
            }
        }

        private void PlayCardForPlayer(Player player, Card card)
        {
            if(player.ListCardsHolding.Remove(card))
            {
                //Dictionary to know which card a player has played
                dictCardPlayerForTurn.Add(card,player.Token);
                listCardsTurn.AddLast(card);

                numberPlayersHavePlayed++;
                if(numberPlayersHavePlayed >= 4)
                {
                    CalculateTurn();
                    numberPlayersHavePlayed = 0;
                }
            }
            else
            {
                Console.WriteLine("[server] an error has occured when player "+player.Token+" has played card "+card.Color+" : "+card.Value+" !");
            }
        }

        
        private void CalculateTurn()
        {
            Card firstCard = (listCardsTurn.First).Value;
            Card bestCard = firstCard;

            foreach(Card card in listCardsTurn)
            {
                //If the card has not the same color and is not an asset, the card has no chance of win
                if(card.Color == firstCard.Color || card.Color == asset)
                {
                    if(card.Color == asset)
                    {
                        if(bestCard.Color == asset)
                        {
                            if(card.RealValue > bestCard.RealValue)
                            {
                                bestCard = card;
                            }
                        }
                        else
                        {
                            bestCard = card;
                        }
                    }
                    else
                    {
                        if(card.RealValue > bestCard.RealValue)
                        {
                            bestCard = card;
                        }
                    }
                }
            }

            string winnerToken;
            if(dictCardPlayerForTurn.TryGetValue(bestCard,out winnerToken))
            {
                Player winner;
                if(dictPlayers.TryGetValue(winnerToken, out winner))
                {
                    Console.WriteLine("[server] the best card of the turn is : "+bestCard.Color+" : "+bestCard.Value+" and the player who win is : " + winnerToken);
                    winner.ListCardsWin.AddRange(listCardsTurn);
                    numberPlayerTurn = winner.NumberTurn;
                }
                else
                {
                    Console.WriteLine("[server] an error has occured at the end of the calculation of winner for the turn");
                }

                listCardsTurn.Clear();
                dictCardPlayerForTurn.Clear();
            }
            else
            {
                Console.WriteLine("[server] an error has occured at the end of the calculation of winner for the turn");
            }

            
        }

        public void CreateGameInfoClient()
        {
            DictGameInfoPlayer.Clear();

            foreach(Player p in dictPlayers.Values)
            {
                GameInfo game = new GameInfo(p, dictPlayers.Values.ToList(), numberPlayerTurn);

                //If at least one card is put on the table, get the first to know the color for others players
                if (listCardsTurn.Count > 0)
                    game.FirstCard = listCardsTurn.First.Value;

                game.ListCardsPlayed = listCardsTurn;

                DictGameInfoPlayer.Add(p.Token, game);
            }
        }

        private void EndOfGame()
        {

        }

        /// <summary>
        /// Mix a list of cards in a real randomness
        /// </summary>
        /// <typeparam name="List<Card>"></typeparam>
        /// <param name="list">List to randomize</param>
        private void mix(List<Card> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                Card value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }


    }
}
