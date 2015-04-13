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

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Password { get; set; }

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
        }

        public void AddPlayer(Player player)
        {
            listPlayers.Add(player);
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

            if(isReady)
            {
                nbPlayersReady++;

                if (nbPlayersReady == 2)
                {
                    foreach (Player p in ListPlayers)
                    {
                        gameStarted += p.Callback.GameStarted;
                    }

                    new Thread(new ParameterizedThreadStart(
                        delegate(object message)
                        {
                            gameStarted((string)message);
                        })
                        ).Start("Game started");
                }
            }
            else
            {
                nbPlayersReady--;
            }
        }

        private void callGameStarted(object m)
        {
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
