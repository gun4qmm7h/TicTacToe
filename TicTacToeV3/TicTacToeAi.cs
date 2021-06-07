///Jawad Taj 2021-05-1
///Class for the TicTacToe Ai


using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToeV3
{
    class TicTacToeAi
    {
        TicTacToeGame ticTacToe = new TicTacToeGame();
        Random rad = new Random();


        /// <summary>
        /// Checks if both arguments are the same and if they have value
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns> True if both are same and have a value. Otherwise false </returns>
        private bool Equal2(string a, string b)
        {
            return a != "" && a == b;
        }


        /// <summary>
        /// Checks all winning combo to check on what to land on next
        /// </summary>
        /// <param name="board"></param>
        /// <returns>A number base on how the board is played. </returns>
        public int BestMove(List<List<string>> board)
        {
            for (int i = 0; i < 3; i++)
            {
                // horizontal
                if (Equal2(board[i][0], board[i][1]) && board[i][2] == "")
                {
                    return ticTacToe.UnTilesIndex(i, 2);
                }
                else if (Equal2(board[i][1], board[i][2]) && board[i][0] == "")
                {
                    return ticTacToe.UnTilesIndex(i, 0);
                }
                else if (Equal2(board[i][0], board[i][2]) && board[i][1] == "")
                {
                    return ticTacToe.UnTilesIndex(i, 1);
                }

                // Vertical
                else if (Equal2(board[0][i], board[1][i]) && board[2][i] == "")
                {
                    return ticTacToe.UnTilesIndex(2, i);
                }
                else if (Equal2(board[1][i], board[2][i]) && board[0][i] == "")
                {
                    return ticTacToe.UnTilesIndex(0, i);
                }
                else if (Equal2(board[2][i], board[0][i]) && board[1][i] == "")
                {
                    return ticTacToe.UnTilesIndex(1, i);
                }
            }
            // Diagonal
            if (Equal2(board[0][0], board[1][1]) && board[2][2] == "")
            {
                return ticTacToe.UnTilesIndex(2, 2);
            }
            else if (Equal2(board[2][2], board[1][1]) && board[0][0] == "")
            {
                return ticTacToe.UnTilesIndex(0, 0);
            }
            else if (Equal2(board[0][0], board[2][2]) && board[1][1] == "")
            {
                return ticTacToe.UnTilesIndex(1, 1);
            }


            else if (Equal2(board[2][0], board[1][1]) && board[0][2] == "")
            {
                return ticTacToe.UnTilesIndex(0, 2);
            }
            else if (Equal2(board[0][2], board[1][1]) && board[2][0] == "")
            {
                return ticTacToe.UnTilesIndex(2, 0);
            }
            else if (Equal2(board[0][2], board[2][0]) && board[1][1] == "")
            {
                return ticTacToe.UnTilesIndex(1, 1);
            }
            else
            {
                int randomNum;
                List<int> freeMoves = new List<int>();
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i][j] == "")
                        {
                            freeMoves.Add(ticTacToe.UnTilesIndex(i, j));
                        }
                    }
                }
                randomNum = rad.Next(freeMoves.Count());

                return freeMoves[randomNum];
            }
        }
    }
}
