using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SuecaContracts
{
    [DataContract]
    public class GameInfo
    {
        [DataMember]
        public Player Player { get; set; }

        [DataMember]
        public int NumberPlayerToPlay { get; set; }

        [DataMember]
        public LinkedList<Card> ListCardsPlayed { get; set; }

        [DataMember]
        public List<Player> ListPlayer { get; set; }

        [DataMember]
        public Card[] tabCards;

        [DataMember]
        public Card FirstCard {get;set;}

        public GameInfo(Player player, List<Player> listPlayers, int numberPlayerToPlay) 
        {
            //User for who the gameinfo is
            Player = player;

            //List of other players
            ListPlayer = listPlayers;
            ListPlayer.Remove(Player);

            NumberPlayerToPlay = numberPlayerToPlay;
        }

    }
}
