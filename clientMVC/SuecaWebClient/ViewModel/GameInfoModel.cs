using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuecaWebClient.SuecaService;

namespace SuecaWebClient.Models
{
    public class GameInfoModel
    {
        

        public int playerTurn { get; set; }

        public Card[] listCards { get; set; }

        

        public GameInfoModel(int playerTurn)
        {
            this.playerTurn = playerTurn;
        }

        public GameInfoModel(SuecaService.GameInfo gameInfo)
        {
            playerTurn = gameInfo.NumberPlayerToPlay;
            listCards = gameInfo.ListCardsPlayed;
            
        }


    }
}