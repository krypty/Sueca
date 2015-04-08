using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuecaWebClient.SuecaService;


namespace SuecaWebClient.Helper
{

    public class SuecaServiceHelper
    {
        SuecaClient client = null;

        public SuecaServiceHelper()
        {
            client = new SuecaClient(new System.ServiceModel.InstanceContext(new InterfaceService()));

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
        }


    }
}