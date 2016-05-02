using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Game1
{
    class Menu
    {
        Game1 parent;

        Texture2D t2Dwall, t2Dtitre;
        Texture2D[] tabT2Dbouton;
        Rectangle[] rectBouton;
        Rectangle rectTitre, rectWallpaper;
        SoundEffect sonBouton;
        public Menu(Game1 parent)
        {
            this.parent = parent;
            tabT2Dbouton = new Texture2D[5];
            rectBouton = new Rectangle[5];
            rectWallpaper = new Rectangle(0, 0, Game1.FENETRE.Width, Game1.FENETRE.Height);
        }

        public void loadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            sonBouton = content.Load<SoundEffect>("son/bouton");
            t2Dwall = content.Load<Texture2D>("images/wood/wall");
            t2Dtitre = content.Load<Texture2D>("images/menu/titre");
            tabT2Dbouton[0] = content.Load<Texture2D>("images/menu/arcade");
            tabT2Dbouton[1] = content.Load<Texture2D>("images/menu/multi");
            tabT2Dbouton[2] = content.Load<Texture2D>("images/menu/options");
            tabT2Dbouton[3] = content.Load<Texture2D>("images/menu/quitter");
            tabT2Dbouton[4] = content.Load<Texture2D>("images/menu/classement");
            rectTitre = new Rectangle(Game1.FENETRE.Width / 2 - t2Dtitre.Width / 2, Game1.FENETRE.Height / 2 - t2Dtitre.Height-50, t2Dtitre.Width, t2Dtitre.Height);
           
            int space = (Game1.FENETRE.Width - 4 * tabT2Dbouton[0].Width) / 4;
            for (int i = 0; i < tabT2Dbouton.Length-1; i++)
                rectBouton[i] = new Rectangle(space + (i * tabT2Dbouton[i].Width + space), Game1.FENETRE.Height/2 + rectBouton[i].Height+100, tabT2Dbouton[i].Width, tabT2Dbouton[i].Height);
            rectBouton[4] = new Rectangle(rectBouton[0].X, rectBouton[0].Height + rectBouton[0].Y, tabT2Dbouton[4].Width, tabT2Dbouton[4].Height);

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
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(t2Dwall, rectWallpaper, Color.White);
            spriteBatch.Draw(t2Dtitre, rectTitre, Color.White);
            for (int i = 0; i < tabT2Dbouton.Length; i++)
                spriteBatch.Draw(tabT2Dbouton[i], rectBouton[i], Color.White);
        }

        public void buttonAction(int i)
        {
            sonBouton.Play();
            switch (i)
            {
                case 0:
                    parent.startArcade();
                    break;
                case 1:
                    parent.startMulti();
                    break;
                case 2:
                    parent.startSettings();
                    break;
                case 3:
                    parent.Exit();
                    break;
                case 4:
                    parent.startClassement();
                    break;
            }
        }
    }
}
