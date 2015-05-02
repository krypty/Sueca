using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SuecaContracts
{
    [DataContract]
    public class GameInfo
    {
        [DataMember]
        public string PlayerToken { get; set; }
        [DataMember]
        public int NumberPlayerToPlay { get; set; }
        [DataMember]
        public LinkedList<Card> ListCardsPlayed { get; set; }
        [DataMember]
        public int NumberCardsWin { get; set; }

        public GameInfo(string playerToken, int numberCardsWin, int numberPlayerToPlay) 
        {
            PlayerToken = playerToken;
            ListCardsPlayed = new LinkedList<Card>();
            NumberCardsWin = numberCardsWin;
            this.NumberPlayerToPlay = numberPlayerToPlay;
        }
    }
}
