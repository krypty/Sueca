using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SuecaContracts
{
    [DataContract]
    public class GameInfo
    {
        public Player Player { get; set; }
        [DataMember]
        public int NumberPlayerToPlay { get; set; }
        [DataMember]
        public LinkedList<Card> ListCardsPlayed { get; set; }
        [DataMember]
        public int NumberCardsWin { get; set; }

        public GameInfo(Player player, List<Player> listPlayers) 
        {
            Player = player;
            ListCardsPlayed = new LinkedList<Card>();
            
            //NumberCardsWin = numberCardsWin;
            //this.NumberPlayerToPlay = numberPlayerToPlay;
        }
    }
}
