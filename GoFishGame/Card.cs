using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFishGame
{
    class Card
    {
        public Card(Suites suit, Values value)
        {
            this.Suit = suit;
            this.Value = value;
        }
        public Suites Suit { get; set; }
        public Values Value { get; set; }
        public string Name
        {
            get
            {
                return Value.ToString() + " of " + Suit.ToString();
            }
        }
        public override string ToString()
        {
            return Name;
        }

        public static string Plural(Values value)
        {
            if (value == Values.Six)
                return "Sixes";
            return value.ToString() + "s";
        }
    }
}
