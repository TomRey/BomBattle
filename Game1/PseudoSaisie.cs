using Microsoft.Xna.Framework;
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
    class PseudoSaisie
    {
        private TextBox tbxPseudo;
        private SpriteFont fontSaisie;
        private Texture2D[] tabT2Dbouton;
        private Rectangle[] rectBouton;
        private const int NB_BOUTON = 2;
        private SoundEffect sonBouton;
        private Texture2D t2Dwall;
        private Game1 root;
        private GameOver parent;
        private BouleManager manager; 
        public int Score { get; set; }
        private string message;
        private Vector2 posMessage;

        public PseudoSaisie(Game1 root, GameOver parent, BouleManager manager)
        {
            this.manager = manager;
            this.parent = parent;
            this.root = root;
            tbxPseudo = new TextBox(270, 10, "Pseudo");
            message = "";
            tabT2Dbouton = new Texture2D[NB_BOUTON];
            rectBouton = new Rectangle[NB_BOUTON];
        }

        public void loadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            tbxPseudo.loadContent(content);
            sonBouton = content.Load<SoundEffect>("son/bouton");
            fontSaisie = content.Load<SpriteFont>("font/saisie");
            tabT2Dbouton[0] = content.Load<Texture2D>("images/form/pseudo/retour");
            tabT2Dbouton[1] = content.Load<Texture2D>("images/form/pseudo/valider");
            t2Dwall = content.Load<Texture2D>("images/form/pseudo/wall");

            posMessage = new Vector2(Game1.FENETRE.Width / 2 - fontSaisie.MeasureString("Veuillez entrer votre pseudo").X / 2, 50);
            int space = (Game1.FENETRE.Width - NB_BOUTON * tabT2Dbouton[0].Width) / 4;
            for (int i = 0; i < NB_BOUTON; i++)
                rectBouton[i] = new Rectangle(space + (i * tabT2Dbouton[i].Width + space), 400, tabT2Dbouton[i].Width, tabT2Dbouton[i].Height);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(t2Dwall, Game1.rectWallpaper, Color.White);

            for (int i = 0; i < tabT2Dbouton.Length; i++)
                spriteBatch.Draw(tabT2Dbouton[i], rectBouton[i], Color.White);

            spriteBatch.DrawString(fontSaisie, message, posMessage, Color.Red);
            tbxPseudo.draw(spriteBatch, fontSaisie);
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

            tbxPseudo.update(ms, lastms);
        }

        public void buttonAction(int i)
        {
            sonBouton.Play();
            switch (i)
            {
                case 0:
                    parent.Saisie = false;
                    message = "";
                    break;
                case 1:
                    if (tbxPseudo.Text.Trim().Length > 0)
                    {
                        save();
                        root.backToMenu();
                        parent.Saisie = false;
                        message = "";
                    }
                    else
                        message = "Veuillez entrer votre pseudo";
                    break;
            }
        }

        private void save()
        {
            // This text is added only once to the file.
            if (!File.Exists(Game1.CLASSEMENT_PATH))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(Game1.CLASSEMENT_PATH))
                {
                    sw.WriteLine(tbxPseudo.Text + ":" + manager.score);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(Game1.CLASSEMENT_PATH))
                {
                    sw.WriteLine(tbxPseudo.Text + ":" + manager.score);
                }
            }
        }
    }
}
