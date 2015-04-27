using System.Collections.Generic;

namespace SuecaContracts
{
    class GameInfo
    {
        public string PlayerToken { get; set; }
        public int NumberPlayerToPlay { get; set; }
        public LinkedList<Card> ListCardsPlayed { get; set; }
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
