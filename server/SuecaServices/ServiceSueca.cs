using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using SuecaContracts;

using System.Threading;

namespace SuecaServices
{
    // Only one instance of the service is running. Only one call by client at a time but multiple clients allowed 
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single, AddressFilterMode = AddressFilterMode.Any)]
    public class ServiceSueca : ISuecaContract
    {
        private const int CHECK_ROOM_TIME = 30000;
        private System.Timers.Timer timerCheckRoom;

        //private List<Room> _listRooms;
        //private HashSet<String, Room>  
        private Dictionary<String, Room> dictRoom;
        //delegate void delegateCallbacks(ISuecaCallbackContract callback);

        public delegate void CallbackDelegate<T>(T t);
        public static CallbackDelegate<string> gameStarted;

        private int NB_CALL = 0;

        public ServiceSueca()
        {
            // this._listRooms = new List<Room>();
            this.dictRoom = new Dictionary<string, Room>();
            timerCheckRoom = new System.Timers.Timer(CHECK_ROOM_TIME);
            timerCheckRoom.Elapsed += (sender, e) =>
            {
                timerCheckRoom_Elapsed();
            };

            timerCheckRoom.Enabled = true;
        }

        void timerCheckRoom_Elapsed()
        {
            var dictCopy = (from x in dictRoom
                            select x).ToDictionary(x => x.Key, x => x.Value);

            Console.WriteLine("[server] check if a room has to be killed " + NB_CALL);
            foreach (Room r in dictCopy.Values)
            {
                lock (r)
                {

                    foreach (Player p in r.ListPlayers)
                    {
                        if (p.TimeOutClientWeb.HasValue)
                        {
                            Console.WriteLine("[server] check web client player " + p.Token);
                            if (DateTime.Now.Subtract(p.TimeOutClientWeb.Value).Milliseconds > 10000)
                            {
                                //A web client is deconnect
                                r.IsPlayerDisconnectDuringParty = true;
                                r.ListPlayerDisconnectDuringParty.Add(p);
                                Console.WriteLine("[server] the player " + p.Token + " is disconnect");
                            }
                        }
                        else if (p.Callback != null)
                        {
                            Console.WriteLine("[server] check wpf client player " + p.Token);
                            try
                            {
                                p.Callback.CheckConnection();
                            }
                            catch
                            {
                                Console.WriteLine("[server] client player disconnect");
                                r.ListPlayerDisconnectDuringParty.Add(p);
                            }

                        }
                    }


                    if (r.ListPlayerDisconnectDuringParty.Count > 0)
                    {
                        //Permit to another user to come in the room by removing the other user
                        foreach (Player p in r.ListPlayerDisconnectDuringParty)
                        {
                            r.RemovePlayer(p);
                            Console.WriteLine("[server] remove the player " + p.Token + " from the list players");
                        }


                        int i = 0;
                        //Reset the order of turn
                        foreach (Player p in r.ListPlayers)
                        {
                            p.NumberTurn = i;
                            i++;
                        }

                        r.ListPlayerDisconnectDuringParty.Clear();

                        if (r.RoomState == Room.StateRoom.GAME_IN_PROGRESS || r.RoomState == Room.StateRoom.END_GAME)
                        {
                            r.ResetRoom();
                            Console.WriteLine("[server] the room " + r.Name + " has to be relaunch ");
                        }

                       
                    }
                }
            }

            NB_CALL++;

        }

        public string CreateRoom(string password = "")
        {
            Console.WriteLine("[server] createRoom");
            Room room = new Room(password);
            dictRoom.Add(room.Name, room);
            //_listRooms.Add(room);
            return room.Name;
        }

        public String JoinRoom(string roomName, string password = "", bool isUsingCallback = true)
        {
            Console.WriteLine("[server] joinRoom");
            Room currentRoom;
            String playerToken = "";
            if (!dictRoom.TryGetValue(roomName, out currentRoom))
            {
                Console.WriteLine("[Join room] room doesn't exist");
                return "";
            }

            //Room room = this._listRooms.Find(r => r.Name == roomName);
            if (currentRoom != null)
            {
                if (currentRoom.CountPlayers() < Room.NB_PLAYER_PER_GAME)
                {
                    if (!currentRoom.Password.Equals(password))
                    {
                        Console.WriteLine("[Join room] invalid password");
                        return null;
                    }

                    playerToken = Guid.NewGuid().ToString();

                    Player newPlayer = new Player(playerToken);
                    if (isUsingCallback)
                        newPlayer.Callback = OperationContext.Current.GetCallbackChannel<ISuecaCallbackContract>();
                    currentRoom.AddPlayer(newPlayer);

                }
                else
                {
                    return null;
                }
            }

            return playerToken;
        }


        public List<Room> ListRoom()
        {
            return this.dictRoom.Values.ToList();
        }

        public Room GetRoom(string playerToken, string roomName)
        {
            Room currentRoom;
            try
            {
                if (!dictRoom.TryGetValue(roomName, out currentRoom))
                {
                    return null;
                }

                Player currentPlayer = currentRoom.ListPlayers.Where<Player>(p => p.Token == playerToken).First();
                currentPlayer.TimeOutClientWeb = DateTime.Now;

                return currentRoom;

            }
            catch
            {
                return null;
            }
        }


        public void SendReady(string playerToken, bool isReady)
        {
            Console.WriteLine("[server] Player with token " + playerToken + " send ready");
            if (playerToken != "")
            {
                Room currentRoom = GetRoomFromPlayerToken(playerToken);

                currentRoom.MakePlayerReady(playerToken, isReady);
            }

        }

        public bool PlayCard(string playerToken, CardColor color, CardValue value)
        {
            Room currentRoom = GetRoomFromPlayerToken(playerToken);

            if (currentRoom != null)
            {
                return currentRoom.PlayCard(playerToken, color, value);
            }

            return false;
        }

        public void SendEndGameReceived(string playerToken)
        {
            Console.WriteLine("send game received !");

            if (playerToken != "")
            {
                try
                {
                    Room currentRoom = GetRoomFromPlayerToken(playerToken);
                    if (currentRoom != null)
                    {
                        Console.WriteLine("send game received 2");
                        if (!currentRoom.ListPlayersReceivedEndGame.Exists(p => p.Token == playerToken))
                            currentRoom.ListPlayersReceivedEndGame.Add(currentRoom.ListPlayers.First(p => p.Token == playerToken));

                        Console.WriteLine("count: " + currentRoom.ListPlayersReceivedEndGame.Count);
                        if (currentRoom.ListPlayersReceivedEndGame.Count >= 4)
                        {
                            Console.WriteLine("send game received 3");
                            currentRoom.ResetRoom();
                        }
                    }
                }
                catch
                {

                }
            }
        }

        private Room GetRoomFromPlayerToken(string playerToken)
        {
            List<Room> listRoom = dictRoom.Values.ToList();
            IEnumerable<Room> room = listRoom.Where<Room>(r => r.ListPlayers.Exists(p => p.Token == playerToken));

            if (room.Count() < 1)
            {
                //throw new Exception("Player with token [" + playerToken + "] doesn't exist");
                return null;
            }

            return room.First();
        }

        public GameInfo GetGameInfo(string playerToken, string roomId)
        {
            Room currentRoom = GetRoomFromPlayerToken(playerToken);

            if (currentRoom != null && currentRoom.Name == roomId)
            {
                return currentRoom.getGameInfoForPlayerToken(playerToken);
            }
            return null;
        }
    }
}
