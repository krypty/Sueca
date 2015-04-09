using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuecaContracts
{
    class Player
    {
        string token;
        int holdingCards;
        int takenCards;

        public Player()
        {

        }

        public Player(string playerToken) : this()
        {
            this.token = playerToken;
        }

    }
}
