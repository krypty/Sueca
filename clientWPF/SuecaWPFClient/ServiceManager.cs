using System;
using System.ServiceModel;
using suecaWPFClient.ServiceReference1;

namespace suecaWPFClient
{

    class ServiceManager : SuecaCallback
    {
        private static ServiceManager _instance;

        //public delegate void GameInfoUpdatedEventHandlerText(string message);
        //public event GameInfoUpdatedEventHandlerText OnGameInfoUpdatedText;

        public delegate void GameInfoUpdatedEventHandler(GameInfo gameInfo);
        public event GameInfoUpdatedEventHandler OnGameInfoUpdated;

        public delegate void RoomUpdatedEventHandler(Room room);
        public event RoomUpdatedEventHandler OnRoomUpdated;

        private readonly Sueca _suecaClient;

        public string PlayerToken { get; set; }

        private ServiceManager()
        {
            _suecaClient = new SuecaClient(new InstanceContext(this));
        }

        public static ServiceManager GetInstance()
        {
            return _instance ?? (_instance = new ServiceManager());
        }

        internal string CreateRoom(string password)
        {
            return _suecaClient.CreateRoom(password);
        }

        internal string JoinRoom(string roomName, string password)
        {
            return _suecaClient.JoinRoom(roomName, password, true);
        }

        internal void SendReady(string playerToken, bool isReady)
        {
            _suecaClient.SendReady(playerToken, isReady);
        }

        internal Room[] ListRoom()
        {
            return _suecaClient.ListRoom();
        }

        public void GameStarted(string message)
        {
           throw new NotImplementedException();
        }

        public void RoomUpdated(Room room)
        {
            if (OnRoomUpdated != null)
            {
                OnRoomUpdated(room);
            }
        }

        public void GameInfoUpdated(GameInfo gameInfo)
        {
            if (OnGameInfoUpdated != null)
            {
                OnGameInfoUpdated(gameInfo);
            }
        }
    }
}
