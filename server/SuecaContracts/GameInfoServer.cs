using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace SuecaContracts
{
    class GameInfoServer
    {
        List<Card> listCard;
        CardColor asset;

        public List<GameInfo> ListGameInfoPlayer { get; set; }

        public GameInfoServer()
        {
            listCard = new List<Card>();
            ListGameInfoPlayer = new List<GameInfo>();

            //List not static to permit the mix
            List<CardColor> listColor = new List<CardColor>();

            foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
            {
                foreach(CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    listCard.Add(new Card(color,value));
                }
                listColor.Add(color);
            }
            mix(listCard);

            //Choose the 
            Random rand = new Random();
            asset = listColor[rand.Next(4)];
        }

        public void distributeCardsPerPlayer(Player p)
        {
            //Distribute ten cards to a player (start game)
            for(int i=0; i < 10; i++)
            {
                p.ListCardsHolding.Add(listCard[i]);
            }

            listCard.RemoveRange(0,10);
        }

        public void createGameInfoClient(List<Player> listPlayer)
        {
            ListGameInfoPlayer.Clear();

            foreach(Player p in listPlayer)
            {
                ListGameInfoPlayer.Add(new GameInfo(p.Token, p.ListCardsWin.Count));
            }
        }

        /// <summary>
        /// Mix a list of cards in a real randomness
        /// </summary>
        /// <typeparam name="T"></typeparam>
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
