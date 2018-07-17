using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GoFishGame
{
    class Player
    {
        private string name;
        public string Name { get { return name;} }

        private Random random;
        private Deck cards;
        private TextBox textBoxOnForm;

        public Player(string name, Random random, TextBox textBoxOnForm)
        {
            this.name = name;
            this.random = random;
            this.textBoxOnForm = textBoxOnForm;

            textBoxOnForm.Text += Name + " has just joined the game.\r\n";
        }

        public IEnumerable<Values> PullOutBooks()
        {
            List<Values> books = new List<Values>();
            for (int i = 1; i<=13; i++)
            {
                Values value = (Values)i;
                int howMany = 0;
                for (int card = 0; card < cards.Count; card++)
                    if (cards.Peek(card).Value == value)
                        howMany++;

                if (howMany == 4)
                {
                    books.Add(value);
                    cards.PullOutValue(value);
                }
            }
            return books;
        }

        public Values GetRandomValue()
        {
            int randomIndex = random.Next(cards.Count);
            return cards.Peek(randomIndex).Value;
        }

        public Deck DoYouHaveAny(Values value)
        {
            Deck deckWithValue = cards.PullOutValue(value);

            textBoxOnForm.Text += String.Format("{0} has {1} {2}.\r\n", Name, deckWithValue.Count, Card.Plural(value));
            return deckWithValue;
        }

        public void AskForACard(List<Player> players, int myIndex, Deck stock)
        {
            AskForACard(players, myIndex, stock, this.GetRandomValue());
        }
        
        public void AskForACard(List<Player> players, int myIndex, Deck stock, Values value)
        {
            textBoxOnForm.Text += String.Format("{0} asks if anyone has a {1}.\r\n", this.Name, value);
            int howManyCardAdded = 0;
            Deck deckWithValue;
            for (int i = 0; i < players.Count;i++)
            {
                if (i == myIndex)
                    continue;

                deckWithValue = players[i].DoYouHaveAny(value);

                howManyCardAdded += deckWithValue.Count;
                for (int j = 0; j < deckWithValue.Count; j++)
                    this.cards.Add(deckWithValue.Peek(j));
            }

            if (howManyCardAdded == 0)
            {
                this.cards.Add(stock.Deal());
                textBoxOnForm.Text += (this.Name + " had to draw from the stock.\r\n");
            }
        }

        public int CardCount { get { return cards.Count; } }
        public void TakeCard(Card card) { cards.Add(card); }
        public IEnumerable<string> GetCardName() { return cards.GetCardNames(); }
        public Card Peek(int cardNumber) { return cards.Peek(cardNumber);}
        public void SortHand() { cards.SortByValue(); }

    }
}
