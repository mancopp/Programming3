using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    public class LeftOverTile
    {
        public int pos;
        public int target;
        public string direction;
        public bool isLast;
        public int num;

        public LeftOverTile(int pos, int target, string direction, bool isLast, int num)
        {
            this.pos = pos;
            this.target = target;
            this.direction = direction;
            this.isLast = isLast;
            this.num = num;
        }
    }
}
