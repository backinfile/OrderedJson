using OrderedJson.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OJApp
{
    public class GameContext: OJContext
    {
        public Card hostCard;
        public Card cursor;
    }

    public class Card
    {
        public IOJMethod TurnStart;
        public int health;
    }
}
