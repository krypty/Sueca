using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using SuecaContracts;

using System.Threading;

namespace SuecaServices
{
    // Only one instance of the service is running. Only one call by client at a time but multiple clients allowed 
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
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

        public ServiceSueca()
        {
           // this._listRooms = new List<Room>();
            this.dictRoom = new Dictionary<string, Room>();
            timerCheckRoom = new System.Timers.Timer(CHECK_ROOM_TIME);
            timerCheckRoom.Elapsed += (sender, e) =>
            {
                timerCheckRoom_Elapsed(dictRoom);
            }; 
        }

        void timerCheckRoom_Elapsed(Dictionary<string,Room> dict)
        {
            Console.WriteLine("[server] check if a room has to be killed");
            foreach(Room r in dictRoom.Values)
            {
                if(r.PlayerDisconnectDuringParty != null)
                {
                    //If the party did not begin
                    if(r.RoomState == Room.StateRoom.WAITING_READY)
                    {
                        //Permit to another user to come in the room by removing the other user
                        r.ListPlayers.Remove(r.PlayerDisconnectDuringParty);

                        //Reset the order of turn
                        foreach(Player p in r.ListPlayers)
                        {
                            switch(r.PlayerDisconnectDuringParty.NumberTurn)
                            {
                                case 0:
                                    p.NumberTurn--;
                                    break;
                                case 1:
                                    if(p.NumberTurn > 0)
                                        p.NumberTurn--;
                                    break;
                                case 2:
                                    if(p.NumberTurn > 1)
                                        p.NumberTurn--;
                                    break;
                                default:
                                    break;
                            }
                        }

                        Console.WriteLine("[server] the room " + r.Name + " has the player " + r.PlayerDisconnectDuringParty.Token + " disconnect and reset the order of turn");
                    }
                    else if(r.RoomState == Room.StateRoom.GAME_IN_PROGRESS)
                    {
                        //Kill the room
                        Console.WriteLine("[server] the room " + r.Name + " has to be killed because player " + r.PlayerDisconnectDuringParty.Token + " is disconnect");
                    }
                }
            }
        }

        public string CreateRoom(string password = "")
        {
            Console.WriteLine("[server] createRoom");
            Room room = new Room(password);
            dictRoom.Add(room.Name, room);
            //_listRooms.Add(room);
            return room.Name;
        }

        public String JoinRoom(string roomName, string password = "", bool isUsingCallback=true)
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
                if(currentRoom.CountPlayers() < 4)
                { 
                    if (!currentRoom.Password.Equals(password))
                    {
                        Console.WriteLine("[Join room] invalid password");
                        return null;
                    }
                    else
                    {
                        playerToken = Guid.NewGuid().ToString();

                        Player newPlayer = new Player(playerToken);
                        if(isUsingCallback)
                            newPlayer.Callback = OperationContext.Current.GetCallbackChannel<ISuecaCallbackContract>();
                        currentRoom.AddPlayer(newPlayer);
                    }
                }
                else
                {
                    return "The game is full";
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
            catch(Exception e)
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

        public void PlayCard(string playerToken, CardColor color, CardValue value)
        {
            Room currentRoom = GetRoomFromPlayerToken(playerToken);

            if(currentRoom != null)
            {
                currentRoom.PlayCard(playerToken,color,value);
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

            if(currentRoom != null && currentRoom.Name == roomId)
            {
                return currentRoom.getGameInfoForPlayerToken(playerToken);
            }
            return null;
        }
    }
}
