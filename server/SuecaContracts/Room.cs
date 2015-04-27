using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
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
            //CAREFULL : if somebody quit the game => not manage
            //Add a number to known when the user comes
            //First : 0, second : 1, third : 2, fourth : 3
            player.NumberTurn = listPlayers.Count;

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
                if (nbPlayersReady == 1)
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

                if (RoomState == StateRoom.GAME_IN_PROGRESS)
                {
                    RoomState = StateRoom.WAITING_READY;
                    
                    //If the user send not ready during a game, it means that he leaves the game
                    IEnumerable<Player> playerToRemove = listPlayers.Where<Player>(p => p.Token == playerToken);
                    Player player = playerToRemove.First();
                    listPlayers.Remove(player);

                    gameInfo = null;
                    gameUpdated = null;
                }
            }
        }

        private void StartGame()
        {
            //Initialize the gameInfoServer
            gameInfo = new GameInfoServer(listPlayers);

            //Get the callback from players
            foreach (Player p in ListPlayers)
            {
                gameUpdated += p.Callback.RoomUpdated;
            }

            //Change state of room
            RoomState = StateRoom.GAME_IN_PROGRESS;

            //Distribute cards
            gameInfo.distributeCardsForEachPlayer();

            Console.WriteLine("[server] has distribute cards");

            new Thread(new ParameterizedThreadStart(
                delegate(object room)
                {
                    gameUpdated((Room)room);
                })
            ).Start(this);

            Console.WriteLine("[server] has send callback to update room");
        }

        public void PlayCard(string playerToken, CardColor color, CardValue value)
        {
            gameInfo.PlayCard(playerToken, color, value);
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
