using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuecaContracts
{
    public enum CardColor { None, Spades, Diamonds, Clubs, Hearts };
    public enum CardValue { Two, Three, Four, Five, Six, Seven, Jack, Queen, King, Ace };

    class Card
    {
        private CardColor color;
        private CardValue value;

        public Card(CardColor color, CardValue value)
        {
            this.color = color;
            this.value = value;
        }

    }
}
