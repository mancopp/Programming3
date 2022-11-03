using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    public class GameLogic
    {
        private PlayerBoard pb1;
        private PlayerBoard pb2;

        private bool[,] defaultLayout = new bool[,] {
            { false, false, false, true, true, true, true, false, false, false },
            { false, false, false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false, false, false },
            { true, true, true, false, false, false, false, true, true, true },
            { false, false, false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false, false, false },
            { true, true, false, false, true, true, false, false, true, true },
            { false, false, false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false, false, false },
            { true, false, false, true, false, false, true, false, false, true }
            };

        public void setPlayerBoardToDefaultLayout(int player)
        {
            if(player == 1)
            {
                pb1 = new PlayerBoard(defaultLayout);
            }
            else if(player == 2)
            {
                pb2 = new PlayerBoard(defaultLayout);
            }
        }
    }
}
