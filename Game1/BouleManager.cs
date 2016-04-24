using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        Chariot chariot;
        SpriteFont fontScore, fontInfo;
        int[] tabValeurBoule;
        string strPoint = "";
        Vector2 posTxt, posScore;
        double time = 2;
        const int SCORE_MAX = 1500;
        public int score { get; set; }
        int currentScore = 0;
        Game1 parent;
        public bool bGameOver { get; set; }
        Bonus bonus;
        SoundEffect sonPoint, sonBonus;

        public BouleManager(World world, Vector2[] posCanon, Body sol, Chariot chariot, Game1 parent)
        {
            this.parent = parent;
            score = 0;
            bGameOver = false;
            tabValeurBoule = new int[] { 10, 20, 30, -10, -20, -30 };
            posTxt = new Vector2((Game1.FENETRE.Width / 2), Game1.FENETRE.Height / 2);
            posScore = new Vector2(50, 50);
            createBodies(world, sol);
            rdmCanon = new Random();
            tabPosCanon = posCanon;
            this.chariot = chariot;
            bonus = new Bonus(chariot);
            for (int i = 0; i < posCanon.Length; i++)
            {
                tabPosCanon[i].X /= Game1.METERINPIXEL;
                tabPosCanon[i].Y /= Game1.METERINPIXEL;
            }
        }

        public void loadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            fontScore = content.Load<SpriteFont>("font/candy");
            fontInfo = content.Load<SpriteFont>("font/info");
            sonPoint = content.Load<SoundEffect>("son/chtonk");
            sonBonus = content.Load<SoundEffect>("son/bonus");
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

            bonus.loadContent(content);
        }

        public void reset()
        {
            bGameOver = false;
            score = 0;
            strPoint = "";
            bonus.reset();
        }

        public string getPlayerData()
        {
            return score + "#" + bonus.nbVie + "#" + bonus.nbBonus + "#" + bGameOver;
        }

        private void createBodies(World world, Body sol)
        {
            for(int i = 0; i < tabBboule.Length; i++)
            {
                tabBboule[i] = new Boule(world, sol);
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (time < 1)
                spriteBatch.DrawString(fontScore, strPoint, posTxt, Color.Black);

            spriteBatch.DrawString(fontInfo, score.ToString(), posScore, Color.White);

            for (int i = 0; i < tabBboule.Length; i++)
            {
                tabBboule[i].draw(spriteBatch, fontScore);
            }

            bonus.draw(spriteBatch);
        }

        public void update(GameTime gameTime)
        {
            for(int i = 0; i < tabBboule.Length; i++)
            {
                 tabBboule[i].update(chariot, gameTime, this);
            }
            time += gameTime.ElapsedGameTime.TotalSeconds;

            bonus.update(gameTime);
        }

        public void addPoint(int id)
        {
            if (!bGameOver)
            {
                int scoreBoule = tabValeurBoule[id];
                if ((bonus.id[0] == 1 || bonus.id[1] == 1) && id > 2)//bonus 1 : point neg doublé
                {
                    scoreBoule *= 2;
                }

                if (time < 1)
                    currentScore += scoreBoule;
                else
                    currentScore = scoreBoule;

                score += scoreBoule;

                if (score < 0)
                    score = 0;

                if (parent.gameState == GameState.Multi && score >= SCORE_MAX)
                {
                    bGameOver = true;
                    parent.sendWinner();
                }

                time = 0;
                strPoint = currentScore.ToString();
                posTxt.X = Game1.FENETRE.Width / 2 - (fontScore.MeasureString(strPoint).X / 2);
                posTxt.Y = Game1.FENETRE.Height / 2 - (fontScore.MeasureString(strPoint).Y / 2);
                sonPoint.Play();
            }
        }

        public void gameOver()
        {
            if (!bGameOver)
            {
                if (bonus.nbVie <= 0)
                {
                    strPoint = "";
                    chariot.explose();
                    if (parent.gameState == GameState.Multi)
                    {
                        parent.gameOverMulti();
                        bGameOver = true;

                    }
                    else
                    {
                        this.parent.gameState = GameState.GameOver;
                        bGameOver = true;
                    }
                }
                else
                    bonus.nbVie--;
            }
        }

        public void addBonus()
        {
            if (!bGameOver)
            {
                sonBonus.Play();
                int idBonus = rdmCanon.Next(89);
                if (parent.gameState == GameState.Multi)
                {
                    if (idBonus >= 60 && idBonus <= 69)//4)
                    {
                        bonus.newBonus(idBonus);
                    }
                    else
                    {
                        parent.addBonus(idBonus);
                        bonus.setBonusDispo(idBonus);
                    }
                }
                else
                    bonus.newBonus(idBonus);
            }
        }

        public void activeBonus(int idBonus)
        {
            bonus.newBonus(idBonus);
        }

        public void removeBonusDispo()
        {
            bonus.removeBonusDispo();
        }

        public void launchBoule(int idCanon, int idBoule, int direction, int maxW, int maxH)
        {
            
            if (bonus.id[0] == 6 || bonus.id[1] == 6)//Bonus 6 : rafale de bombe!
                idBoule = 6;
            else if ((bonus.id[0] == 2 || bonus.id[1] == 2) && idBoule < 3)//Bonus 2 : pas de boule positives
                idBoule = rdmCanon.Next(3, 8);

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
                    direction = idCanon > 1 ? -1 : 1;
                    if (idCanon == 0 || idCanon == 2)
                    {
                        maxH = 3;
                        maxW = 5;
                    }
                    else
                    {
                        maxH = 4;
                        maxW = 6;
                    }
                    tabBboule[i].reset(tabPosCanon[idCanon], idBoule, direction, maxW, maxH, tabT2Dboule[idBoule]);
                    break;
                }
            }
        }

    }
}
