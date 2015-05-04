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
        public bool IsMyTurn { get; set; }

        [DataMember]
        public LinkedList<Card> ListCardsPlayed { get; set; }

        [DataMember]
        public List<Card> ListCardsPlayer{get;set;}

        [DataMember]
        public LinkedList<Player> ListPlayer { get; set; }

        [DataMember]
        public int FirstPlayerNumber { get; set; }

        public GameInfo(Player player, LinkedList<Player> listPlayers, LinkedList<Card> listCards, bool isMyTurn) 
        {
            //User for who the gameinfo is
            Player = player;
            ListCardsPlayer = player.ListCardsHolding;

            //List of other players
            ListPlayer = listPlayers;
            ListCardsPlayed = listCards;

            IsMyTurn = isMyTurn;
        }

    }
}
