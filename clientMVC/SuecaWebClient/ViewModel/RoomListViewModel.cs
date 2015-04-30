using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SuecaWebClient.Models
{
    public class RoomListViewModel
    {
        public string roomId { get; set; }

        public bool hasPassword { get; set; }

        public RoomListViewModel(string roomId, bool hasPassword = false)
        {
            this.roomId = roomId;
            this.hasPassword = hasPassword;
        }

    }
}