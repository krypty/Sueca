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
        private List<String> _listPlayers;

        public Room(string password = "")
        {
            Password = password;
            Name = GenerateName();

            _listPlayers = new List<string>();
        }

        public void AddPlayer(String name)
        {
            _listPlayers.Add(name);
        }

        public void RemovePlayer(String name)
        {
            _listPlayers.Remove(name);
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
