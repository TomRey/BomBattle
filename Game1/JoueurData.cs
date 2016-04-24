using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class JoueurData
    {
        public string Score { get; set; }
        public string Vie { get; set; }
        public string Bonus { get; set; }
        public string Pseudo { get; set; }
        public Microsoft.Xna.Framework.Input.Keys Key { get; set; }
        public Vector2 pseudoPosition;
        public string Finish { get; set; }

        public JoueurData(string pseudo, int id, SpriteFont font, Microsoft.Xna.Framework.Input.Keys key)
        {
            Score = "0";
            Vie = "0";
            Bonus = "0";
            Pseudo = pseudo;
            pseudoPosition = new Vector2(id * 110 + (250 - font.MeasureString(Pseudo).X / 2), 50);
            Key = key;
            Finish = "False";
        }

        public void setData(string score, string vie, string bonus, string gameOver)
        {
            Score = score;
            Vie = vie;
            Bonus = bonus;
            Finish = gameOver;
        }
    }
}
