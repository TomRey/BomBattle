using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class Canon
    {
        Texture2D t2DCanon;
        Rectangle[] tabRectCanon = new Rectangle[4];
        Vector2[] tabPosCanon = new Vector2[4];
        const int CANON_WIDTH = 60;
        const int CANON_HEIGHT = 60; 

        public Canon()
        {
            tabPosCanon[0] = new Vector2(CANON_WIDTH / 2, (Game1.FENETRE.Height / 4) - (CANON_HEIGHT / 2));
            tabPosCanon[1] = new Vector2(CANON_WIDTH / 2, (Game1.FENETRE.Height / 2) + (CANON_HEIGHT / 2));
            tabPosCanon[2] = new Vector2(Game1.FENETRE.Width - (int)(CANON_WIDTH * 1.5), (Game1.FENETRE.Height / 4) - (CANON_HEIGHT / 2));
            tabPosCanon[3] = new Vector2(Game1.FENETRE.Width - (int)(CANON_WIDTH * 1.5), (Game1.FENETRE.Height / 2) + (CANON_HEIGHT / 2));

            tabRectCanon[0] = new Rectangle((int)tabPosCanon[0].X, (int)tabPosCanon[0].Y, CANON_WIDTH, CANON_HEIGHT);
            tabRectCanon[1] = new Rectangle((int)tabPosCanon[1].X, (int)tabPosCanon[1].Y, CANON_WIDTH, CANON_HEIGHT);
            tabRectCanon[2] = new Rectangle((int)tabPosCanon[2].X, (int)tabPosCanon[2].Y, CANON_WIDTH, CANON_HEIGHT);
            tabRectCanon[3] = new Rectangle((int)tabPosCanon[3].X, (int)tabPosCanon[3].Y, CANON_WIDTH, CANON_HEIGHT);
        }

        public void loadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            t2DCanon = content.Load<Texture2D>("images/trou");
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(t2DCanon, tabRectCanon[0], Color.White);
            spriteBatch.Draw(t2DCanon, tabRectCanon[1], Color.White);
            spriteBatch.Draw(t2DCanon, tabRectCanon[2], Color.White);
            spriteBatch.Draw(t2DCanon, tabRectCanon[3], Color.White);
        }

        public Vector2[] getPosCanon()
        {
            return tabPosCanon;
        }
    }
}
