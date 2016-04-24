using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class GameData
    {
        public int Canon { get; set; }
        public int Boule { get; set; }
        public int Direction { get; set; }
        public int W { get; set; }
        public int H { get; set; }

        public GameData(int idCanon, int idBoule, int direction, int w, int h)
        {
            Canon = idCanon;
            Boule = idBoule;
            Direction = direction;
            W = w;
            H = h;
        }
    }
}
