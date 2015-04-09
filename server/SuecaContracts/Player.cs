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

        public Player()
        {
            this.isReady = false;
        }

        public Player(string playerToken) : this()
        {
            this.token = playerToken;
        }

    }
}
