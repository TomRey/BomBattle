using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameMPE.Core;
using MonoGameMPE.Core.Modifiers;
using MonoGameMPE.Core.Modifiers.Container;
using MonoGameMPE.Core.Profiles;
using System;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Rectangle FENETRE;
        public static float METERINPIXEL = 64f;
        public const string THEME = "wood";
        const int MOUSE_HEIGT = 35;
        const int MOUSE_WIDTH = 30;
        const int MOUSE_WIDTH_2 = MOUSE_WIDTH / 2;
        const int MOUSE_HEIGHT_2 = MOUSE_HEIGT / 2;
        Texture2D t2Dwall, t2Dmouse;
        World world;
        Body sol, gauche, droite;
        Chariot chariot;
        Canon canon;
        BouleManager manager;

        KeyboardState kbState;
        KeyboardState kbLastState;
        MouseState ms;

        Rectangle rectWallpaper, rectMouse;

        GameState state;

        enum GameState
        {
            Menu,
            MultiJoueur,
            Options,
            Jeu
        };

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            //Creation d'un nouveau monde en lui assignant une gravité
            world = new World(new Vector2(0, 20));
            state = GameState.Menu;
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
            graphics.PreferredBackBufferWidth = 1920;//System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            graphics.PreferredBackBufferHeight = 1000;//System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            FENETRE = this.Window.ClientBounds;
            this.Window.Position = new Point(0, 0);

            rectWallpaper = new Rectangle(0, 0, FENETRE.Width, FENETRE.Height);
            rectMouse = new Rectangle(0, 0, MOUSE_WIDTH, MOUSE_HEIGT);


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

            chariot = new Chariot(world);
            canon = new Canon();
            manager = new BouleManager(world, canon.getPosCanon(), sol, chariot.getBodies(), new Texture2D(GraphicsDevice, 1, 1));

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
            t2Dwall = Content.Load<Texture2D>("images/"+ THEME + "/wall");
            t2Dmouse = Content.Load<Texture2D>("images/mouse");
            canon.loadContent(Content);
            chariot.loadContent(Content);
            manager.loadContent(Content);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            ms = Mouse.GetState();
            rectMouse.X = ms.X - MOUSE_WIDTH_2;
            rectMouse.Y = ms.Y - MOUSE_HEIGHT_2;

            HandleKeyboard();

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
                manager.launchBoule();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(t2Dwall, rectWallpaper, Color.White);
            canon.draw(spriteBatch);
            spriteBatch.End();
            manager.draw(spriteBatch);
            spriteBatch.Begin();
            chariot.draw(spriteBatch);
            spriteBatch.Draw(t2Dmouse, rectMouse, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
