using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    class TextBox
    {
        public String Text { get; set; }
        public bool Focus { get; set; }
        private KeyboardState currentKeyboardState, lastKeyboardState;
        private Vector2 pos, posText, posLbl;
        private Texture2D t2Dbox, t2DboxFocus;
        private Rectangle rectBox;
        private int maxSize;
        private String label;
        private const int WIDTH = 492;
        Keys[] keysToCheck = new Keys[] {
        Keys.A, Keys.B, Keys.C, Keys.D, Keys.E,
        Keys.F, Keys.G, Keys.H, Keys.I, Keys.J,
        Keys.K, Keys.L, Keys.M, Keys.N, Keys.O,
        Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T,
        Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y,
        Keys.Z, Keys.Back, Keys.D0, Keys.D1,
        Keys.D2, Keys.D3, Keys.D4, Keys.D5,
        Keys.D6, Keys.D7, Keys.D8, Keys.D9,
        Keys.Space, Keys.OemPeriod };

        public TextBox(Vector2 position, int maxSize, string label)
        {
            pos = position;
            init(maxSize, label);
        }

        public TextBox(int y, int maxSize, string label)
        {
            pos = new Vector2(Game1.FENETRE.Width / 2 - WIDTH/2, y);
            init(maxSize, label);
        }

        public Vector2 getPosition()
        {
            return pos;
        }

        public int getWidth()
        {
            return WIDTH;
        }

        private void init(int maxSize, string label)
        {
            this.maxSize = maxSize;
            this.label = label;
            Text = "";
            Focus = false;
            posText = new Vector2(pos.X + 20, pos.Y + 20);
            posLbl = new Vector2(pos.X, pos.Y - 40);
        }

        public void loadContent(ContentManager content)
        {
            t2DboxFocus = content.Load<Texture2D>("images/form/tbxFocus");
            t2Dbox = content.Load<Texture2D>("images/form/tbx");
            rectBox = new Rectangle((int)pos.X, (int)pos.Y, t2Dbox.Width, t2Dbox.Height);
        }

        public void draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if(Focus)
                spriteBatch.Draw(t2DboxFocus, pos, Color.White);
            else
                spriteBatch.Draw(t2Dbox, pos, Color.White);

            spriteBatch.DrawString(font, label, posLbl, Color.Black);
            spriteBatch.DrawString(font, Text, posText, Color.Black);
        }

        public void update(MouseState ms, MouseState lastms)
        {
            if (ms.LeftButton == ButtonState.Released && lastms.LeftButton == ButtonState.Pressed)
            {
                if (ms.X > rectBox.X && ms.X < rectBox.X + rectBox.Width &&
                    ms.Y > rectBox.Y && ms.Y < rectBox.Y + rectBox.Height)
                    Focus = true;
                else
                    Focus = false;
            }

            if (Focus)
            {
                currentKeyboardState = Keyboard.GetState();
                foreach (Keys key in keysToCheck)
                {
                    if (CheckKey(key))
                    {
                        AddKeyToText(key);
                        break;
                    }
                }
                lastKeyboardState = currentKeyboardState;
            }
        }

        private bool CheckKey(Keys theKey)
        {
            return lastKeyboardState.IsKeyDown(theKey) && currentKeyboardState.IsKeyUp(theKey);
        }

        private void AddKeyToText(Keys key)
        {
            string newChar = "";

            if (Text.Length >= maxSize && key != Keys.Back)
                return;

            switch (key)
            {
                case Keys.A:
                    newChar += "a";
                    break;
                case Keys.B:
                    newChar += "b";
                    break;
                case Keys.C:
                    newChar += "c";
                    break;
                case Keys.D:
                    newChar += "d";
                    break;
                case Keys.E:
                    newChar += "e";
                    break;
                case Keys.F:
                    newChar += "f";
                    break;
                case Keys.G:
                    newChar += "g";
                    break;
                case Keys.H:
                    newChar += "h";
                    break;
                case Keys.I:
                    newChar += "i";
                    break;
                case Keys.J:
                    newChar += "j";
                    break;
                case Keys.K:
                    newChar += "k";
                    break;
                case Keys.L:
                    newChar += "l";
                    break;
                case Keys.M:
                    newChar += "m";
                    break;
                case Keys.N:
                    newChar += "n";
                    break;
                case Keys.O:
                    newChar += "o";
                    break;
                case Keys.P:
                    newChar += "p";
                    break;
                case Keys.Q:
                    newChar += "q";
                    break;
                case Keys.R:
                    newChar += "r";
                    break;
                case Keys.S:
                    newChar += "s";
                    break;
                case Keys.T:
                    newChar += "t";
                    break;
                case Keys.U:
                    newChar += "u";
                    break;
                case Keys.V:
                    newChar += "v";
                    break;
                case Keys.W:
                    newChar += "w";
                    break;
                case Keys.X:
                    newChar += "x";
                    break;
                case Keys.Y:
                    newChar += "y";
                    break;
                case Keys.Z:
                    newChar += "z";
                    break;
                case Keys.D0:
                    newChar += "0";
                    break;
                case Keys.D1:
                    newChar += "1";
                    break;
                case Keys.D2:
                    newChar += "2";
                    break;
                case Keys.D3:
                    newChar += "3";
                    break;
                case Keys.D4:
                    newChar += "4";
                    break;
                case Keys.D5:
                    newChar += "5";
                    break;
                case Keys.D6:
                    newChar += "6";
                    break;
                case Keys.D7:
                    newChar += "7";
                    break;
                case Keys.D8:
                    newChar += "8";
                    break;
                case Keys.D9:
                    newChar += "9";
                    break;
                case Keys.OemPeriod:
                    newChar += ".";
                    break;
                case Keys.Space:
                    newChar += " ";
                    break;
                case Keys.Back:
                    if (Text.Length != 0)
                        Text = Text.Remove(Text.Length - 1);
                    return;
            }

            if (currentKeyboardState.IsKeyDown(Keys.RightShift) || currentKeyboardState.IsKeyDown(Keys.LeftShift))
            {
                newChar = newChar.ToUpper();
            }
            Text += newChar;
        }
    }
}
