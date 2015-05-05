using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Threading;
using System.Timers;

namespace SuecaContracts
{
    [DataContract]
    public class Room
    {
        private System.Timers.Timer timer;

        public bool IsPlayerDisconnectDuringParty { get; set; }
        public List<Player> ListPlayerDisconnectDuringParty { get; set; }
        public List<Player> ListPlayersReceivedEndGame { get; set; }

        private const int TIME_SEND_GAME_INFO_CLIENT = 1000;
        private const int TIME_SEND_GAME_INFO_CLIENT_END_TURN = 5000;
        public const int NB_PLAYER_PER_GAME = 4;
        public delegate void CallbackDelegate<T>(T t);
        public CallbackDelegate<string> gameStarted;
        public CallbackDelegate<Room> gameUpdated;

        private GameInfoServer gameInfo;
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

            listPlayers = new List<Player>();
            ListPlayerDisconnectDuringParty = new List<Player>();
            ListPlayersReceivedEndGame = new List<Player>();

            ResetRoom();
        }

        public void ResetRoom()
        {
            ListPlayerDisconnectDuringParty.Clear();
            IsPlayerDisconnectDuringParty = false;
            gameInfo = null;
            ListPlayersReceivedEndGame.Clear();

            foreach (Player p in listPlayers)
            {
                p.IsReady = false;
                p.ScoreParty = 0;
            }

            RoomState = StateRoom.WAITING_READY;

            UpdateRoomForClient();
        }

        public void AddPlayer(Player player)
        {
            //Add a number to known when the user comes
            //First : 0, second : 1, third : 2, fourth : 3
            player.NumberTurn = listPlayers.Count;

            listPlayers.Add(player);

            //Add the new player callback to the delegate
            if (player.Callback != null)
                gameUpdated += player.Callback.RoomUpdated;

            UpdateRoomForClient();
        }

        public int CountPlayers()
        {
            return listPlayers.Count;
        }

        public void RemovePlayer(Player player)
        {
            listPlayers.Remove(player);

            if (player.Callback != null)
                gameUpdated -= player.Callback.RoomUpdated;

            UpdateRoomForClient();
        }

        public void MakePlayerReady(string playerToken, bool isReady)
        {
            Player currentPlayer = ListPlayers.Find(p => p.Token == playerToken);


            int nbPlayersReady = ListPlayers.Count(p => p.IsReady);

            if (!currentPlayer.IsReady && isReady && nbPlayersReady < 4)
            {
                currentPlayer.IsReady = isReady;
                nbPlayersReady++;

                //If the room is full, launch the game
                if (nbPlayersReady == NB_PLAYER_PER_GAME)
                {
                    //Start the game for the server
                    new Thread(new ParameterizedThreadStart(
                        delegate(object room)
                        {
                            Thread.Sleep(1000);
                            StartGame();
                        })
                    ).Start();
                }
            }
            else if (!isReady)
            {
                if (RoomState == StateRoom.GAME_IN_PROGRESS)
                {
                    /*
                    RoomState = StateRoom.WAITING_READY;
                    
                    //If the user send not ready during a game, it means that he leaves the game
                    IEnumerable<Player> playerToRemove = listPlayers.Where<Player>(p => p.Token == playerToken);
                    Player player = playerToRemove.First();
                    listPlayers.Remove(player);

                    gameInfo = null;
                    gameUpdated = null;
                     * 
                     */
                }
            }

            Console.WriteLine("[server] has send callback to update room because a player send ready to " + isReady);

            //Update the room to the clients everytime that a client sendready something
            UpdateRoomForClient();
        }

        private void StartGame()
        {
            Console.WriteLine("[server] Start the game");

            Console.WriteLine("[server] construct party");
            //Initialize the gameInfoServer
            gameInfo = new GameInfoServer(listPlayers);

            Console.WriteLine("[server] change the state of the room");

            //Change state of room
            RoomState = StateRoom.GAME_IN_PROGRESS;

            //Distribute cards
            gameInfo.distributeCardsForEachPlayer();

            Console.WriteLine("[server] has distribute cards");

            CreateGameInfoClient(TIME_SEND_GAME_INFO_CLIENT);
        }

        private void UpdateRoomForClient()
        {
            if (gameUpdated != null)
            {
                new Thread(new ParameterizedThreadStart(
                    delegate(object room)
                    {
                        Thread.Sleep(1000);
                        try
                        {
                            Room r = (Room)room;
                            gameUpdated(r);
                        }
                        catch
                        {
                            Console.WriteLine("[server] error when callback gameupdated");
                        }
                        /*   
                       foreach(Player p in r.ListPlayers)
                       {
                           try
                           {
                           p.Callback.RoomUpdated(r);
                           }
                           catch 
                           {
                               ListPlayerDisconnectDuringParty.Add(p);
                               Console.WriteLine("[server] error when room updated WPF client");
                           }
                       }   
                         * */
                    })
                ).Start(this);
            }

        }

        public bool PlayCard(string playerToken, CardColor color, CardValue value)
        {
            bool isSuccess = gameInfo.PlayCard(playerToken, color, value);

            int nbPlayerFinish = 0;
            foreach (Player p in listPlayers)
            {
                if (p.ListCardsHolding.Count <= 0)
                    nbPlayerFinish++;
            }

            if (nbPlayerFinish >= NB_PLAYER_PER_GAME)
            {
                EndOfGame();
            }

            CreateGameInfoClient(TIME_SEND_GAME_INFO_CLIENT);

            UpdateRoomForClient();

            return isSuccess;
        }

        private void EndOfGame()
        {

            Console.WriteLine("[server] End of Game");

            RoomState = StateRoom.END_GAME;

            //Calculate the score for the round
            foreach (Player p in listPlayers)
            {
                foreach (Card c in p.ListCardsWin)
                {
                    p.ScoreParty += c.RealValue;
                }
            }

            List<Player> listCopy = new List<Player>(listPlayers);

            listCopy.Sort(delegate(Player x, Player y)
            {
                return x.ScoreParty.CompareTo(y.ScoreParty);
            });

            int i = 4;
            //Calculate the final score for the round
            foreach (Player p in listCopy)
            {
                p.Score += i;
                i /= 2;
                Console.WriteLine("[server] Player " + p.Token + " make a score of " + p.Score);
            }
        }

        private void CreateGameInfoClient(int timeInMillis)
        {
            gameInfo.CreateGameInfoClient();

            new Thread(new ParameterizedThreadStart(
                delegate(object dictGameInfo)
                {
                    Thread.Sleep(timeInMillis);

                    Dictionary<string, GameInfo> dict = dictGameInfo as Dictionary<string, GameInfo>;

                    callGameInfoClient(dict);

                })
            ).Start(gameInfo.DictGameInfoPlayer);
        }

        private void callGameInfoClient(Dictionary<string, GameInfo> dict)
        {
            if (dict != null)
            {
                try
                {
                    foreach (GameInfo game in dict.Values)
                    {
                        ISuecaCallbackContract callback = game.Player.Callback;
                        if (callback != null)
                        {
                            //Check is a wpf client is disconnect during party
                            try
                            {
                                callback.GameInfoUpdated(game);
                            }
                            catch
                            {
                                //ListPlayerDisconnectDuringParty.Add(game.Player);
                            }
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("[server] error when send callback gameinfo");
                }

            }
        }

        public GameInfo getGameInfoForPlayerToken(string playerToken)
        {
            GameInfo gameInfoClient;
            try
            {
                if (gameInfo.DictGameInfoPlayer.TryGetValue(playerToken, out gameInfoClient))
                {
                    gameInfoClient.Player.TimeOutClientWeb = DateTime.Now;
                    return gameInfoClient;
                }
            }
            catch
            {
                return null;
            }

            return null;
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
