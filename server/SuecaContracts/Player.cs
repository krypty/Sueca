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
        public int NumberTurn { get; set; }

        [DataMember]
        string token;

        [DataMember]
        bool isReady;

        [DataMember]
        public int HoldingCards{get;set;}

        [DataMember]
        public int TakenCards { get; set; }

        [DataMember]
        public int Score{get;set;}

        public int ScoreParty { get; set; }

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
            TakenCards = listCardsWin.Count;
            }
        }
        internal List<Card> ListCardsHolding
        {
            get { return listCardsHolding; }
            set { listCardsHolding = value;
            HoldingCards = listCardsHolding.Count;
            }
        }

        public DateTime? TimeOutClientWeb {get;set;}

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

        public Player(Player player) : this(player.token)
        {
            this.Score = player.Score;
            this.isReady = player.isReady;
            this.ListCardsHolding = player.listCardsHolding.ToList();
            this.ListCardsWin = player.listCardsWin.ToList();
            this.Callback = player.Callback;
            this.TimeOutClientWeb = player.TimeOutClientWeb;
            this.NumberTurn = player.NumberTurn;
        }

    }
}
