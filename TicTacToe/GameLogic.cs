using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class GameLogic
    {
        public string CurrentPlayer { get; set; } = X;
        private const string X = "X";
        private const string O = "O";
        private string[,] board = new string[3, 3];

        public void SetNextPlayer()
        {
            if(CurrentPlayer == X)
            {
                CurrentPlayer = O;
            }
            else
            {
                CurrentPlayer = X;
            }
        }

        public bool PlayerWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (!String.IsNullOrWhiteSpace(board[i, 0]))
                {
                    if (board[i, 0] == board[i, 1] && board[i, 0] == board[i, 2]) return true;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (!String.IsNullOrWhiteSpace(board[0, i]))
                {
                    if (board[0, i] == board[1, i] && board[0, i] == board[2, i]) return true;
                }
            }

            if (!String.IsNullOrWhiteSpace(board[1, 1]))
            {
                if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) return true;
                if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]) return true;
            }

            return false;
        }

        internal void UpdateBoard(Position pos, string curr)
        {
            board[pos.x, pos.y] = curr;
        }
    }
}
