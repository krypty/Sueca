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

        [DataMember]
        public int Score{get;set;}

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
            set { listCardsWin = value;
            takenCards = listCardsWin.Count;
            }
        }
        internal List<Card> ListCardsHolding
        {
            get { return listCardsHolding; }
            set { listCardsHolding = value;
            holdingCards = listCardsHolding.Count;
            }
        }

        public DateTime? TimeOutClientWeb {get;set;}

        [DataMember]
        public int NumberTurn
        {
            get { return numberTurn; }
            set { numberTurn = value; }
        }

        public Player()
        {
            this.Score = 0;
            this.IsReady = false;
            this.ListCardsHolding = new List<Card>();
            this.ListCardsWin = new List<Card>();
            this.Callback = null;
            this.TimeOutClientWeb = null;
        }

        public Player(string playerToken) : this()
        {
            this.token = playerToken;
        }

    }
}
