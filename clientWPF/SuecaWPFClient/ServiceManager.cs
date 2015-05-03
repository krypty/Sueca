using System;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using suecaWPFClient.Cards;
using suecaWPFClient.ServiceReference1;
using Card = suecaWPFClient.ServiceReference1.Card;

namespace suecaWPFClient
{

    class ServiceManager : SuecaCallback
    {
        private static ServiceManager _instance;

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

        internal Task<Room[]> ListRoom()
        {
            return _suecaClient.ListRoomAsync();
        }


        internal void PlayCard(String playerToken, suecaWPFClient.Cards.Card card)
        {
            Card convertedCard = CardTools.ToService(card);
            _suecaClient.PlayCard(playerToken, convertedCard.Color, convertedCard.Value);
        }

        #region CALLBACK methods

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

        #endregion
    }


}
