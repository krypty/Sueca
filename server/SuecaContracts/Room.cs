﻿using System;
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

        public bool IsPlayerDisconnectDuringParty {get;set;}
        public Player PlayerDisconnectDuringParty { get; set; }

        private const int CHECK_WEB_CLIENT_TIME = 30000;
        private const int NB_PLAYER_PER_GAME = 4;
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

            ResetRoom();
        }

        public void ResetRoom()
        {
            RoomState = StateRoom.WAITING_READY;
            IsPlayerDisconnectDuringParty = false;
            gameInfo = null;

            foreach(Player p in listPlayers)
            {
                p.IsReady = false;
            }
        }

        public void AddPlayer(Player player)
        {
            //Add a number to known when the user comes
            //First : 0, second : 1, third : 2, fourth : 3
            player.NumberTurn = listPlayers.Count;

            listPlayers.Add(player);

            //Add the new player callback to the delegate
            if(player.Callback != null)
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
            else if(!isReady)
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

            CreateGameInfoClient();
             
        }

        void timer_Elapsed(Room room)
        {
            foreach(Player p in room.ListPlayers)
            {
                if(p.TimeOutClientWeb.HasValue)
                {
                    Console.WriteLine("[server] check web client player " + p.Token);
                    if (DateTime.Now.Subtract(p.TimeOutClientWeb.Value).Milliseconds > 10000)
                    {
                        //A web client is deconnect
                        IsPlayerDisconnectDuringParty = true;
                        PlayerDisconnectDuringParty = p;
                    }
                }
            }

            Console.WriteLine("[server] check web client");
        }

        private void UpdateRoomForClient()
        {
            //new System.Threading.Timer(obj => { gameUpdated(this); }, null, 1000, System.Threading.Timeout.Infinite);
            if(gameUpdated != null)
            {
                new Thread(new ParameterizedThreadStart(
                    delegate(object room)
                    {
                        Thread.Sleep(1000);
                        gameUpdated((Room)room);
                    })
                ).Start(this);
            }
            
        }

        public bool PlayCard(string playerToken, CardColor color, CardValue value)
        {
            bool isSuccess = gameInfo.PlayCard(playerToken, color, value);
            
            if (listPlayers.Count<Player>(p => p.ListCardsHolding.Count > 0) <= 0)
            {
                EndOfGame();
            }

            CreateGameInfoClient();

            return isSuccess;
        }

        private void EndOfGame()
        {
            RoomState = StateRoom.END_GAME;

            //Calculate the score for the round
            foreach(Player p in listPlayers)
            {
                foreach(Card c in p.ListCardsWin)
                {
                    p.ScoreParty += c.RealValue;
                }
            }

            //Calculate the final score for the round
            foreach(Player p in listPlayers)
            {
                if(p.ScoreParty > 60 & p.ScoreParty <= 89)
                {
                    p.Score += 1;
                }
                else if(p.ScoreParty >= 90 & p.ScoreParty < 120)
                {
                    p.Score += 2;
                }
                else if(p.ScoreParty >= 120)
                {
                    p.Score += 4;
                }
            }

            //ResetRoom();
        }

        private void CreateGameInfoClient()
        {
            gameInfo.CreateGameInfoClient();

            new Thread(new ParameterizedThreadStart(
                delegate(object dictGameInfo)
                {
                    Thread.Sleep(1000);

                    Dictionary<string, GameInfo> dict = dictGameInfo as Dictionary<string, GameInfo>;

                    callGameInfoClient(dict);

                })
            ).Start(gameInfo.DictGameInfoPlayer);
        }

        private void callGameInfoClient(Dictionary<string, GameInfo> dict)
        {
            if (dict != null)
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
                            PlayerDisconnectDuringParty = game.Player;
                        }
                    }
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
            catch(Exception e)
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
