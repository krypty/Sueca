using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Threading;

namespace SuecaContracts
{
    [DataContract(Name = "StateRoom")]
    public enum StateRoom
    {
        [EnumMember]
        WAITING_READY,
        [EnumMember]
        GAME_IN_PROGRESS,
        [EnumMember]
        END_GAME
    };

    [DataContract]
    public class Room
    {
        private const int NB_PLAYER_PER_GAME = 2;
        private int nbPlayersReady;
        public delegate void CallbackDelegate<T>(T t);
        public CallbackDelegate<string> gameStarted;
        public CallbackDelegate<Room> gameUpdated;

        private GameInfoServer gameInfo;

        private List<GameInfo> listGameInfos;

        [DataMember]
        public string Name { get; set; }

        public string Password { get; set; }

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

            //Add the new player callback to the delegate
            if(player.Callback != null)
                gameUpdated += player.Callback.RoomUpdated;
            UpdateRoomForClient();
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

                Console.WriteLine("[server] has send callback to update room because a player send ready");

                //If the room is full, launch the game
                if (nbPlayersReady == NB_PLAYER_PER_GAME)
                {
                    /*
                    foreach (Player p in ListPlayers)
                    {
                        gameStarted += p.Callback.GameStarted;
                    }
                    */
                    /*
                    //Start the game for the client
                    new Thread(new ParameterizedThreadStart(
                        delegate(object message)
                        {
                            gameStarted((string)message);
                        })
                    ).Start("Game started");
                    */

                    new System.Threading.Timer(obj => { StartGame(); }, null, 1000, System.Threading.Timeout.Infinite);
                    //Start the game for the server
                    //new Thread(StartGame).Start();
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

            //Update the room to the clients everytime that a client sendready something
            UpdateRoomForClient();
        }

        private void StartGame()
        {
            Console.WriteLine("[server] Start the game");


            Console.WriteLine("[server] construct party");
            //Initialize the gameInfoServer
            gameInfo = new GameInfoServer(listPlayers);

            /*
            //Get the callback from players
            foreach (Player p in ListPlayers)
            {
                gameUpdated += p.Callback.RoomUpdated;
            }
            */

            Console.WriteLine("[server] change the state of the room");
            //Change state of room
            RoomState = StateRoom.GAME_IN_PROGRESS;

            //Distribute cards
            gameInfo.distributeCardsForEachPlayer();

            Console.WriteLine("[server] has distribute cards");

            gameInfo.CreateGameInfoClient(listPlayers);

            /*
            new Thread(new ParameterizedThreadStart(
                delegate(object room)
                {
                    gameUpdated((Room)room);
                })
            ).Start(this);
            */

        }

        private void UpdateRoomForClient()
        {
            new System.Threading.Timer(obj => { gameUpdated(this); }, null, 1000, System.Threading.Timeout.Infinite);
            /*
            new Thread(new ParameterizedThreadStart(
                delegate(object room)
                {
                    gameUpdated((Room)room);
                })
            ).Start(this);
            */
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
