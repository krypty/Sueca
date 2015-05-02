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
                //throw new Exception("room with name [" + roomName + "] doesn't exist");
            }

            //Room room = this._listRooms.Find(r => r.Name == roomName);
            if (currentRoom != null)
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

            

            return playerToken;
        }


        public List<Room> ListRoom()
        {
            return this.dictRoom.Values.ToList();
        }

        public Room GetRoom(string roomName)
        {
            Room currentRoom;
            if (!dictRoom.TryGetValue(roomName, out currentRoom))
            {
                return null;
            }
            return currentRoom;
        }


        public void SendReady(string playerToken, bool isReady)
        {
            Console.WriteLine("[server] Player with token " + playerToken + " send ready");

            Room currentRoom = GetRoomFromPlayerToken(playerToken);

            currentRoom.MakePlayerReady(playerToken, isReady);
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
                throw new Exception("Player with token [" + playerToken + "] doesn't exist");
            }

            return room.First();
        }

        public GameInfo GetGameInfo(string playerToken, string roomId)
        {
            return null;
        }
    }
}
