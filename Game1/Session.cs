using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Session
    {
        private SpriteFont fontSaisie;
        private Texture2D[] tabT2Dbouton;
        private Rectangle[] rectBouton;
        private const int NB_BOUTON = 2;
        private SoundEffect sonBouton;
        private Texture2D t2Dwall, t2Dpanel;
        private Game1 root;
        private Multi parent;
        private Vector2 posInfoPlayer, posJoueurs, posPanel;
        public string joueurs{get; set;}
        private int nbPlayer;
        private string infoPlayer;

        public Session(Game1 root, Multi parent)
        {
            this.parent = parent;
            this.root = root;
            joueurs = "";
            nbPlayer = 0;
            infoPlayer = "Nombre de joueurs : " + nbPlayer + " / 8";
            tabT2Dbouton = new Texture2D[NB_BOUTON];
            rectBouton = new Rectangle[NB_BOUTON];
        }

        public void addPlayer(string pseudo)
        {
            nbPlayer++;
            infoPlayer = "Nombre de joueurs : " + nbPlayer + " / 8";
            joueurs += pseudo + Environment.NewLine;
        }

        public void loadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            sonBouton = content.Load<SoundEffect>("son/bouton");
            fontSaisie = content.Load<SpriteFont>("font/saisie");
            tabT2Dbouton[0] = content.Load<Texture2D>("images/form/session/retour");
            tabT2Dbouton[1] = content.Load<Texture2D>("images/form/session/start");
            t2Dwall = content.Load<Texture2D>("images/form/wall");
            t2Dpanel = content.Load<Texture2D>("images/form/session/panel");

            posInfoPlayer = new Vector2(Game1.FENETRE.Width/2 - fontSaisie.MeasureString(infoPlayer).X/2 + 20, 100);
            posPanel = new Vector2(Game1.FENETRE.Width / 2 - t2Dpanel.Width / 2 + 30, -50);
            posJoueurs = new Vector2(posPanel.X + 680, 210);
            int space = (Game1.FENETRE.Width - NB_BOUTON * tabT2Dbouton[0].Width) / 4;
            for (int i = 0; i < NB_BOUTON; i++)
                rectBouton[i] = new Rectangle(space + (i * tabT2Dbouton[i].Width + space) + 20, 700, tabT2Dbouton[i].Width, tabT2Dbouton[i].Height);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(t2Dwall, Game1.rectWallpaper, Color.White);
            spriteBatch.Draw(t2Dpanel, posPanel, Color.White);

            spriteBatch.Draw(tabT2Dbouton[0], rectBouton[0], Color.White);

            if (nbPlayer > 1)
                spriteBatch.Draw(tabT2Dbouton[1], rectBouton[1], Color.White);

            spriteBatch.DrawString(fontSaisie, joueurs, posJoueurs, Color.Black);
            spriteBatch.DrawString(fontSaisie, infoPlayer, posInfoPlayer, Color.Black);
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
            switch (i)
            {
                case 0:
                    sonBouton.Play();
                    parent.getServer().stopServer();
                    reset();
                    break;
                case 1:
                    if (nbPlayer > 1)
                    {
                        sonBouton.Play();
                        root.gameState = GameState.Multi;
                        parent.getServer().startGame();
                        root.startSonJeu();
                        reset();
                    }
                    break;
            }
            Debug.WriteLine("Bouton pressé");
        }

        private void reset()
        {
            parent.SessionOn = false;
            joueurs = "";
            nbPlayer = 0;
            infoPlayer = "Nombre de joueurs : " + nbPlayer + " / 8";
        }
    }
}
