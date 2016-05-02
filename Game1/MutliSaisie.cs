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
    class MutliSaisie
    {
        private TextBox tbxPseudo;
        private TextBox tbxIp;
        private SpriteFont fontSaisie;
        private Texture2D[] tabT2Dbouton;
        private Rectangle[] rectBouton;
        private const int NB_BOUTON = 4;
        private SoundEffect sonBouton;
        private Texture2D t2Dwall;
        private Game1 root;
        private Multi parent;
        private Vector2 posMessage;
        public string message;
        private string monIP;
        public MutliSaisie(Game1 root, Multi parent)
        {
            this.parent = parent;
            this.root = root;
            message = "";
            tbxPseudo = new TextBox(270, 10, "Pseudo");
            tbxIp = new TextBox(450, 15, "Adresse IP");
            posMessage = new Vector2(0, 50);
            tabT2Dbouton = new Texture2D[NB_BOUTON];
            rectBouton = new Rectangle[NB_BOUTON];

            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("10.0.2.4", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                monIP = endPoint.Address.ToString();
                tbxIp.Text = monIP;
            }
        }

        public void loadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            tbxIp.loadContent(content);
            tbxPseudo.loadContent(content);
            sonBouton = content.Load<SoundEffect>("son/bouton");
            fontSaisie = content.Load<SpriteFont>("font/saisie");
            tabT2Dbouton[0] = content.Load<Texture2D>("images/form/multi/retour");
            tabT2Dbouton[1] = content.Load<Texture2D>("images/form/multi/creer");
            tabT2Dbouton[2] = content.Load<Texture2D>("images/form/multi/rejoindre");
            tabT2Dbouton[3] = content.Load<Texture2D>("images/form/multi/ip");
            t2Dwall = content.Load<Texture2D>("images/form/wall");

            int space = (Game1.FENETRE.Width - 3 * tabT2Dbouton[0].Width) / 4;
            for (int i = 0; i < 3; i++)
                rectBouton[i] = new Rectangle(space + (i * tabT2Dbouton[i].Width + space), 600, tabT2Dbouton[i].Width, tabT2Dbouton[i].Height);
            rectBouton[3] = new Rectangle((int)tbxIp.getPosition().X + tbxIp.getWidth()+10, (int)tbxIp.getPosition().Y, tabT2Dbouton[3].Width, tabT2Dbouton[3].Height);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(t2Dwall, Game1.rectWallpaper, Color.White);

            for (int i = 0; i < 3; i++)
                spriteBatch.Draw(tabT2Dbouton[i], rectBouton[i], Color.White);

            spriteBatch.Draw(tabT2Dbouton[3], rectBouton[3], Color.White);
            spriteBatch.DrawString(fontSaisie, message, posMessage, Color.Red);
            tbxPseudo.draw(spriteBatch, fontSaisie);
            tbxIp.draw(spriteBatch, fontSaisie);
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
            tbxIp.update(ms, lastms);
        }

        public void setMessage(string message)
        {
            posMessage.X = Game1.FENETRE.Width / 2 - fontSaisie.MeasureString(message).X/2;
            this.message = message;
        }

        public void buttonAction(int i)
        {
            sonBouton.Play();
            switch (i)
            {
                case 0:
                    root.gameState = GameState.Menu;
                    message = "";
                    break;
                case 1:
                    if (verifSaisie())
                    {
                        message = "";
                        parent.creer(tbxPseudo.Text, tbxIp.Text);
                    }
                    else
                        setMessage("Merci de remplir les 2 champs");
                    break;
                case 2:
                    if (verifSaisie())
                    {
                        message = "";
                        parent.rejoindre(tbxPseudo.Text, tbxIp.Text);
                    }
                    else
                        setMessage("Merci de remplir les 2 champs");
                    break;
                case 3:
                    tbxIp.Text = monIP;
                    break;
            }
        }

        private bool verifSaisie()
        {
            return tbxIp.Text.Trim().Length > 0 && tbxPseudo.Text.Trim().Length > 0 ? true : false;
        }
    }
}
