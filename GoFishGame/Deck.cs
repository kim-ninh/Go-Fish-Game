using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFishGame
{
    class Deck
    {
        private List<Card> cards;
        private Random random;

        public Deck()
        {
            cards = new List<Card>();
            for (Suites suite = Suites.Spades; suite <= Suites.Hearts; suite++)
            {
                for (Values value = Values.Aces; value <= Values.King; value++)
                    cards.Add(new Card(suite, value));
            }
        }

        public Deck(IEnumerable<Card> initialCards)
        {
            cards = new List<Card>(initialCards);
        }

        public int Count { get { return cards.Count; } }

        public void Add(Card cardToAdd)
        {
            cards.Add(cardToAdd);
        }

        public Card Deal(int index)
        {
            Card CardToDeal = cards[index];
            cards.RemoveAt(index);
            return CardToDeal;
        }

        public IEnumerable<string> GetCardNames()
        {
            string[] cardNames = new string[this.Count];
            for (int i = 0; i < cardNames.Length; i++)
            {
                cardNames[i] = cards[i].ToString();
            }

            return cardNames;
        }

        public void Shuffle()
        {
            random = new Random();
            for (int i = 0; i < Count; i++)
            {
                int randomIndex = random.Next(Count);
                cards.Add(this.Deal(randomIndex));
            }
            
        }

        public void Sort()
        {
            cards.Sort(new CardComparer_bySuit());
        }

        public Card Peek(int cardNumber)
        {
            return cards[cardNumber];
        }

        public Card Deal()
        {
            return Deal(0);
        }

        public bool ContainsValue(Values value)
        {
            foreach (Card card in cards)
            {
                if (card.Value == value)
                    return true;
            }
            return false;
        }

        public Deck PullOutValue(Values value)
        {
            Deck deckToReturn = new Deck(new Card[] { });
            for (int i = cards.Count - 1; i >= 0; i--)
                if (cards[i].Value == value)
                    deckToReturn.Add(Deal(i));
            return deckToReturn;
        }

        public bool HasBook(Values value)
        {
            int numberOfCard = 0;
            foreach (Card card in cards)
            {
                if (card.Value == value)
                    numberOfCard++;
            }

            if (numberOfCard == 4)
                return true;
            return false;
        }

        public void SortByValue()
        {
            cards.Sort(new CardComparer_byValue());
        }
    }
}
