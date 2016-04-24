using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGameMPE.Core;
using MonoGameMPE.Core.Modifiers;
using MonoGameMPE.Core.Modifiers.Container;
using MonoGameMPE.Core.Profiles;
using System;

namespace Game1
{
    public enum GameState
    {
        Menu,
        Multi,
        Game,
        GameOver,
        GameOverMulti
    };

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Rectangle FENETRE;
        public static float METERINPIXEL = 64f;
        public static string CLASSEMENT_PATH = @"c:\temp\classement.txt";
        public const string THEME = "wood";
        const int MOUSE_HEIGT = 35;
        const int MOUSE_WIDTH = 30;
        const int MOUSE_WIDTH_2 = MOUSE_WIDTH / 2;
        const int MOUSE_HEIGHT_2 = MOUSE_HEIGT / 2;

        Texture2D t2Dwall, t2Dmouse;
        Vector2 posFps;
        World world;
        Body sol, gauche, droite;
        Chariot chariot;
        Canon canon;
        BouleManager manager;

        KeyboardState kbState;
        KeyboardState kbLastState;
        MouseState ms, lastms;

        Rectangle rectMouse;
        Rectangle rectWallpaper;
        Menu menu;
        GameOver gameOver;
        Multi multi;
        Arcade arcade;
        public GameState gameState{ get; set; }
        private FrameCounter _frameCounter;
        SpriteFont fontInfo;
        Song sonMenu, sonJeu;
        SongCollection songCollection;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            //Creation d'un nouveau monde en lui assignant une gravité
            world = new World(new Vector2(0, 20));
            gameState = GameState.Menu;
            _frameCounter = new FrameCounter();

    }

        public void launchBoule(int idCanon, int idBoule, int direction, int maxW, int maxH)
        {
            manager.launchBoule(idCanon, idBoule, direction, maxW, maxH);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 1000;
            //pour fullscreen
            //graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            FENETRE = this.Window.ClientBounds;
            this.Window.Position = new Point(0, 0);

            rectWallpaper = new Rectangle(0, 0, FENETRE.Width, FENETRE.Height);
            rectMouse = new Rectangle(0, 0, MOUSE_WIDTH, MOUSE_HEIGT);
            menu = new Menu(this);
            gameOver = new GameOver(this);

            #region Définition des bords
            sol = BodyFactory.CreateRectangle(world, FENETRE.Width, 1f / METERINPIXEL, 1f, new Vector2(0, FENETRE.Height / METERINPIXEL));
            sol.BodyType = BodyType.Static;
            sol.Friction = 1f;
            sol.Restitution = 0.1f;
            sol.CollisionCategories = Category.Cat4;

            gauche = BodyFactory.CreateRectangle(world, 1f / METERINPIXEL, FENETRE.Height, 1f, new Vector2(0, 0));
            gauche.BodyType = BodyType.Static;
            gauche.Friction = 0.5f;
            gauche.Restitution = 0.5f;
            gauche.CollisionCategories = Category.Cat1;

            droite = BodyFactory.CreateRectangle(world, 1f / METERINPIXEL, FENETRE.Height, 1f, new Vector2(FENETRE.Width / METERINPIXEL, 0));
            droite.BodyType = BodyType.Static;
            droite.Friction = 0.5f;
            droite.Restitution = 0.5f;
            droite.CollisionCategories = Category.Cat1;
            #endregion

            posFps = new Vector2(FENETRE.Width - 100, 10);

            chariot = new Chariot(world);
            canon = new Canon();
            manager = new BouleManager(world, canon.getPosCanon(), sol, chariot, this);
            arcade = new Arcade(manager);
            multi = new Multi(this, manager, chariot);
            
            MediaPlayer.IsRepeating = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            fontInfo = Content.Load<SpriteFont>("font/info");
            t2Dwall = Content.Load<Texture2D>("images/wood/wall");
            t2Dmouse = Content.Load<Texture2D>("images/mouse");
            sonMenu = Content.Load<Song>("son/menu");
            sonJeu = Content.Load<Song>("son/jeu");

            menu.loadContent(Content);
            gameOver.loadContent(Content);
            canon.loadContent(Content);
            chariot.loadContent(Content);
            manager.loadContent(Content);
            arcade.loadContent(Content);
            multi.loadContent(Content);
            
            MediaPlayer.Play(sonMenu);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            ms = Mouse.GetState();
            rectMouse.X = ms.X - MOUSE_WIDTH_2;
            rectMouse.Y = ms.Y - MOUSE_HEIGHT_2;
            
            if (gameState == GameState.Menu)
            {
                menu.update(ms, lastms);
                lastms = ms;
            }

            if (gameState == GameState.GameOver)
            {
                gameOver.update(ms, lastms);
                lastms = ms;
            }

            if (gameState == GameState.GameOverMulti)
            {
                multi.update(ms, lastms);
                lastms = ms;
            }

            if (gameState == GameState.Multi)
            {
                multi.update(gameTime);
            }

            if (gameState != GameState.GameOver && gameState != GameState.GameOverMulti)
                HandleKeyboard();

            if (gameState == GameState.Game)
                arcade.update(gameTime);

            chariot.update(gameTime.ElapsedGameTime);
            manager.update(gameTime);

            world.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f);
            base.Update(gameTime);
        }

        private void HandleKeyboard()
        {
            kbState = Keyboard.GetState();

            kbLastState = kbState;

            if (kbState.IsKeyDown(Keys.Left))
            {
                chariot.move(-1);
            }

            if (kbState.IsKeyDown(Keys.Right))
            {
                chariot.move(1);
            }

            if (kbState.IsKeyDown(Keys.Down))
            {
                chariot.frein(1);
            }

            if (kbState.IsKeyDown(Keys.Space))
            {
                //manager.launchBoule();
                manager.launchBoule(1, 5, 1, 3, 3);
            }

            if(gameState == GameState.Multi)
            {
                multi.handleKeyboard(kbState);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _frameCounter.Update(deltaTime);

            string fps = ""+ Math.Round(_frameCounter.AverageFramesPerSecond);

            spriteBatch.Begin();

            if (gameState == GameState.Menu)
            {
                menu.draw(spriteBatch);
                spriteBatch.Draw(t2Dmouse, rectMouse, Color.White);
            }

            if(gameState == GameState.Game)
            {
                spriteBatch.Draw(t2Dwall, rectWallpaper, Color.White);
                canon.draw(spriteBatch);
                manager.draw(spriteBatch);
                chariot.draw(spriteBatch);
                arcade.draw(spriteBatch);
            }

            if (gameState == GameState.Multi)
            {
                spriteBatch.Draw(t2Dwall, rectWallpaper, Color.White);
                canon.draw(spriteBatch);
                manager.draw(spriteBatch);
                chariot.draw(spriteBatch);
                multi.draw(spriteBatch);
            }

            if (gameState == GameState.GameOver)
            {
                spriteBatch.Draw(t2Dwall, rectWallpaper, Color.White);
                gameOver.draw(spriteBatch);
                canon.draw(spriteBatch);
                manager.draw(spriteBatch);
                chariot.draw(spriteBatch);
                spriteBatch.Draw(t2Dmouse, rectMouse, Color.White);
            }

            if (gameState == GameState.GameOverMulti)
            {
                spriteBatch.Draw(t2Dwall, rectWallpaper, Color.White);
                canon.draw(spriteBatch);
                manager.draw(spriteBatch);
                chariot.draw(spriteBatch);
                multi.draw(spriteBatch);
                spriteBatch.Draw(t2Dmouse, rectMouse, Color.White);
            }

            spriteBatch.DrawString(fontInfo, fps, posFps, Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void startArcade()
        {
            gameState = GameState.Game;
            arcade.start();
            startSonJeu();
        }

        public void replay()
        {
            chariot.reset();
            gameState = GameState.Game;
            arcade.start();
            startSonJeu();
        }

        public void backToMenu()
        {
            chariot.reset();
            gameState = GameState.Menu;
            startSon();
        }

        public void startMulti()
        {
            multi.start();
        }

        public void addBonus(int id)
        {
            multi.addBonus(id);
        }

        public void startSettings()
        {
        }

        public void gameOverMulti()
        {
            multi.sendFinish();
        }

        public void sendWinner()
        {
            multi.sendWinner();
        }

        public void startSon()
        {
            MediaPlayer.Play(sonMenu);
        }

        public void startSonJeu()
        {
            MediaPlayer.Play(sonJeu);
        }

        public void save()
        {
            FormArcade form = new FormArcade(manager.score);
            form.ShowDialog();
            if (form.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                chariot.reset();
                gameState = GameState.Menu;
                startSon();
            }
            System.Diagnostics.Debug.WriteLine(form.Pseudo);
        }      
    }
}
