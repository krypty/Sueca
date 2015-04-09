using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using SuecaContracts;

namespace SuecaServices
{
    // Only one instance of the service is running. Only one call by client at a time but multiple clients allowed 
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class ServiceSueca : ISuecaContract
    {
        private Dictionary<String, Room> _dictRoom; // <roomName, Room>
        private Dictionary<String, String> _dictPlayers; // <playerToken, Player>
        private Dictionary<String, ISuecaCallbackContract> _dictCallbacks;

        public ServiceSueca()
        {
            this._dictRoom = new Dictionary<string, Room>();
            this._dictPlayers = new Dictionary<string, string>();
            this._dictCallbacks = new Dictionary<string, ISuecaCallbackContract>();
        }

        public string CreateRoom(string password = "")
        {
            Console.WriteLine("[server] createRoom");
            Room room = new Room(password);
            _dictRoom.Add(room.Name, room);
            return room.Name;
        }

        public String JoinRoom(string roomName, string password = "")
        {
            Console.WriteLine("[server] joinRoom");
            
            if (!_dictRoom.ContainsKey(roomName))
            {
                throw new Exception("room with name [" + roomName + "] doesn't exist");
            }

            Room room = this._dictRoom[roomName];

            
            String playerToken = Guid.NewGuid().ToString();
            _dictCallbacks.Add(playerToken, OperationContext.Current.GetCallbackChannel<ISuecaCallbackContract>());
            
            
            String player = "toto"; // TODO: String -> Player
            this._dictPlayers.Add(playerToken, player);
            room.AddPlayer(player);

            Console.WriteLine("[server] call callback");
            //_callback.GameStarted("[server] " + player + " has been added to a room");
            Console.WriteLine("[server] callback called");
            return playerToken;
        }

        public List<Room> ListRoom()
        {
            return this._dictRoom.Values.ToList();
        }

        public void SendReady(string playerToken, bool isReady)
        {
            var currentCallback = OperationContext.Current.GetCallbackChannel<ISuecaCallbackContract>();

            var playerToken = _dictCallbacks.FirstOrDefault(x => x.Value == currentCallback).Key;

            throw new NotImplementedException();
        }
    }
}
