using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardGame
{
    public partial class Form1 : Form
    {

        Image[] images = new Image[52];
        int[] cardValues = new int[52];
        int currentCard = 0;
        int computerScore;
        int playerScore;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int[] values = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10",
                "jack", "queen", "king", "ace" };
            string[] suits = { "diamonds", "hearts", "spades", "clubs" };

            string path = @"C:\temp\cardimages\";

            int currentIndex = 0;

            for (int i = 0; i < ranks.Length; i++)
            {
                for (int j = 0; j < suits.Length; j++)
                {
                    string filename = $"{path}{ranks[i]}_of_{suits[j]}.png";

                    images[currentIndex] = Image.FromFile(filename);
                    cardValues[currentIndex] = values[i];
                    currentIndex++;
                }
            }
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            currentCard = 0;

            Random random = new Random();
            for (int i = 0; i < cardValues.Length - 1; i++)
            {
                int r1 = random.Next(i, cardValues.Length);
                Image image = images[i];
                int value = cardValues[i];

                images[i] = images[r1];
                cardValues[i] = cardValues[r1];

                images[r1] = image;
                cardValues[r1] = value;
            }

            DrawButton.Enabled = true;
            NewGameButton.Enabled = false;

            computerScore = 0;
            playerScore = 0;
            ComputerScoreTextBox.Text = computerScore.ToString();
            PlayerScoreTextBox.Text = playerScore.ToString();
            ResultTextBox.Text = "Ready... Play!";
            ComputerPictureBox.Image = null;
            PlayerPictureBox.Image = null;
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            ComputerPictureBox.Image = images[currentCard];
            int computerCardValue = cardValues[currentCard];
            currentCard++;

            PlayerPictureBox.Image = images[currentCard];
            int playerCardValue = cardValues[currentCard];
            currentCard++;

            if (computerCardValue > playerCardValue)
            {
                ResultTextBox.Text = "Computer Wins";
                computerScore = computerScore + 1;
            }
            if (computerCardValue < playerCardValue)
            {
                ResultTextBox.Text = "Player Wins";
                playerScore = playerScore + 1;
            }
            if (computerCardValue == playerCardValue)
            {
                ResultTextBox.Text = "Tie";
            }

            ComputerScoreTextBox.Text = computerScore.ToString();
            PlayerScoreTextBox.Text = playerScore.ToString();


            if (currentCard == 52)
            {
                MessageBox.Show("The entire deck has been dealt!", "Game Over");
                NewGameButton.Enabled = true;
                DrawButton.Enabled = false;
            }
        }
        
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
