using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFishGame
{
    class CardComparer_bySuit : IComparer<Card>
    {
        public int Compare(Card x, Card y)
        {
            if (x.Suit < y.Suit)
                return -1;

            if (x.Suit > y.Suit)
                return 1;

            if (x.Value < y.Value)
                return -1;

            if (x.Value > y.Value)
                return 1;

            return 0;
        }
    }

    class CardComparer_byValue : IComparer<Card>
    {
        public int Compare(Card x, Card y)
        {
            if (x.Value < y.Value)
                return -1;

            if (x.Value > y.Value)
                return 1;

            if (x.Suit < y.Suit)
                return -1;

            if (x.Suit > y.Suit)
                return 1;
            return 0;
        }
    }
}
