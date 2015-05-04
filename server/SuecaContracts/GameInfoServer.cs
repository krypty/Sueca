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
        public Dictionary<string, Player> DictPlayers { get; set; }
        //0-3
        public int NumberPlayerTurn { get; set; }
        //1-4
        public int NumberPlayersHavePlayed { get; set; }
        //Number of first player of the round
        public int FirstPlayerToPlay { get; set; }

        public LinkedList<Card> ListCardsTurn;
        //Dictionnary to known who's player have this card on a turn
        public Dictionary<string, Card> DictCardPlayerForTurn { get; set; }

        public Dictionary<string, GameInfo> DictGameInfoPlayer { get; set; }

        public GameInfoServer()
        {
            listCard = new List<Card>();
            DictPlayers = new Dictionary<string, Player>();
            DictGameInfoPlayer = new Dictionary<string, GameInfo>();

            List<CardColor> listColor = new List<CardColor>();

            //List not static to permit the mix
            foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
            {
                foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    if (color != CardColor.None & value != CardValue.None)
                        listCard.Add(new Card(color, value));
                }
                if (color != CardColor.None)
                    listColor.Add(color);
            }
            mix(listCard);

            //Choose the 
            Random rand = new Random();
            asset = listColor[rand.Next(4)];
        }


        public GameInfoServer(List<Player> listPlayers)
            : this()
        {
            foreach (Player p in listPlayers)
            {
                DictPlayers.Add(p.Token, p);
            }

            //Define the first player to play and the number of players who have played
            NumberPlayerTurn = 0; //Player to the right of the first player
            NumberPlayersHavePlayed = 0;
            FirstPlayerToPlay = NumberPlayerTurn;
        }

        public void distributeCardsForEachPlayer()
        {
            foreach (Player p in DictPlayers.Values)
            {
                //Distribute ten cards to a player (start game)
                for (int i = 0; i < 10; i++)
                {
                    p.ListCardsHolding.Add(listCard[i]);
                }
                p.HoldingCards = p.ListCardsHolding.Count;
                listCard.RemoveRange(0, 10);
            }

            //Initialize the list to contain all cards play in a turn 
            ListCardsTurn = new LinkedList<Card>();
            DictCardPlayerForTurn = new Dictionary<string, Card>();
        }

        public bool PlayCard(string playerToken, CardColor color, CardValue value)
        {
            bool isSuccess = false;

            Player player;
            if (DictPlayers.TryGetValue(playerToken, out player))
            {
                //If it's the turn of this player
                if (player.NumberTurn == NumberPlayerTurn)
                {
                    //Card played
                    Card card = new Card(color, value);
                    if (ListCardsTurn.Count > 0)
                    {
                        //If it's the right color to play
                        Card firstCard = (ListCardsTurn.First).Value;
                        if (color == firstCard.Color)
                        {
                            isSuccess = PlayCardForPlayer(player, card);
                        }
                        //Else if the player has no card of this color, he has the right to put a card of another color
                        else if (player.ListCardsHolding.Count<Card>(c => c.Color == firstCard.Color) <= 0)
                        {
                            isSuccess = PlayCardForPlayer(player, card);
                        }
                        else
                        {
                            Console.WriteLine("[server] the false user attempt to play");
                        }
                    }
                    else
                    {
                        //First card on the turn
                        isSuccess = PlayCardForPlayer(player, card);
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

            return isSuccess;
        }

        private bool PlayCardForPlayer(Player player, Card card)
        {
            bool isSuccess = false;
            if (card.Value != CardValue.None && card.Color != CardColor.None)
            {
                Card cardToRemove = player.ListCardsHolding.First<Card>(c => c.Value == card.Value & c.Color == card.Color);

                if (player.ListCardsHolding.Remove(cardToRemove))
                {
                    isSuccess = true;
                    player.HoldingCards = player.ListCardsHolding.Count;
                    //Dictionary to know which card a player has played
                    DictCardPlayerForTurn.Add(player.Token, cardToRemove);
                    ListCardsTurn.AddLast(cardToRemove);

                    NumberPlayersHavePlayed++;
                    NumberPlayerTurn = (NumberPlayerTurn+1)%4;

                    if (NumberPlayersHavePlayed >= 4)
                    {
                        CalculateTurn();
                        NumberPlayersHavePlayed = 0;
                    }
                }
                else
                {
                    Console.WriteLine("[server] an error has occured when player " + player.Token + " has played card " + card.Color + " : " + card.Value + " !");
                }
            }
            else
            {
                Console.WriteLine("[server] player " + player.Token + " played no card");
            }

            return isSuccess;
        }


        private void CalculateTurn()
        {
            Card firstCard = (ListCardsTurn.First).Value;
            Card bestCard = firstCard;

            foreach (Card card in ListCardsTurn)
            {
                //If the card has not the same color and is not an asset, the card has no chance of win
                if (card.Color == firstCard.Color || card.Color == asset)
                {
                    if (card.Color == asset)
                    {
                        if (bestCard.Color == asset)
                        {
                            if (card.RealValue > bestCard.RealValue)
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
                        if (card.RealValue > bestCard.RealValue)
                        {
                            bestCard = card;
                        }
                    }
                }
            }

            try
            {
                string winnerToken = DictCardPlayerForTurn.First(x => x.Value == bestCard).Key;

                if (winnerToken != null)
                {
                    Player winner;
                    if (DictPlayers.TryGetValue(winnerToken, out winner))
                    {
                        Console.WriteLine("[server] the best card of the turn is : " + bestCard.Color + " : " + bestCard.Value + " and the player who win is : " + winnerToken);
                        winner.ListCardsWin.AddRange(ListCardsTurn);
                        winner.TakenCards = winner.ListCardsWin.Count;
                        NumberPlayerTurn = winner.NumberTurn;
                        FirstPlayerToPlay = NumberPlayerTurn;
                    }
                    else
                    {
                        Console.WriteLine("[server] an error has occured at the end of the calculation of winner for the turn");
                    }

                    ListCardsTurn.Clear();
                    DictCardPlayerForTurn.Clear();
                }
                else
                {
                    Console.WriteLine("[server] an error has occured at the end of the calculation of winner for the turn");
                }
            }
            catch
            {
                Console.WriteLine("[server] an error has occured when server tried to get the winner of the turn");
            }

        }

        public void CreateGameInfoClient()
        {
            DictGameInfoPlayer.Clear();

            foreach (Player p in DictPlayers.Values)
            {
                DictGameInfoPlayer.Add(p.Token,GameInfoFactory.CreateGameInfoClient(p, this));
            }
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
