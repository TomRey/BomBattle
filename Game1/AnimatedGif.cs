using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class AnimatedGif
    {
        /// <summary>
        /// Duration of Each Frame
        /// </summary>
        public double FrameDuration { get; set; }

        /// <summary>
        /// Number of Frames
        /// </summary>
        public uint StateCount { get; set; }

        /// <summary>
        /// Path of Image Resource
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Position on Screen
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Gets or sets whether animation should be looped
        /// </summary>
        public bool Loop { get; set; }

        public bool CenterY { get; set; }
        /// <summary>
        /// Format of File Path
        /// </summary>
        public string FilePathFormat { get; set; }

        /// <summary>
        /// Stores all Frames in Textures
        /// </summary>
        private Texture2D[] textures;

        /// <summary>
        /// Time elapsed since last state change
        /// </summary>
        private double timeSinceLastStateChange;

        /// <summary>
        /// Current State
        /// </summary>
        private int state;
        private bool active = false;

        private int FRAME_WIDTH, FRAME_HEIGHT;

        public AnimatedGif(string Path, uint stateCount, bool centerY)
        {
            FrameDuration = 0.05;
            Position = new Vector2(Game1.FENETRE.Width/2, 800);
            Loop = true;
            FilePathFormat = "{0}{1}";
            this.Path = Path;
            this.StateCount = stateCount;
            state = (int)stateCount + 1;
            CenterY = centerY;
        }

        public void Load(ContentManager content)
        {
            uint i;
            textures = new Texture2D[StateCount];
            for (i = 0; i < StateCount; i++)
            {
                textures[i] = content.Load<Texture2D>(string.Format(FilePathFormat, Path, i));
            }
            FRAME_WIDTH = textures[0].Width/2;
            FRAME_HEIGHT = textures[0].Height;
            if (CenterY)
                FRAME_HEIGHT /= 2;
        }

        public void start()
        {
            active = true;
            state = 0;
        }

        public void stop()
        {
            active = false;
        }

        public void Update(TimeSpan elapsedGameTime, float X, float Y)
        {
            if (active)
            {
                Position.X = X * Game1.METERINPIXEL - FRAME_WIDTH;
                Position.Y = Y * Game1.METERINPIXEL - FRAME_HEIGHT;
                timeSinceLastStateChange += elapsedGameTime.TotalSeconds;
                if (timeSinceLastStateChange > FrameDuration)
                {
                    timeSinceLastStateChange -= FrameDuration;
                    state++;
                }
                if (Loop && state == StateCount)
                {
                    state = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (state < StateCount && active)
            {
                spriteBatch.Draw(textures[state], Position, Color.White);
            }
        }

    }
}
