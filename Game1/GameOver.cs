﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class GameOver
    {
        Game1 parent;

        Texture2D t2Dtitre;
        Texture2D[] tabT2Dbouton;
        Rectangle[] rectBouton;
        Rectangle rectTitre;
        const int NB_BOUTON = 3;
        SoundEffect sonBouton;
        PseudoSaisie pseudoSaisie;
        public bool Saisie { get; set; }


        public GameOver(Game1 parent, BouleManager manager)
        {
            Saisie = false;
            pseudoSaisie = new PseudoSaisie(parent, this, manager);
            this.parent = parent;
            tabT2Dbouton = new Texture2D[NB_BOUTON];
            rectBouton = new Rectangle[NB_BOUTON];
        }

        public void loadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            sonBouton = content.Load<SoundEffect>("son/bouton");
            t2Dtitre = content.Load<Texture2D>("images/gameOver/gameOver");
            tabT2Dbouton[0] = content.Load<Texture2D>("images/gameOver/rejouer");
            tabT2Dbouton[1] = content.Load<Texture2D>("images/gameOver/sauver");
            tabT2Dbouton[2] = content.Load<Texture2D>("images/gameOver/menu");
            pseudoSaisie.loadContent(content);

            rectTitre = new Rectangle(Game1.FENETRE.Width / 2 - t2Dtitre.Width / 2, 50, t2Dtitre.Width, t2Dtitre.Height);
            int space = (Game1.FENETRE.Width - NB_BOUTON * tabT2Dbouton[0].Width) / 4;
            for (int i = 0; i < tabT2Dbouton.Length; i++)
                rectBouton[i] = new Rectangle(space + (i * tabT2Dbouton[i].Width + space), 600, tabT2Dbouton[i].Width, tabT2Dbouton[i].Height);

        }

        public void update(MouseState ms, MouseState lastms)
        {
            if (ms.LeftButton == ButtonState.Released && lastms.LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i < tabT2Dbouton.Length; i++)
                {
                    if (ms.X > rectBouton[i].X && ms.X < rectBouton[i].X + rectBouton[i].Width &&
                        ms.Y > rectBouton[i].Y && ms.Y < rectBouton[i].Y + rectBouton[i].Height)
                    {
                        buttonAction(i);
                    }
                }
            }

            if (Saisie)
                pseudoSaisie.update(ms, lastms);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(t2Dtitre, rectTitre, Color.White);
            for (int i = 0; i < tabT2Dbouton.Length; i++)
                spriteBatch.Draw(tabT2Dbouton[i], rectBouton[i], Color.White);

            if (Saisie)
                pseudoSaisie.draw(spriteBatch);
        }

        public void buttonAction(int i)
        {
            sonBouton.Play();
            switch (i)
            {
                case 0:
                    parent.replay();
                    break;
                case 1:
                    Saisie = true;
                    break;
                case 2:
                    parent.backToMenu();
                    break;
            }
        }
    }
}
