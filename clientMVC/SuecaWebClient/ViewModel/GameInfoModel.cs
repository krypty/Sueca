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


        public bool playerTurn { get; set; }

        public CardInfo[] listPlayedCards { get; set; }

        public string colorGame { get; set; }

        public CardInfo[] listPlayerCards { get; set; }

        public int[] nbCards { get; set; }
    



        public GameInfoModel(SuecaService.GameInfo gameInfo)
        {
            playerTurn = gameInfo.IsMyTurn;
            colorGame = SuecaServiceHelper.CardColorToString(gameInfo.Asset);
            nbCards = new int[4];
            listPlayedCards = new CardInfo[4];
            for (int i = 0; i < 4;i++)
            {
                if(gameInfo.ListCardsPlayed[i] != null)
                {
                    Card c = gameInfo.ListCardsPlayed[i];
                    
                    listPlayedCards[i] = new CardInfo(c.Color, c.Value);
                    
                }
               
                Player p = gameInfo.ListPlayer[i];
                nbCards[i] = p.HoldingCards;
               

            }
           
            
            

            listPlayerCards = new CardInfo[10];

            int j = 0;
            foreach (Card c in gameInfo.ListCardsPlayer)
            {
                listPlayerCards[j] = new CardInfo(c.Color, c.Value);
                j++;
            }
            
        }


    }
}