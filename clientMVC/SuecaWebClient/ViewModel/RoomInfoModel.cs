using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using SuecaWebClient.SuecaService;

namespace SuecaWebClient.Models
{
    public class RoomInfoModel
    {

        public int gameState { get; set; }

        //public 
        public int[] getPlayerState { get; set; }
        
        public string roomId { get; set; }
        public string playerToken { get; set; }

        public int playerNumber { get; set; }
    

        //public 
        public RoomInfoModel(string roomId, string playerToken, Player[] players)
        {
            this.roomId = roomId;
            this.playerToken = playerToken;

            //0 : not in
            //1 : not rdy
            //2 : rdy
            int[] listState = new int[]{0,0,0,0};

            int number = players.Where(p => p.token == playerToken).First().NumberTurn;

            playerNumber = number;

            foreach (Player p in players)
            {
                if (p != null)
                {
                    listState[(playerNumber + p.NumberTurn)%4] = p.isReady ? 2 : 1;
                }
            }
            

            getPlayerState = listState;
            /*foreach (Player p in players)
            {
                p.isReady
            }*/

            //this.getPlayerState = new List<Boolean>(statesBools);

        }



    }
}