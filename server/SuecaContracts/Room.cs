using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SuecaContracts
{
    [DataContract]
    public class Room
    {
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
