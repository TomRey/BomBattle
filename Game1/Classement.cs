using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
    class Classement
    {
        private SpriteFont fontSaisie;
        private Texture2D[] tabT2Dbouton;
        private Rectangle[] rectBouton;
        private SoundEffect sonBouton;
        private Texture2D t2Dwall, t2Dpanel;
        private Game1 root;
        private Vector2 posPosition, posPseudo, posScore, posPanel;
        private int nbElementByPage = 10;
        private int page = 0;
        private List<KeyValuePair<string, int>> list;
        IOrderedEnumerable<KeyValuePair<string, int>> listSorted;
        private string position;
        private string pseudo;
        private string score;
        private const int NB_BOUTON = 3;
        private bool maxPage = false;

        public Classement(Game1 root)
        {
            this.root = root;
            posPseudo = new Vector2(700, 150);
            posScore = new Vector2(1050, 150);

            tabT2Dbouton = new Texture2D[NB_BOUTON];
            rectBouton = new Rectangle[NB_BOUTON];

            maxPage = true;
            pseudo = "PSEUDO";
            score = "SCORE";
            position = "POSITION";

            list = new List<KeyValuePair<string, int>>();         
        }

        private bool getData()
        {
            if (File.Exists(Game1.CLASSEMENT_PATH))
            {
                using (StreamReader sr = File.OpenText(Game1.CLASSEMENT_PATH))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] values = s.Split(':');
                        list.Add(new KeyValuePair<string, int>(values[0], int.Parse(values[1])));
                    }
                }
                return true;
            }
            else
                return false;
        }

        public void loadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            sonBouton = content.Load<SoundEffect>("son/bouton");
            fontSaisie = content.Load<SpriteFont>("font/saisie");
            tabT2Dbouton[0] = content.Load<Texture2D>("images/form/classement/retour");
            tabT2Dbouton[1] = content.Load<Texture2D>("images/form/classement/previous");
            tabT2Dbouton[2] = content.Load<Texture2D>("images/form/classement/next");
            t2Dpanel = content.Load<Texture2D>("images/form/classement/panel");
            t2Dwall = content.Load<Texture2D>("images/form/wall");

            posPanel = new Vector2(Game1.FENETRE.Width / 2 - t2Dpanel.Width / 2, Game1.FENETRE.Height / 2 - t2Dpanel.Height / 2);
            posPosition = new Vector2(posPanel.X + 445, posPanel.Y + 140);
            posPseudo = new Vector2(posPosition.X + 285, posPosition.Y);
            posScore = new Vector2(posPseudo.X + 370, posPosition.Y);
            rectBouton[0] = new Rectangle(Game1.FENETRE.Width/2 - tabT2Dbouton[0].Width /2 , Game1.FENETRE.Height-tabT2Dbouton[0].Height - 10, tabT2Dbouton[0].Width, tabT2Dbouton[0].Height);
            rectBouton[1] = new Rectangle(Game1.FENETRE.Width / 2 + 340, (int)(posPanel.Y + 800), tabT2Dbouton[1].Width, tabT2Dbouton[1].Height);
            rectBouton[2] = new Rectangle(rectBouton[1].X + rectBouton[1].Width + 10, rectBouton[1].Y, tabT2Dbouton[2].Width, tabT2Dbouton[2].Height);
        }

        public void loadClassement()
        {
            page = 0;
            list.Clear();
            if (getData())
            {
                maxPage = false;
                listSorted = list.OrderByDescending(key => key.Value);
                nextPage();
            }
        }

        private void nextPage()
        {
            int b1 = (page * nbElementByPage);
            int b2 = (page * nbElementByPage) + nbElementByPage;
            pseudo = "PSEUDO" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            score = "SCORE" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            position = "POSITION" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            for (int i = b1; i < b2; i++)
            {
                position += (i + 1) + Environment.NewLine;
                pseudo += listSorted.ElementAt<KeyValuePair<string, int>>(i).Key + Environment.NewLine;
                score += listSorted.ElementAt<KeyValuePair<string, int>>(i).Value + Environment.NewLine;

                if (i >= (list.Count-1))
                {
                    maxPage = true;
                    break;
                }
            }
            page++; 
        }

        private void previousPage()
        {
            maxPage = false;
            page--;
            int b2 = (page * nbElementByPage);
            int b1 = (page * nbElementByPage) - nbElementByPage;
            pseudo = "PSEUDO" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            score = "SCORE" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            position = "POSITION" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            for (int i = b1; i < b2; i++)
            {
                position += (i + 1) + Environment.NewLine;
                pseudo += listSorted.ElementAt<KeyValuePair<string, int>>(i).Key + Environment.NewLine;
                score += listSorted.ElementAt<KeyValuePair<string, int>>(i).Value + Environment.NewLine;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(t2Dwall, Game1.rectWallpaper, Color.White);

            spriteBatch.Draw(t2Dpanel, posPanel, Color.White);
            spriteBatch.Draw(tabT2Dbouton[0], rectBouton[0], Color.White);
            if (page > 1)
                spriteBatch.Draw(tabT2Dbouton[1], rectBouton[1], Color.White);
            if (!maxPage)
                spriteBatch.Draw(tabT2Dbouton[2], rectBouton[2], Color.White);

            spriteBatch.DrawString(fontSaisie, position, posPosition, Color.Black);
            spriteBatch.DrawString(fontSaisie, pseudo, posPseudo, Color.Black);
            spriteBatch.DrawString(fontSaisie, score, posScore, Color.Black);
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
                    root.gameState = GameState.Menu;
                    sonBouton.Play();
                    break;
                case 1:
                    if (page > 1)
                    {
                        previousPage();
                        sonBouton.Play();
                    }
                    break;
                case 2:
                    if (!maxPage)
                    {
                        nextPage();
                        sonBouton.Play();
                    }
                    break;
            }
        }
    }
}
