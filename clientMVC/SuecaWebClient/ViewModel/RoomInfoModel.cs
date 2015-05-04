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

        public int roomState { get; set; }

        //public 
        public int[] getPlayerState { get; set; }
        
        public string roomId { get; set; }
        public string playerToken { get; set; }

        public int playerNumber { get; set; }
    

        //public 
        public RoomInfoModel(string roomId, string playerToken, Player[] players, int roomState)
        {
            this.roomId = roomId;
            this.playerToken = playerToken;
            this.roomState = roomState;
            //0 : not in
            //1 : not rdy
            //2 : rdy
            int[] listState = new int[]{0,0,0,0};


            /*for (int i = 0; i < players.Count();i++)
            {
                listState[i] = players[i].isReady ? 2 : 1;
            }*/

                playerNumber = players.Where(p => p.token == playerToken).First().NumberTurn;
                int i = 0;
                foreach (Player p in players)
                {
                    if (p != null)
                    {
                        //listState[p.NumberTurn] = p.NumberTurn;
                        //i++;
                    
                        if (p.NumberTurn == playerNumber)
                        {
                            listState[0] = p.isReady ? 2 : 1;
                        }
                        else if (p.NumberTurn == ((playerNumber + 1)%4))
                        {
                            listState[1] = p.isReady ? 2 : 1;
                        }
                        else if (p.NumberTurn == ((playerNumber + 2) % 4))
                        {
                            listState[2] = p.isReady ? 2 : 1;
                        }
                        else if (p.NumberTurn == ((playerNumber + 3) % 4))
                        {
                            listState[3] = p.isReady ? 2 : 1;
                        }

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