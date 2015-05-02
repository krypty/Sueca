using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace SuecaWebClient.Models
{
    public class RoomInfoModel
    {

        public int gameState { get; set; }

        //public 
        public List<Boolean> getPlayerState { get; set; }
        
        public string roomId { get; set; }
        public string playerToken { get; set; }

        //public 
        public RoomInfoModel(string roomId, string playerToken, bool[] statesBools)
        {
            this.roomId = roomId;
            this.playerToken = playerToken;
            this.getPlayerState = new List<Boolean>(statesBools);
        }



    }
}