using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using SuecaContracts;

using System.Timers;
using System.Threading;

namespace SuecaServices
{
    // Only one instance of the service is running. Only one call by client at a time but multiple clients allowed 
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class ServiceSueca : ISuecaContract
    {
        private List<Room> _listRooms;
        delegate void delegateCallbacks(ISuecaCallbackContract callback);

        public ServiceSueca()
        {
            this._listRooms = new List<Room>();

        }

        public string CreateRoom(string password = "")
        {
            Console.WriteLine("[server] createRoom");
            Room room = new Room(password);
            _listRooms.Add(room);
            return room.Name;
        }

        public String JoinRoom(string roomName, string password = "")
        {
            try
            {
                Console.WriteLine("[server] joinRoom");

                if (!_listRooms.Exists(r => r.Name == roomName))
                {
                    throw new Exception("room with name [" + roomName + "] doesn't exist");
                }

                Room room = this._listRooms.Find(r => r.Name == roomName);

                if (password != "" && password != room.Password)
                {
                    throw new Exception("room with name [" + roomName + "] has not this password");
                }

                String playerToken = Guid.NewGuid().ToString();

                Player newPlayer = new Player(playerToken);
                newPlayer.Callback = OperationContext.Current.GetCallbackChannel<ISuecaCallbackContract>();
                room.AddPlayer(newPlayer);

                return playerToken;
                
            }
            catch
            {
                return "";
            }
        }


        public List<Room> ListRoom()
        {
            return this._listRooms;
        }

        public void SendReady(string playerToken, bool isReady)
        {
            Console.WriteLine("[server] Player with token " + playerToken + " send ready");

            IEnumerable<Room> room = _listRooms.Where<Room>(r => r.ListPlayers.Exists(p => p.Token == playerToken));
            
            if(room.Count() < 1)
            {
                throw new Exception("Player with token [" + playerToken + "] doesn't exist");
            }

            Room currentRoom = room.First();

            Player currentPlayer = currentRoom.ListPlayers.Find(p => p.Token == playerToken);

            if(isReady)
            {
                currentPlayer.IsReady = true;

                int nbPlayersReady = currentRoom.ListPlayers.Count<Player>(p => p.IsReady);

                if(nbPlayersReady >= 4)
                {
                    delegateCallbacks delCallbacks = delegate(ISuecaCallbackContract callback)
                        {
                            callback.GameStarted("game started");
                        };

                    Console.WriteLine("[server] Send callback to players");
                    
                   // currentRoom.ListPlayers.ForEach(p => delCallbacks(p.Callback));
                    foreach(Player p in currentRoom.ListPlayers)
                    {
                        if(p.Token != playerToken)
                        {
                            delCallbacks(p.Callback);
                        }
                        else
                        {
                            delCallbacks(OperationContext.Current.GetCallbackChannel<ISuecaCallbackContract>());
                        }

                    }

                }
                else
                {
                    Console.WriteLine("[server] Not enough player to send callback");
                }
            }

        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }

    }
}
