///Jawad Taj 2021-05-1
///TicTacToe Game
///You can play with an ai or play with your friend.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToeV3
{
    public partial class TicTacToeGame : Form
    {
        public TicTacToeGame()
        {
            InitializeComponent();
        }

        bool ai = true;
        bool turn = true; // true = blue // false = red
        string player = "Blue";
        string player2 = "Red";
        int player1Score = 0;
        int player2Score = 0;
        int tieScore = 0;
        int time = 0;
        bool counting = true;
        Button[] tilesButton;
        List<int> winningRow = new List<int>();

        //This is use for the TicTacToe Ai
        List<List<string>> board = new List<List<string>>
        {
            new List<string> { "","","", },
            new List<string> { "","","", },
            new List<string> { "","","", }
        };

        private void TicTacToeGame_Load_1(object sender, EventArgs e)
        {
            tilesButton = new Button[] { btnTiles0, btnTiles1, btnTiles2, btnTiles3, btnTiles4, btnTiles5, btnTiles6, btnTiles7, btnTiles8 };
            TimeAsync();
        }


        /// <summary>
        /// Adds a timer to the tictactoe game
        /// </summary>
        private async Task TimeAsync()
        {
            await Task.Run(() => {
                //if code is done, the funchion will stop running.
                //This while loop will keep the program checking if counting is true
                while (true)
                {
                    while(counting)
                    {
                        TimeSpan strTime = TimeSpan.FromSeconds(time);
                        string timeWithoutHours = string.Format("{0:D2}:{1:D2}", (int)strTime.TotalMinutes, strTime.Seconds); //Removes the hours 

                        System.Threading.Thread.Sleep(1000);
                        time++;
                        lblTime.Text = $"Time: {timeWithoutHours}";
                    }
                }
            });
        }


        /// <summary>
        /// Changes the game from single player to 2 player game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picPlayer_Click(object sender, EventArgs e)
        {
            if (!ai)
            {
                picPlayer.Image = Image.FromFile("../Pics/Single.jpg");
                lblPlayer1.Text = "Player";
                lblPlayer2.Text = "Ai";
            }
            else
            {
                picPlayer.Image = Image.FromFile("../Pics/2 player.jpg");
                lblPlayer1.Text = "Blue";
                lblPlayer2.Text = "Red";
            }
            ai = !ai;
            Restart();  //restarts to avoid the two games mixing
        }




        /// <summary>
        /// turns a number to a 3x3 board instrustions. Ex: 4 => 1, 1
        /// Ex:
        /// </summary>
        /// <param name="index"></param>
        /// <returns> Tuple with 2 int value used for the board </returns>
        private Tuple<int, int> TilesIndex(int index)
        {
            if (index < 3) { return Tuple.Create(0, index); }
            else if (index < 6) { return Tuple.Create(1, index - 3); }
            else { return Tuple.Create(2, index - 6); }
        }


        /// <summary>
        /// turns a 3x3 board instrustions to a number. Ex: 1, 1 => 4
        /// </summary>
        /// <param name="i"></param>
        /// <param name="x"></param>
        /// <returns> Integer </returns>
        public int UnTilesIndex(int i, int x)
        {
            if (i == 0) { return x; }
            else if (i == 1) { return x + 3; }
            else { return x + 6; }
        }


        /// <summary>
        /// Changes color of tiles and adds it to a board list(used in the Minimax Algorithm)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TicTacToe(object sender, EventArgs e)
        {
            Button tiles = (Button)sender;
            Tuple<int, int> index = TilesIndex(tiles.TabIndex);

            if (turn)
            {
                tiles.BackColor = Color.Blue;
                board[index.Item1][index.Item2] = player;
                lblTurn.Text = "Red Turn";
                lblTurn.ForeColor = Color.Red;
            }
            else
            {
                tiles.BackColor = Color.Red;
                board[index.Item1][index.Item2] = player2;
                lblTurn.Text = "Blue Turn";
                lblTurn.ForeColor = Color.Blue;
            }

            tiles.Enabled = false;

            if (ai)
            {
                if (CheckWinner() != "tie")
                {
                    TicTacToeAi ai = new TicTacToeAi();
                    int move = ai.BestMove(board);
                    Tuple<int, int> aiIndex;

                    tilesButton[move].BackColor = Color.Red;
                    aiIndex = TilesIndex(move);
                    board[aiIndex.Item1][aiIndex.Item2] = player2;
                    lblTurn.Text = "Blue Turn";
                    lblTurn.ForeColor = Color.Blue;
                }
            }
            else
            {
                turn = !turn;
            }

            IsThereAWinner();
        }



        /// <summary>
        /// checks if all three parameter are the same and if they have a value
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns> true if all are the same and have a value. Anything else will return false </returns>
        private bool Equal3(string a, string b, string c)
        {
            return a != "" && a == b && b == c;
        }



        /// <summary>
        /// Checks who is the winner and if the game is a tie
        /// </summary>
        /// <param name="board"></param>
        /// <returns> The Winner name or the word tie, if there is no winner or game is not a tie then null is return </returns>
        private string CheckWinner()
        {
            string winner = null;
            //checks who the winners are
            for (int i = 0; i < 3; i++)
            {
                // horizontal
                if (Equal3(board[i][0], board[i][1], board[i][2]))
                {
                    winner = board[i][0];
                    winningRow = new List<int>() { UnTilesIndex(i, 0), UnTilesIndex(i, 2), UnTilesIndex(i, 2)};
                }
                // Vertical
                else if (Equal3(board[0][i], board[1][i], board[2][i]))
                {
                    winner = board[0][i];
                    winningRow = new List<int>() { UnTilesIndex(0, i), UnTilesIndex(1, i), UnTilesIndex(2, i)};
                }
            }

            // Diagonal
            if (Equal3(board[0][0], board[1][1], board[2][2]))
            {
                winner = board[0][0];
                winningRow = new List<int>() { UnTilesIndex(0, 0), UnTilesIndex(1, 1), UnTilesIndex(2, 2)};
            }
            else if (Equal3(board[2][0], board[1][1], board[0][2]))
            {
                winner = board[2][0];
                winningRow = new List<int>() { UnTilesIndex(2, 0), UnTilesIndex(1, 1), UnTilesIndex(0, 2)};
            }

            int openSpots = 0;
            //checks how many open spots there are
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i][j] == "")
                    {
                        openSpots++;
                    }
                }
            }

            //checks if it's a tie
            if (winner == null && openSpots == 0)
            {
                return "tie";
            }
            else
            {
                return winner;
            }
        }



        /// <summary>
        /// Displays a message based on who wins
        /// Ask user if they what to restart or leave
        /// </summary>
        private void IsThereAWinner()
        {
            string winner = CheckWinner();
            if (winner != null)
            {
                TimeSpan strTime = TimeSpan.FromSeconds(time);
                string timeWithoutHours = string.Format("{0:D2}:{1:D2}", (int)strTime.TotalMinutes, strTime.Seconds); //Removes the hours 
                counting = false;

                if (winner == "tie")
                {
                    tieScore++;
                    lblTieScore.Text = tieScore.ToString();
                    MessageBox.Show("The Game Is A Tie");
                }
                else if (winner == "Blue")
                {
                    //sets the row that won to green
                    for (int i = 0; i < 3; i++){tilesButton[winningRow[i]].BackColor = Color.Green; player1Score++;}
                    lblPlayer1Score.Text = player1Score.ToString();
                    MessageBox.Show("Congrats! Blue. You Won The Game");

                }
                else
                {
                    //sets the row that won to green
                    for (int i = 0; i < 3; i++) { tilesButton[winningRow[i]].BackColor = Color.Green; }
                    player2Score++;
                    lblPlayer2Score.Text = player2Score.ToString();
                    MessageBox.Show("Congrats! Red. You Won The Game");
                }

                DialogResult result = MessageBox.Show($"Do You What To Play Again Or Exit The Game?\nThat Game Took {timeWithoutHours}", "Game Over", MessageBoxButtons.RetryCancel);

                if (result == DialogResult.Retry)
                {
                    Restart();
                }
                else
                {
                    Application.Exit();
                }
            }
        }


        /// <summary>
        /// Restarts the game to a playable state
        /// </summary>
        private void Restart()
        { 
            for (int i = 0; i < 9; i++)
            {
                tilesButton[i].BackColor = default(Color);
                tilesButton[i].UseVisualStyleBackColor = true;  //this auto set to false when backcolor is change
                tilesButton[i].Enabled = true;
            }

            winningRow.Clear();
            time = 0;
            counting = true;
            board = new List<List<string>>
        {
            new List<string> { "","","", },
            new List<string> { "","","", },
            new List<string> { "","","", }
        };
        }

    }
}