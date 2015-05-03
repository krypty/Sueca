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

        public String joinRoom(string roomId, string password = "")
        {
            if (roomId != "" && client != null)
            {
                try
                {
                    string playerToken = client.JoinRoom(roomId, password, false);
                    return playerToken;
                }
                catch (Exception e)
                {
                    
                }
            }
            return "";
        }



        public RoomInfoModel getRoomState(string roomId, string playerToken)
        {
            //SuecaService.Room.StateRoom
            //client.JoinRoom();
            if (roomId != "" && client != null)
            {
                try
                {


                    Room r = client.GetRoom(roomId);
                    if (r != null)
                        return new RoomInfoModel(r.Name, playerToken, r.listPlayers);
                    else
                        return null;

                }
                catch (Exception e)
                {
                    
                }
            }
            return null;
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
                //throw new NotImplementedException();
            }


            public void GameInfoUpdated(GameInfo gameInfo)
            {
                //throw new NotImplementedException();
            }
        }


    }
}