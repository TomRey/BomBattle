using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Settings
    {
        private SpriteFont fontSaisie;
        private Texture2D[] tabT2Dbouton;
        private Rectangle[] rectBouton;
        private SoundEffect sonBouton;
        private Texture2D t2Dwall;
        private Game1 root;
        private const int NB_BOUTON = 3;
        private GraphicsDeviceManager graphics;
        private string message;
        private Vector2 posMessage;

        public Settings(Game1 root, GraphicsDeviceManager graphics)
        {
            this.root = root;
            this.graphics = graphics;
            tabT2Dbouton = new Texture2D[NB_BOUTON+2];
            rectBouton = new Rectangle[NB_BOUTON];
            message = "Le mode fenetre / plein ecran necessite un redemarrage de l'application";
        }

        public void loadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            sonBouton = content.Load<SoundEffect>("son/bouton");
            fontSaisie = content.Load<SpriteFont>("font/saisie");
            tabT2Dbouton[0] = content.Load<Texture2D>("images/form/multi/retour");
            tabT2Dbouton[1] = content.Load<Texture2D>("images/form/settings/sonon");
            tabT2Dbouton[2] = content.Load<Texture2D>("images/form/settings/sonoff");
            tabT2Dbouton[3] = content.Load<Texture2D>("images/form/settings/fenetreon");
            tabT2Dbouton[4] = content.Load<Texture2D>("images/form/settings/fenetreoff");
            t2Dwall = content.Load<Texture2D>("images/form/wall");

            rectBouton[0] = new Rectangle(Game1.FENETRE.Width / 2 - tabT2Dbouton[0].Width / 2, Game1.FENETRE.Height - tabT2Dbouton[0].Height, tabT2Dbouton[0].Width, tabT2Dbouton[0].Height);
            rectBouton[1] = new Rectangle(Game1.FENETRE.Width / 2 - tabT2Dbouton[1].Width, Game1.FENETRE.Height / 2 - tabT2Dbouton[1].Height / 2, tabT2Dbouton[1].Width, tabT2Dbouton[1].Height);
            rectBouton[2] = new Rectangle(Game1.FENETRE.Width / 2, Game1.FENETRE.Height / 2 - tabT2Dbouton[1].Height / 2, tabT2Dbouton[3].Width, tabT2Dbouton[3].Height);
            posMessage = new Vector2(Game1.FENETRE.Width / 2 - fontSaisie.MeasureString(message).X / 2, 50);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(t2Dwall, Game1.rectWallpaper, Color.White);
            spriteBatch.Draw(tabT2Dbouton[0], rectBouton[0], Color.White);
            spriteBatch.DrawString(fontSaisie, message, posMessage, Color.Red);

            if(Settings1.Default.son)
                spriteBatch.Draw(tabT2Dbouton[1], rectBouton[1], Color.White);
            else
                spriteBatch.Draw(tabT2Dbouton[2], rectBouton[1], Color.White);

            if (Settings1.Default.fenetre)
                spriteBatch.Draw(tabT2Dbouton[3], rectBouton[2], Color.White);
            else
                spriteBatch.Draw(tabT2Dbouton[4], rectBouton[2], Color.White);
        }

        public void update(MouseState ms, MouseState lastms)
        {
            if (ms.LeftButton == ButtonState.Released && lastms.LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i < NB_BOUTON; i++)
                {
                    if (ms.X > rectBouton[i].X && ms.X < rectBouton[i].X + rectBouton[i].Width &&
                        ms.Y > rectBouton[i].Y && ms.Y < rectBouton[i].Y + rectBouton[i].Height)
                    {
                        buttonAction(i);
                    }
                }
            }
        }

        public void buttonAction(int i)
        {
            sonBouton.Play();
            switch (i)
            {
                case 0:
                    root.gameState = GameState.Menu;
                    Settings1.Default.Save();
                    break;
                case 1:
                    Settings1.Default.son = !Settings1.Default.son;
                    if (Settings1.Default.son)
                    {
                        SoundEffect.MasterVolume = 0.5f;
                        root.startSon();
                    }
                    else
                    { 
                        SoundEffect.MasterVolume = 0;
                        MediaPlayer.Pause();
                    }
                    break;
                case 2:
                    Settings1.Default.fenetre = !Settings1.Default.fenetre;
                    break;
            }
        }
    }
}
