using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using System.Threading;

namespace SuecaContracts
{
    [DataContract]
    public class Room
    {
        private int nbPlayersReady;
        public delegate void CallbackDelegate<T>(T t);
        public CallbackDelegate<string> gameStarted;
        public CallbackDelegate<Room> gameUpdated;

        private GameInfoServer gameInfo;

        private List<GameInfo> listGameInfos;

        [DataMember]
        public string Name { get; set; }

        public string Password { get; set; }

        public enum StateRoom
        {
            WAITING_READY,
            GAME_IN_PROGRESS,
            END_GAME
        };

        [DataMember]
        public StateRoom RoomState
        { get; private set; }

        [DataMember]
        private List<Player> listPlayers;

        public List<Player> ListPlayers
        {
            get { return listPlayers; }
            set { listPlayers = value; }
        }

        public Room(string password = "")
        {
            Password = password;
            Name = GenerateName();

            nbPlayersReady = 0;
            listPlayers = new List<Player>();
            RoomState = StateRoom.WAITING_READY;
        }

        public void AddPlayer(Player player)
        {
            listPlayers.Add(player);
            //  listGameInfos.Add(GameInfoFactory(player.Token));
        }

        public int CountPlayers()
        {
            return listPlayers.Count;
        }

        public void RemovePlayer(Player player)
        {
            listPlayers.Remove(player);
        }

        public void MakePlayerReady(string playerToken, bool isReady)
        {
            Player currentPlayer = ListPlayers.Find(p => p.Token == playerToken);

            currentPlayer.IsReady = isReady;

            if (isReady)
            {
                nbPlayersReady++;

                //If the room is full, launch the game
                if (nbPlayersReady == 2)
                {
                    foreach (Player p in ListPlayers)
                    {
                        gameStarted += p.Callback.GameStarted;
                    }

                    //Start the game for the client
                    new Thread(new ParameterizedThreadStart(
                        delegate(object message)
                        {
                            gameStarted((string)message);
                        })
                        ).Start("Game started");

                    //Start the game for the server
                    new Thread(StartGame).Start();
                }
            }
            else
            {
                nbPlayersReady--;
            }
        }

        private void StartGame()
        {
            //Initialize the gameInfoServer
            gameInfo = new GameInfoServer();

            //Get the callback from players
            foreach (Player p in ListPlayers)
            {
                gameUpdated += p.Callback.RoomUpdated;
            }

            //Change state of room
            RoomState = StateRoom.GAME_IN_PROGRESS;

            //Distribute cards
            DistributeCards();

            Console.WriteLine("[server] has distribute cards");

            //Call
            new Thread(new ParameterizedThreadStart(
                delegate(object room)
                {
                    gameUpdated((Room)room);
                })
            ).Start(this);

            Console.WriteLine("[server] has send callback to update room");
        }

        private void DistributeCards()
        {
            foreach(Player p in listPlayers)
            {
                gameInfo.distributeCardsPerPlayer(p);
            }
        }

        private static string GenerateName()
        {
            string guid = Guid.NewGuid().ToString();
            return guid.Split('-')[0];
        }

        override public String ToString()
        {
            return Name;
        }
    }
}
