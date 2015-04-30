using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuecaWebClient.Models
{
    public class RoomInfoModel
    {

        public int gameState { get; set; }

        //public 
        
        public string roomId { get; set; }
        public string playerToken { get; set; }

        //public 
        public RoomInfoModel(string roomId, string playerToken)
        {
            this.roomId = roomId;
            this.playerToken = playerToken;

        }



    }
}