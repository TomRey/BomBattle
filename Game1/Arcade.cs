using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Arcade
    {
        public int Point {get; set;}
        public string Pseudo { get; set; }
        private BouleManager manager;
        private bool startCount = false;
        private bool startGame = false;
        private string textCount = "";
        private double timer = 4;
        SpriteFont fontInfo;
        Vector2 posTxt;
        float VitesseTimer1, VitesseTimer2, VitesseTimer3, VitesseTimer4 = 0;
        float timer1, timer2, timer3, timer4 = 0;
        float tempsJeu;
        Random rand = new Random();
        public Arcade(BouleManager manager)
        {
            this.manager = manager;
            posTxt = new Vector2((Game1.FENETRE.Width / 2), Game1.FENETRE.Height / 2);
        }

        public void loadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            fontInfo = content.Load<SpriteFont>("font/candy");
        }

        public void update(GameTime gameTime)
        {
            if(startCount)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                if(timer <= 1)
                    textCount = "GO!";
                else
                    textCount = Math.Floor(timer)+"";

                posTxt.X = Game1.FENETRE.Width / 2 - (fontInfo.MeasureString(textCount).X / 2);
                posTxt.Y = Game1.FENETRE.Height / 2 - (fontInfo.MeasureString(textCount).Y / 2);

                if (timer <= 0)
                {
                    startCount = false;
                    startGame = true;
                }
            }
            else if (startGame)
            {
                generateBoules(gameTime);
                tempsJeu += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if(startCount)
                spriteBatch.DrawString(fontInfo, textCount, posTxt, Color.White);
        }

        public void start()
        {
            manager.score = 0;
            startCount = true;
            startGame = false;
            timer = 4;
            timer1 = 0;
            timer2 = 0;
            timer3 = 0;
            timer4 = 0;
            tempsJeu = 0;
            VitesseTimer1 = 1;
            VitesseTimer2 = 6;
            VitesseTimer3 = 4;
            VitesseTimer4 = 3;
        }


        public void generateBoules(GameTime gameTime)
        {
            if (tempsJeu > 20)
            {
                VitesseTimer1 = 3;
                VitesseTimer2 = 1;
                VitesseTimer4 = 3;
            }

            if (tempsJeu > 40)
            {
                VitesseTimer3 = 2;
            }

            if (tempsJeu > 70)
            {
                VitesseTimer1 = 2;
                VitesseTimer2 = 3;
                VitesseTimer3 = 2;
                VitesseTimer4 = 1;
            }

            if (tempsJeu > 100)
            {
                VitesseTimer1 = 2;
                VitesseTimer2 = 1;
                VitesseTimer3 = 2;
                VitesseTimer4 = 1;
            }

            if (timer1 < VitesseTimer1)
            {
                timer1 += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                manager.launchBoule(0, rand.Next(8), 1, rand.Next(2, 5), rand.Next(1, 3));
                timer1 = 0;
            }

            if (tempsJeu > 20)
            {
                if (timer2 < VitesseTimer2)
                {
                    timer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    manager.launchBoule(1, rand.Next(8), 1, rand.Next(1, 4), rand.Next(3, 5));
                    timer2 = 0;
                }
            }

            if (tempsJeu > 40)
            {
                if (timer3 < VitesseTimer3)
                {
                    timer3 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    manager.launchBoule(2, rand.Next(8), -1, rand.Next(2, 5), rand.Next(1, 3));
                    timer3 = 0;
                }
            }

            if (timer4 < VitesseTimer4)
            {
                timer4 += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                manager.launchBoule(3, rand.Next(8), -1, rand.Next(1, 4), rand.Next(3, 5));
                timer4 = 0;
            }
        }
    }
}
