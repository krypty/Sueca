using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SuecaContracts
{
    public enum CardColor { None, Spades, Diamonds, Clubs, Hearts };
    public enum CardValue { None, Two, Three, Four, Five, Six, Seven, Jack, Queen, King, Ace };

    [DataContract]
    public class Card
    {
        private CardColor color;
        private CardValue value;
        private int realValue;

        public int RealValue
        {
            get { return realValue; }
            set { realValue = value; }
        }
        [DataMember]
        public CardColor Color
        {
            get { return color; }
            set { color = value; }
        }
        [DataMember]
        public CardValue Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public Card(CardColor color, CardValue value)
        {
            this.color = color;
            this.value = value;

            switch(value)
            {
                case CardValue.Ace:
                    realValue = 11;
                    break;

                case CardValue.Seven:
                    realValue = 10;
                    break;

                case CardValue.King:
                    realValue = 4;
                    break;

                case CardValue.Jack:
                    realValue = 3;
                    break;

                case CardValue.Queen:
                    realValue = 2;
                    break;

                default:
                    realValue = 0;
                    break;
            }
        }

    }
}
