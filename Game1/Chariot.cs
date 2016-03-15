using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class Chariot
    {
        Texture2D t2Droue, t2Dpanier , CorbeilleGSprite, CorbeilleBSprite;
        Body[] tabBpanier = new Body[3]; //bChariotG, bChariotD, bChariotB, 
        Body bRoueG, bRoueD;

        const int ROUE_SIZE = 50;
        const int PANIER_WIDTH = 200;
        const int PANIER_HEIGHT = 100;
        Vector2 panierOrigin, roueOrigin, impulsion;

        public Chariot(World world)
        {
            createBodies(world);
            panierOrigin = new Vector2(PANIER_WIDTH / 2f, PANIER_HEIGHT);
            roueOrigin = new Vector2(ROUE_SIZE / 2f, ROUE_SIZE / 2f);
            impulsion = new Vector2(2f, 0f);
        }

        public void loadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            t2Dpanier = content.Load<Texture2D>("images/"+ Game1.THEME + "/panier");
            t2Droue = content.Load<Texture2D>("images/" + Game1.THEME + "/roue");
            CorbeilleBSprite = content.Load<Texture2D>("images/CorbeilleB");
            CorbeilleGSprite = content.Load<Texture2D>("images/CorbeilleG");
        }

        private void createBodies(World world)
        {
            #region Body Panier
            //Base
            tabBpanier[0] = BodyFactory.CreateRectangle(world, 200 / Game1.METERINPIXEL, 20 / Game1.METERINPIXEL, 1f, new Vector2(((Game1.FENETRE.Width / 2) - (200 / 2)) / Game1.METERINPIXEL, (Game1.FENETRE.Height - (ROUE_SIZE / 2)) / Game1.METERINPIXEL));
            tabBpanier[0].BodyType = BodyType.Dynamic;
            tabBpanier[0].Friction = 1f;
            tabBpanier[0].CollisionCategories = Category.Cat1;
            tabBpanier[0].CollidesWith = Category.All;

            //Bord gauche
            tabBpanier[1] = BodyFactory.CreateRectangle(world, 10 / Game1.METERINPIXEL, 100 / Game1.METERINPIXEL, 1f, new Vector2((((Game1.FENETRE.Width / 2) - (200 / 2)) - 100) / Game1.METERINPIXEL, (Game1.FENETRE.Height - (ROUE_SIZE / 2) - (100 / 2)) / Game1.METERINPIXEL));
            tabBpanier[1].BodyType = BodyType.Dynamic;
            tabBpanier[1].Friction = 1f;
            tabBpanier[1].CollisionCategories = Category.Cat1;
            tabBpanier[1].CollidesWith = Category.All;

            //Bord droite
            tabBpanier[2] = BodyFactory.CreateRectangle(world, 10 / Game1.METERINPIXEL, 100 / Game1.METERINPIXEL, 1f, new Vector2((((Game1.FENETRE.Width / 2) - (200 / 2)) + 100) / Game1.METERINPIXEL, (Game1.FENETRE.Height - (ROUE_SIZE / 2) - (100 / 2)) / Game1.METERINPIXEL));
            tabBpanier[2].BodyType = BodyType.Dynamic;
            tabBpanier[2].Friction = 1f;
            tabBpanier[2].CollisionCategories = Category.Cat1;
            tabBpanier[2].CollidesWith = Category.All;

            bRoueG = BodyFactory.CreateCircle(world, ROUE_SIZE / (2f * Game1.METERINPIXEL), 1f, new Vector2(((Game1.FENETRE.Width / 2) - (200 / 2) - 60) / Game1.METERINPIXEL, (Game1.FENETRE.Height - (ROUE_SIZE / 2)) / Game1.METERINPIXEL));
            bRoueG.BodyType = BodyType.Dynamic;
            bRoueG.CollisionCategories = Category.Cat2;
            bRoueG.Friction = 1f;

            bRoueD = BodyFactory.CreateCircle(world, ROUE_SIZE / (2f * Game1.METERINPIXEL), 1f, new Vector2(((Game1.FENETRE.Width / 2) - (200 / 2) + 60) / Game1.METERINPIXEL, (Game1.FENETRE.Height - (ROUE_SIZE / 2)) / Game1.METERINPIXEL));
            bRoueD.BodyType = BodyType.Dynamic;
            bRoueD.CollisionCategories = Category.Cat2;
            bRoueD.Friction = 1f;
            #endregion

            #region Joint Panier
            JointFactory.CreateRevoluteJoint(world, tabBpanier[0], bRoueG, Vector2.Zero);
            JointFactory.CreateRevoluteJoint(world, tabBpanier[0], bRoueD, Vector2.Zero);

            JointFactory.CreateWeldJoint(world, tabBpanier[0], tabBpanier[1], new Vector2(tabBpanier[0].LocalCenter.X - (PANIER_WIDTH/ (2f*Game1.METERINPIXEL)), tabBpanier[0].LocalCenter.Y), new Vector2(tabBpanier[1].LocalCenter.X, tabBpanier[1].LocalCenter.Y + (PANIER_HEIGHT / (2f * Game1.METERINPIXEL))));
            JointFactory.CreateWeldJoint(world, tabBpanier[0], tabBpanier[2], new Vector2(tabBpanier[0].LocalCenter.X + (PANIER_WIDTH / (2f * Game1.METERINPIXEL)), tabBpanier[0].LocalCenter.Y), new Vector2(tabBpanier[2].LocalCenter.X, tabBpanier[2].LocalCenter.Y + (PANIER_HEIGHT / (2f * Game1.METERINPIXEL))));
            /*JointFactory.CreateRevoluteJoint(world, tabBpanier[0], tabBpanier[1], Vector2.Zero);
            JointFactory.CreateRevoluteJoint(world, tabBpanier[0], tabBpanier[2], Vector2.Zero);
            JointFactory.CreateAngleJoint(world, tabBpanier[0], tabBpanier[1]);
            JointFactory.CreateAngleJoint(world, tabBpanier[0], tabBpanier[2]);*/
            #endregion
        }

        public void move(int direction)
        {
            /*bRoueG.FixedRotation = false;
            bRoueD.FixedRotation = false;*/
            bRoueG.ApplyTorque(direction * 8f);
            bRoueD.ApplyTorque(direction * 8f);

            tabBpanier[0].ApplyLinearImpulse(impulsion * direction);
        }

        public void frein(int direction)
        {
            /*bRoueG.FixedRotation = true;
            bRoueD.FixedRotation = true;*/

            tabBpanier[0].ApplyForce(new Vector2(0f, 50f));
        }

        public void draw(SpriteBatch spriteBatch)
        {
            //Debug
            /*
            spriteBatch.Draw(CorbeilleBSprite, tabBpanier[0].Position * Game1.METERINPIXEL, null, Color.White, tabBpanier[0].Rotation, new Vector2(CorbeilleBSprite.Width / 2f, CorbeilleBSprite.Height / 2f), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(CorbeilleGSprite, tabBpanier[1].Position * Game1.METERINPIXEL, null, Color.White, tabBpanier[0].Rotation, new Vector2(CorbeilleGSprite.Width / 2f, CorbeilleGSprite.Height / 2f), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(CorbeilleGSprite, tabBpanier[2].Position * Game1.METERINPIXEL, null, Color.White, tabBpanier[0].Rotation, new Vector2(CorbeilleGSprite.Width / 2f, CorbeilleGSprite.Height / 2f), 1f, SpriteEffects.None, 0f);
            */
            spriteBatch.Draw(t2Dpanier, tabBpanier[0].Position * Game1.METERINPIXEL, null, Color.White, tabBpanier[0].Rotation, panierOrigin, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(t2Droue, bRoueG.Position * Game1.METERINPIXEL, null, Color.White, bRoueG.Rotation, roueOrigin, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(t2Droue, bRoueD.Position * Game1.METERINPIXEL, null, Color.White, bRoueD.Rotation, roueOrigin, 1f, SpriteEffects.None, 0f);
        }

        public Body[] getBodies()
        {
            return tabBpanier;
        }
    }
}
