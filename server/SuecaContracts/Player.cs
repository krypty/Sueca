using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SuecaContracts
{
    [DataContract]
    public class Player
    {
        private List<Card> listCardsHolding;
        private List<Card> listCardsWin;
        private int numberTurn;

        [DataMember]
        string token;

        [DataMember]
        bool isReady;

        [DataMember]
        int holdingCards;

        [DataMember]
        int takenCards;

        ISuecaCallbackContract callback;
        public string Token
        {
            get { return token; }
            set { token = value; }
        }
        public bool IsReady
        {
            get { return isReady; }
            set { isReady = value; }
        }
        public ISuecaCallbackContract Callback
        {
            get { return callback; }
            set { callback = value; }
        }
        internal List<Card> ListCardsWin
        {
            get { return listCardsWin; }
            set { listCardsWin = value; }
        }
        internal List<Card> ListCardsHolding
        {
            get { return listCardsHolding; }
            set { listCardsHolding = value; }
        }
        public int NumberTurn
        {
            get { return numberTurn; }
            set { numberTurn = value; }
        }

        public Player()
        {
            this.isReady = false;
            this.ListCardsHolding = new List<Card>();
            this.ListCardsWin = new List<Card>();
        }

        public Player(string playerToken) : this()
        {
            this.token = playerToken;
        }

    }
}
