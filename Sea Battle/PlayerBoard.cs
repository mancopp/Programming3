using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    public class PlayerBoard
    {
        private bool[,] boolBoard;

        public PlayerBoard()
        {
            this.boolBoard = new bool[10, 10];
        }

        public PlayerBoard(bool[,] matrix)
        {
            this.boolBoard = matrix;
        }

    }
}
