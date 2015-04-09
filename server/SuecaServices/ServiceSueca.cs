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
        //private ISuecaCallbackContract _callback = null;

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
            ISuecaCallbackContract _callback = OperationContext.Current.GetCallbackChannel<ISuecaCallbackContract>();
            
            if (!_dictRoom.ContainsKey(roomName))
            {
                throw new Exception("room with name [" + roomName + "] doesn't exist");
            }

            Room room = this._dictRoom[roomName];

            // TODO: Check that playerToken is unique
            String playerToken = Guid.NewGuid().ToString();
            String player = "toto"; // TODO: String -> Player
            this._dictPlayers.Add(playerToken, player);
            room.AddPlayer(player);
            _dictCallbacks.Add(playerToken, _callback);
            Console.WriteLine("[server] call callback");

            sendGameStarted(playerToken);
            //_callback.GameStarted("[server] " + player + " has been added to a room");
            //Console.WriteLine("[server] callback called");
            return playerToken;
        }


        public void sendGameStarted(string playerToken)
        {
            foreach (KeyValuePair<string, ISuecaCallbackContract> callback in _dictCallbacks)
            {
                callback.Value.GameStarted("YO Y'A TA GAME QUI EST LANCEE VIEUX BIDET de " + callback.Key);


            }
            //_dictCallbacks[playerToken].GameStarted("YOLO IT'S UP BITCHES");
        }

        public List<Room> ListRoom()
        {
            return this._dictRoom.Values.ToList();
        }

        public void SendReady(bool isReady)
        {
            throw new NotImplementedException();
        }
    }
}
