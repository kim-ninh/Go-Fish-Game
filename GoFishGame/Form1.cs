using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GoFishGame
{
    public partial class Form1 : Form
    {
        private Game game;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textName.Text))
            {
                MessageBox.Show("Please enter your name", "Can't start the game yet");
                return;
            }

            game = new Game(textName.Text, new List<String> { "Eve", "Wall-E" }, textProgress);
            textName.Enabled = false;
            buttonStart.Enabled = false;
            buttonAsk.Enabled = true;
            UpdateForm();
        }

        private void UpdateForm()
        {
            listHand.Items.Clear();
            foreach (string cardName in game.GetPlayerCardNames())
                listHand.Items.Add(cardName);

            textBooks.Text = game.DescribeBooks();
            textProgress.Text += game.DescribePlayerHand();
            textProgress.SelectionStart = textProgress.Text.Length;
            textProgress.ScrollToCaret();
            
        }

        private void buttonAsk_Click(object sender, EventArgs e)
        {
            textProgress.Text = "";
            if (listHand.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a card");
                return;
            }

            if (game.PlayOneRound(listHand.SelectedIndex))
            {
                textProgress.Text += "The winner is... " + game.GetWinnerName();
                textBooks.Text = game.DescribeBooks();
                buttonAsk.Enabled = false;
            }
            else
                UpdateForm();
        }
    }
}
