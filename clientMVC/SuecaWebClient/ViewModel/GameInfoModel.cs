using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuecaWebClient.Helper;
using SuecaWebClient.SuecaService;

namespace SuecaWebClient.Models
{
    public class GameInfoModel
    {


        public class CardInfo
        {
            public string color { get; set; }
            public string value { get; set; }

            public CardInfo(CardColor color, CardValue value)
            {
                this.color = SuecaServiceHelper.CardColorToString(color);
                this.value = SuecaServiceHelper.CardValueToString(value);
            }

        }


        public int playerTurn { get; set; }

        public CardInfo[] listPlayedCards { get; set; }


        public CardInfo[] listPlayerCards { get; set; }

        public int[] nbCards { get; set; }
    



        public GameInfoModel(int playerTurn)
        {
            this.playerTurn = playerTurn;
        }

        public GameInfoModel(SuecaService.GameInfo gameInfo)
        {
            playerTurn = gameInfo.NumberPlayerToPlay;


            listPlayedCards = new CardInfo[4];
            int i = 0;
            foreach (Card c in gameInfo.ListCardsPlayed)
            {
                listPlayedCards[i] = new CardInfo(c.Color,c.Value);
                i++;
            }

            nbCards = new int[4];
            i = 0;
            foreach(Player p in gameInfo.ListPlayer)
            {
                nbCards[i] = p.holdingCards;
                i++;

            }
            //gameInfo.ListPlayer

            //listCards = gameInfo.ListCardsPlayed;

            listPlayerCards = new CardInfo[10];
            i = 0;
            foreach (Card c in gameInfo.ListCardsPlayer)
            {
                listPlayerCards[i] = new CardInfo(c.Color, c.Value);
                i++;
            }
            
        }


    }
}