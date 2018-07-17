using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GoFishGame
{
    class Game
    {
        private List<Player> players;
        private Dictionary<Values, Player> books;
        private Deck stock;
        private TextBox textBoxOnForm;

        public Game(string playerName, IEnumerable<string> oppenentNames, TextBox textBoxOnForm)
        {
            Random random = new Random();
            this.textBoxOnForm = textBoxOnForm;
            players = new List<Player>();
            players.Add(new Player(playerName, random, textBoxOnForm));
            foreach (string player in oppenentNames)
            {
                players.Add(new Player(player, random, textBoxOnForm));
            }
            books = new Dictionary<Values, Player>();
            stock = new Deck();
            Deal();
            players[0].SortHand();
        }

        private void Deal()
        {
            stock.Shuffle();
            foreach (Player player in players)
            {
                for (int i = 1; i <= 5; i++)
                    player.TakeCard(stock.Deal());
            }

            foreach (Player player in players)
            {
                List<Values> playerBook = (player.PullOutBooks() as List<Values>);
                foreach (Values value in playerBook)
                    books.Add(value, player);

            }
        }

        public bool PlayOneRound(int selectedPlayerCard)
        {
            
            for (int i = 0; i < players.Count; i++)
            {
                if (i == 0)
                    players[i].AskForACard(players, i, stock, (Values)selectedPlayerCard);
                else
                    players[i].AskForACard(players, i, stock);

                if (PullOutBooks(players[i]) && stock.Count != 0)
                    players[i].TakeCard(stock.Deal());
            }

            if (stock.Count == 0)
            {
                this.textBoxOnForm.Text = "The stock is out of cards. Game over!";
                return true;
            }
            return false;
        }

        public bool PullOutBooks(Player player)
        {
            List<Values> playerBook =(List<Values>) player.PullOutBooks();

            foreach (Values value in playerBook)
                books.Add(value, player);

            if (player.CardCount == 0)
                return true;
            return false;
        }

        public string DescribeBooks()
        {
            string description = "";
            for (int i = 0; i < books.Count; i++)
                description += String.Format("{0} has a book of {1}.\r\n", books.ElementAt(i).Value, books.ElementAt(i).Key);
            return description;
        }

        public string GetWinnerName()
        {
            Dictionary<string, int> winners = new Dictionary<string, int>();
            
            foreach (Values value in books.Keys)
            {
                string playerName = books[value].Name;
                if (winners.ContainsKey(playerName))
                    winners[playerName]++;
                else
                    winners.Add(playerName, 1);
            }

            int maxBook = 0;
            for (int i = 0; i < players.Count; i++)
                if (winners[players[i].Name] > maxBook)
                    maxBook = winners[players[i].Name];

            List<String> winnersName = new List<string>();
            foreach (string player in winners.Keys)
                if (winners[player] == maxBook)
                    winnersName.Add(player);

            if (winnersName.Count == 1)
                return String.Format("{0} with {1} books", winnersName[0], maxBook);
            return String.Format("A tie between {0} and {1} with {2} books.", winnersName[0], winnersName[1], maxBook);
        }

        public IEnumerable<string> GetPlayerCardNames()
        {
            return players[0].GetCardName();
        }

        public string DescribePlayerHand()
        {
            string description = "";
            foreach (Player player in players)
            {
                description += String.Format("{0} has {1} {2}\r\n", player.Name, player.CardCount, player.CardCount == 1 ? "card." : "cards.");
            }
            description += String.Format("The stock has {0} cards left.", stock.Count);
            return description;
        }
    }
}
