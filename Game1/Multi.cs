using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game1
{
    class Multi
    {
        string pseudo = "";
        string ip = "";
        int type = 0;
        Game1 parent;
        Client client;
        Server server;
        FormSession formS;
        BouleManager manager;
        Texture2D t2DRuban, t2DCercle, t2DMenu, t2DWinner, t2DGameOver;
        Vector2[] v2TabPosJoueurData;
        SpriteFont fontDataPlayer;
        int[] iTabPosJoueurData;
        public Dictionary<string, JoueurData> joueurData;
        bool bonusDispo = false;
        int idBonus = 0;
        Microsoft.Xna.Framework.Input.Keys[] keyPlayer;
        SpriteFont fontDecompte;
        string txtCpt = "";
        bool bDecompte = true;
        Vector2 posTxt, posVainqueur, posBoutonMenu, posWinnerLogo;
        string vainqueur;
        bool bFinishGame = false;
        Chariot chariot;
        bool runningGame = false;
        float VitesseTimer1, VitesseTimer2, VitesseTimer3, VitesseTimer4 = 0;
        float timer1, timer2, timer3, timer4 = 0;
        float tempsJeu;
        Queue<GameData> listBoule;
        SoundEffect sonBouton;

        public Multi(Game1 parent, BouleManager manager, Chariot chariot)
        {
            listBoule = new Queue<GameData>();
            this.chariot = chariot;
            posTxt = new Vector2((Game1.FENETRE.Width / 2), Game1.FENETRE.Height / 2);
            posWinnerLogo = new Vector2(0, 0);
            keyPlayer = new Microsoft.Xna.Framework.Input.Keys[8];
            keyPlayer[0] = Microsoft.Xna.Framework.Input.Keys.F1;
            keyPlayer[1] = Microsoft.Xna.Framework.Input.Keys.F2;
            keyPlayer[2] = Microsoft.Xna.Framework.Input.Keys.F3;
            keyPlayer[3] = Microsoft.Xna.Framework.Input.Keys.F4;
            keyPlayer[4] = Microsoft.Xna.Framework.Input.Keys.F5;
            keyPlayer[5] = Microsoft.Xna.Framework.Input.Keys.F6;
            keyPlayer[6] = Microsoft.Xna.Framework.Input.Keys.F7;
            keyPlayer[7] = Microsoft.Xna.Framework.Input.Keys.F8;

            v2TabPosJoueurData = new Vector2[6];
            iTabPosJoueurData = new int[6];
            iTabPosJoueurData[0] = 200;
            iTabPosJoueurData[1] = 250;
            iTabPosJoueurData[2] = 240;
            iTabPosJoueurData[3] = 220;
            iTabPosJoueurData[4] = 245;
            iTabPosJoueurData[5] = 275;

            v2TabPosJoueurData[0] = new Vector2(iTabPosJoueurData[0], 10);
            v2TabPosJoueurData[1] = new Vector2(iTabPosJoueurData[0], 50);
            v2TabPosJoueurData[2] = new Vector2(iTabPosJoueurData[0], 70);
            v2TabPosJoueurData[3] = new Vector2(iTabPosJoueurData[0], 30);
            v2TabPosJoueurData[4] = new Vector2(iTabPosJoueurData[0], 30);
            v2TabPosJoueurData[5] = new Vector2(iTabPosJoueurData[0], 30);

            this.manager = manager;
            joueurData = new Dictionary<string, JoueurData>();
            this.parent = parent;
        }

        public void loadContent(ContentManager content)
        {
            sonBouton = content.Load<SoundEffect>("son/bouton");
            fontDecompte = content.Load<SpriteFont>("font/candy");
            fontDataPlayer = content.Load<SpriteFont>("font/joueurdatafont");
            t2DRuban = content.Load<Texture2D>("images/multi/barre");
            t2DCercle = content.Load<Texture2D>("images/multi/cercle");
            t2DMenu = content.Load<Texture2D>("images/gameOver/menu");
            t2DWinner = content.Load<Texture2D>("images/multi/winner");
            t2DGameOver = content.Load<Texture2D>("images/multi/gameOver");
            posBoutonMenu = new Vector2(Game1.FENETRE.Width / 2 - t2DMenu.Width / 2, Game1.FENETRE.Height / 2 + t2DMenu.Height/2);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < iTabPosJoueurData.Length; i++)
                v2TabPosJoueurData[i].X = iTabPosJoueurData[i];
            
            foreach (KeyValuePair<string, JoueurData> joueur in joueurData)
            {
                spriteBatch.Draw(t2DCercle, v2TabPosJoueurData[0], null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(fontDataPlayer, joueur.Key, joueur.Value.pseudoPosition, Color.White);
                spriteBatch.DrawString(fontDataPlayer, joueur.Value.Score, v2TabPosJoueurData[2], Color.White);
                spriteBatch.DrawString(fontDataPlayer, joueur.Value.Vie, v2TabPosJoueurData[3], Color.Red);
                spriteBatch.DrawString(fontDataPlayer, joueur.Value.Key+"", v2TabPosJoueurData[4], Color.White);
                spriteBatch.DrawString(fontDataPlayer, joueur.Value.Bonus, v2TabPosJoueurData[5], Color.Yellow);

                if(joueur.Value.Finish == "True")
                    spriteBatch.Draw(t2DGameOver, v2TabPosJoueurData[0], null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                for (int i = 0; i < iTabPosJoueurData.Length; i++)
                    v2TabPosJoueurData[i].X += 110;
            }

            if (bDecompte)
                spriteBatch.DrawString(fontDecompte, txtCpt, posTxt, Color.White);

            if (bFinishGame)
            {
                spriteBatch.DrawString(fontDecompte, vainqueur, posVainqueur, Color.White);
                spriteBatch.Draw(t2DMenu, posBoutonMenu, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(t2DWinner, posWinnerLogo, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }

        public void sendFinish()
        {
            client.SendData("5:" + pseudo + ";");
            parent.gameState = GameState.GameOverMulti;
        }

        public void addBonus(int idBonus)
        {
            this.idBonus = idBonus;
            bonusDispo = true;
        }

        public void receiveBonus(int idBonus)
        {
            manager.activeBonus(idBonus);
        }

        public void handleKeyboard(KeyboardState state)
        {
            foreach (KeyValuePair<string, JoueurData> joueur in joueurData)
            {
                if (state.IsKeyDown(joueur.Value.Key) && joueur.Value.Finish == "False")
                {
                    if(bonusDispo)
                    {
                        client.SendData("3:" + joueur.Key + "#" + idBonus + ";");
                        bonusDispo = false;
                        manager.removeBonusDispo();
                    }
                }
            }
        }

        public void update(GameTime gameTime)
        {
            if (runningGame)
            {
                generateBoules(gameTime);
                tempsJeu += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
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
                GameData data = listBoule.Dequeue();
                manager.launchBoule(0, data.Boule, 1, data.W, data.H);
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
                    GameData data = listBoule.Dequeue();
                    manager.launchBoule(1, data.Boule, 1, data.W, data.H);
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
                    GameData data = listBoule.Dequeue();
                    manager.launchBoule(2, data.Boule, -1, data.W, data.H);
                    timer3 = 0;
                }
            }

            if (timer4 < VitesseTimer4)
            {
                timer4 += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                GameData data = listBoule.Dequeue();
                manager.launchBoule(3, data.Boule, -1, data.W, data.H);
                timer4 = 0;
            }

        }
        public void update(MouseState ms, MouseState lastms)
        {
            if (ms.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released && lastms.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                if (ms.X > posBoutonMenu.X && ms.X < posBoutonMenu.X + t2DMenu.Width &&
                    ms.Y > posBoutonMenu.Y && ms.Y < posBoutonMenu.Y + t2DMenu.Height)
                {
                    if(type == 0)
                    {
                        server.stopServer();
                    }

                    client.close();
                    if (chariot.IsExplose)
                        chariot.reset();

                    parent.gameState = GameState.Menu;
                    bDecompte = true;
                    bFinishGame = false;
                    bonusDispo = false;
                    joueurData = new Dictionary<string, JoueurData>();
                    idBonus = 0;
                    txtCpt = "";
                    manager.reset();
                    vainqueur = "";
                    sonBouton.Play();
                    parent.startSon();
                }
            }
        }

        public void start()
        {
            listBoule.Clear();
            timer1 = 0;
            timer2 = 0;
            timer3 = 0;
            timer4 = 0;
            tempsJeu = 0;
            VitesseTimer1 = 1;
            VitesseTimer2 = 6;
            VitesseTimer3 = 4;
            VitesseTimer4 = 3;
            FormMulti form;
            string message = "";
            manager.reset();
            do
            {
                form = new FormMulti(message, pseudo, ip, type);
                form.ShowDialog();
                if (form.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    pseudo = form.Pseudo;
                    ip = form.IP;
                    type = form.Type;

                    if (type == 0)
                    {
                        try
                        {
                            server = new Server(ip, this);
                        }
                        catch (Exception e)
                        {
                            form.Erreur = true;
                            message = "impossible de lancer le serveur!";
                            continue;
                        }

                        form.Dispose();
                        formS = new FormSession();
                        client = new Client(ip, pseudo, this);
                        formS.ShowDialog();

                        if (formS.DialogResult == System.Windows.Forms.DialogResult.OK)
                        {
                            parent.gameState = GameState.Multi;
                            server.startGame();
                            parent.startSonJeu();
                        }
                        else
                        {
                            server.stopServer();
                        }
                    }
                    else
                    {
                        try
                        {
                            client = new Client(ip, pseudo, this);
                            parent.gameState = GameState.Multi;
                        }
                        catch (Exception e)
                        {
                            form.Erreur = true;
                            message = "impossible de contacter le serveur!";
                        }
                    }
                }
            } while (form.Erreur);
        }

        public void decompte(string cpt)
        {
            txtCpt = cpt;
            posTxt.X = Game1.FENETRE.Width / 2 - (fontDecompte.MeasureString(txtCpt).X / 2);
            if (txtCpt == "-1")
                bDecompte = false;
        }

        public string getPlayerData()
        {
            return manager.getPlayerData();
        }

        public void setDataPlayer(string data)
        {
            string[] values = data.Split('#');
            for (int i = 0; i < values.Length; i++)
            {
                joueurData[values[0]].setData(values[1], values[2], values[3], values[4]);
                
            }
            //Debug.WriteLine(data);
        }

        public void sendWinner()
        {
            client.SendData("7:" + pseudo + ";");
            finishGame(pseudo);
        }

        public void finishGameWinner(string pseudo)
        {
            chariot.explose();
            finishGame(pseudo);
        }

        public void finishGame(string pseudoGagnant)
        {
            runningGame = false;
            parent.gameState = GameState.GameOverMulti;
            bFinishGame = true;
            this.vainqueur = pseudoGagnant;
            posVainqueur.X = Game1.FENETRE.Width/2 - fontDecompte.MeasureString(pseudoGagnant).X / 2;
            posVainqueur.Y = Game1.FENETRE.Height/2 - fontDecompte.MeasureString(pseudoGagnant).Y;
            posWinnerLogo.X = Game1.FENETRE.Width / 2 + fontDecompte.MeasureString(pseudoGagnant).X / 2 - t2DWinner.Width/2;
            posWinnerLogo.Y = Game1.FENETRE.Height / 2 - fontDecompte.MeasureString(pseudoGagnant).Y/2;
        }

        public void initPlayer(string data)
        {
            Debug.WriteLine(data);
            
            string[] values = data.Split('#');
            for (int i = 0; i < values.Length; i++)
                joueurData.Add(values[i], new JoueurData(values[i], i, fontDataPlayer, keyPlayer[i]));

            client.start();
        }

        public void setConnection(string data)
        {
            formS.setConnection(data);
        }

        public void setGame(string data)
        {
            Debug.WriteLine(data);
            string[] values = data.Split('a');
            //Debug.WriteLine(data);
            //parent.launchBoule(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3]), int.Parse(values[4]));
            //parent.launchBoule(0, 2, 1, 4, 7);
            for(int i = 0; i < values.Length-1; i++)
            {
                string[] d = values[i].Split('#');
                listBoule.Enqueue(new GameData(int.Parse(d[0]), int.Parse(d[1]), int.Parse(d[2]), int.Parse(d[3]), int.Parse(d[4])));
            }
            //Debug.WriteLine("boule " + listBoule.ElementAt(0).Boule+"");
            runningGame = true;
        }
    }
}
