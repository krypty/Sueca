using System.ServiceModel;
using suecaWPFClient.ServiceReference1;

namespace suecaWPFClient
{

    class ServiceManager : SuecaCallback
    {
        private static ServiceManager _instance;

        public delegate void GameInfoUpdatedEventHandler(string message);
        public event GameInfoUpdatedEventHandler OnGameInfoUpdated;

        private readonly Sueca _suecaClient;

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
            return _suecaClient.JoinRoom(roomName, password);
        }

        internal Room[] ListRoom()
        {
            return _suecaClient.ListRoom();
        }


        public void GameStarted(string message)
        {
            if (OnGameInfoUpdated != null)
            {
                OnGameInfoUpdated(message);
            }
        }
    }
}
