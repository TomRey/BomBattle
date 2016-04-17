using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**************************************
* BONUS!
***************************************
* 1) 0 - 19    : points nég doublé pendant 10 secondes
* 2) 20 - 39   : pas de boules positives pendant 10 secondes
* 3) 40 - 59   : voiture ralentie pendant 10 secondes
* 4) 60 - 69   : absorber une bombe (1 vie)
* 5) 70 - 79   : inverser les commandes pendant 10 secondes
* 6) 80 - 85   : rafale de bombe pendant 8 secondes
* 7) 86 - 88   : BLACKSCREEN pendant 10 secondes!
****************************************/

namespace Game1
{
    class Bonus
    {
        public int[] id { get; set; }
        double[] timer, timerMax;
        public bool active { get; set; }
        public int nbVie { get; set; }
        Vector2 blackscreenOrigin, posInfo;
        Texture2D blackScreen, blackScreen2;
        Texture2D[] t2Dbonus;
        Chariot chariot;
        SpriteFont fontInfo;
        Texture2D[] tabT2Dbonus;
        Vector2 posBonus, posVie;
        Vector2[] posBonusDispo;
        int POS_H_VIE = Game1.FENETRE.Height - 75;
        int nbBonus;
        int lastBonusIDtab;
        public Bonus(Chariot chariot)
        {
            id = new int[2] { -1, -1 };
            timer = new double[2] { 0, 0 };
            timerMax = new double[2] { 0, 0 };
            posInfo = new Vector2(Game1.FENETRE.Width - 300, Game1.FENETRE.Height - 100);
            this.chariot = chariot;
            active = false;
            nbVie = 0;
            nbBonus = 0;
            lastBonusIDtab = 0;
            tabT2Dbonus = new Texture2D[8];
            t2Dbonus = new Texture2D[2];
            posVie = new Vector2(Game1.FENETRE.Width - 75, Game1.FENETRE.Height - 75);
            posBonusDispo = new Vector2[2];
            posBonusDispo[0] = new Vector2(10, Game1.FENETRE.Height - 75);
            posBonusDispo[1] = new Vector2(85, Game1.FENETRE.Height - 75);
            posBonus = new Vector2(10, Game1.FENETRE.Height - 75);
        }

        public void loadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            fontInfo = content.Load<SpriteFont>("font/info");
            blackScreen = content.Load<Texture2D>("images/bonus/blackscreen");
            blackScreen2 = content.Load<Texture2D>("images/bonus/blackscreen2");
            tabT2Dbonus[0] = content.Load<Texture2D>("images/bonus/bonus0");
            tabT2Dbonus[1] = content.Load<Texture2D>("images/bonus/bonus1");
            tabT2Dbonus[2] = content.Load<Texture2D>("images/bonus/bonus2");
            tabT2Dbonus[3] = content.Load<Texture2D>("images/bonus/bonus3");
            tabT2Dbonus[4] = content.Load<Texture2D>("images/bonus/bonus4");
            tabT2Dbonus[5] = content.Load<Texture2D>("images/bonus/bonus5");
            tabT2Dbonus[6] = content.Load<Texture2D>("images/bonus/bonus6");
            tabT2Dbonus[7] = content.Load<Texture2D>("images/bonus/bonus7");
            t2Dbonus[0] = tabT2Dbonus[0];
            t2Dbonus[1] = tabT2Dbonus[0];
            blackscreenOrigin = new Vector2(blackScreen.Width / 2, blackScreen.Height / 2);
        }

        private void bonusUpdate(int idTab)
        {
            if (timer[idTab] > timerMax[idTab])
            {
                resetChariot(idTab);
                t2Dbonus[idTab] = tabT2Dbonus[0];
                id[idTab] = -1;
                timer[idTab] = -1;
                nbBonus--;
            }
        }

        public void update(GameTime gameTime)
        {
            bonusUpdate(0);
            bonusUpdate(1);

            if (timer[0] >= 0)
                timer[0] += gameTime.ElapsedGameTime.TotalSeconds;
            if (timer[1] >= 0)
                timer[1] += gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (id[0] == 7 || id[1] == 7)
            {
                spriteBatch.Draw(blackScreen2, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(blackScreen, chariot.getPos(), null, Color.White, 0, blackscreenOrigin, 1f, SpriteEffects.None, 0f);
            }

            posVie.Y = POS_H_VIE;
            for (int i = 0; i < nbVie; i++)
            {
                spriteBatch.Draw(tabT2Dbonus[4], posVie, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                posVie.Y -= 75;
            }

            spriteBatch.Draw(t2Dbonus[0], posBonusDispo[0], null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(t2Dbonus[1], posBonusDispo[1], null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public void resetChariot(int idBonus)
        {
            if (id[idBonus] == 3)
            {
                chariot.vNormal();
            }
            else if(id[idBonus] == 5)
            { 
                chariot.invertCommande(1);
            }
        }

        private void setBonus(int idTabBonus, int bonus)
        {
            timer[idTabBonus] = 0;
            resetChariot(idTabBonus);
            timerMax[idTabBonus] = 10;
            this.id[idTabBonus] = bonus;
            t2Dbonus[idTabBonus] = tabT2Dbonus[bonus];
            nbBonus++;
        }

        private void checkBonus(int bonus)
        {
            if (id[0] == bonus)
            {
                timerMax[0] += 10;
                lastBonusIDtab = 0;
            }
            else if (id[1] == bonus)
            {
                timerMax[1] += 10;
                lastBonusIDtab = 1;
            }
            else if (id[0] == -1 || lastBonusIDtab == 1)
            {
                setBonus(0, bonus);
                lastBonusIDtab = 0;
            }
            else
            {
                setBonus(1, bonus);
                lastBonusIDtab = 1;
            }
        }

        public void newBonus(int idBonus)
        {
            if (idBonus >= 0 && idBonus <= 19) //1)
            {
                checkBonus(1);
            }
            else if (idBonus >= 20 && idBonus <= 39)//2)
            {
                checkBonus(2);
            }
            else if(idBonus >= 40 && idBonus <= 59)//3)
            {
                checkBonus(3);
                chariot.vRalentie();
            }
            else if (idBonus >= 60 && idBonus <= 69)//4)
            {
                nbVie++;
            }
            else if (idBonus >= 70 && idBonus <= 79)//5)
            {
                checkBonus(5);
                chariot.invertCommande(-1);
            }
            else if (idBonus >= 80 && idBonus <= 85)//6)
            {
                checkBonus(6);
            }
            else//7)
            {
                checkBonus(7);
            }
        }

    }
}
