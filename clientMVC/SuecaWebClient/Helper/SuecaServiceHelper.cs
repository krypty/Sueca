using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuecaWebClient.SuecaService;
using SuecaWebClient.Models;


namespace SuecaWebClient.Helper
{

    public class SuecaServiceHelper
    {
        SuecaClient client = null;

        public SuecaServiceHelper()
        {
            client = new SuecaClient(new System.ServiceModel.InstanceContext(new InterfaceService()));

        }

        public RoomInfoModel joinRoom(string roomId, string password = "")
        {
            
            string playerToken = client.JoinRoom(roomId, password);
            


            return new RoomInfoModel(roomId,playerToken);
        }



        public void getRoomState()
        {
            //SuecaService.Room.StateRoom
            //client.JoinRoom();

        }

        public void sendClientReady(bool state)
        {


        }

        
        public Room[] getRoomsArray()
        {
            try
            {
                return client.ListRoom();
            }
            catch(Exception e)
            {
                return null;
            }
            
        }

        public String createRoom(string password = "")
        {
            try
            {
                return client.CreateRoom(password);
            }
            catch(Exception e)
            {
                return "";
            }
        }


        private class InterfaceService : SuecaService.SuecaCallback
        {

            public void GameStarted(string message)
            {
                //TODO
            }


            public void RoomUpdated(Room room)
            {
                throw new NotImplementedException();
            }
        }


    }
}