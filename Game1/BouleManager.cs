using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class BouleManager
    {
        Texture2D[] tabT2Dboule = new Texture2D[8];
        Boule[] tabBboule = new Boule[10];
        Random rdmCanon;
        Vector2[] tabPosCanon = new Vector2[4];
        Body[] chariot;
        public BouleManager(World world, Vector2[] posCanon, Body sol, Body[] chariot, Texture2D blank)
        {
            createBodies(world, sol, blank);
            rdmCanon = new Random();
            tabPosCanon = posCanon;
            this.chariot = chariot;
            for (int i = 0; i < posCanon.Length; i++)
            {
                tabPosCanon[i].X /= Game1.METERINPIXEL;
                tabPosCanon[i].Y /= Game1.METERINPIXEL;
            }
        }

        public void loadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            tabT2Dboule[0] = content.Load<Texture2D>("images/" + Game1.THEME + "/1");
            tabT2Dboule[1] = content.Load<Texture2D>("images/" + Game1.THEME + "/2");
            tabT2Dboule[2] = content.Load<Texture2D>("images/" + Game1.THEME + "/3");
            tabT2Dboule[3] = content.Load<Texture2D>("images/" + Game1.THEME + "/4");
            tabT2Dboule[4] = content.Load<Texture2D>("images/" + Game1.THEME + "/5");
            tabT2Dboule[5] = content.Load<Texture2D>("images/" + Game1.THEME + "/6");
            tabT2Dboule[6] = content.Load<Texture2D>("images/" + Game1.THEME + "/bombe");
            tabT2Dboule[7] = content.Load<Texture2D>("images/" + Game1.THEME + "/bonus");
            for (int i = 0; i < tabBboule.Length; i++)
            {
                tabBboule[i].setTexture(tabT2Dboule[7]);
            }
        }

        private void createBodies(World world, Body sol, Texture2D blank)
        {
            for(int i = 0; i < tabBboule.Length; i++)
            {
                tabBboule[i] = new Boule(world, sol, blank);
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tabBboule.Length; i++)
            {
                tabBboule[i].draw(spriteBatch);
            }
        }

        public void update(GameTime gameTime)
        {
            for(int i = 0; i < tabBboule.Length; i++)
            {
                 tabBboule[i].update(chariot, gameTime);
            }
        }

        public void launchBoule(int idCanon, int idBoule, int direction, int maxW, int maxH)
        {
            for (int i = 0; i < tabBboule.Length; i++)
            {
                if (tabBboule[i].isSleeping())
                {
                    tabBboule[i].reset(tabPosCanon[idCanon], idBoule, direction, maxW, maxH, tabT2Dboule[idBoule]);
                    break;
                }
            }
        }

        public void launchBoule()
        {
            for(int i = 0; i < tabBboule.Length; i++)
            {
                if(tabBboule[i].isSleeping())
                {
                    int idCanon = rdmCanon.Next(4);
                    int idBoule = rdmCanon.Next(8);
                    int direction, maxH, maxW;
                    if(idCanon > 1)
                    {
                        direction = -1;
                        maxH = 3;
                        maxW = 5;
                    }
                    else
                    {
                        direction = 1;
                        maxH = 5;
                        maxW = 6;
                    }

                    tabBboule[i].reset(tabPosCanon[idCanon], idBoule, direction, maxW, maxH, tabT2Dboule[idBoule]);
                    break;
                }
            }
        }

    }
}
