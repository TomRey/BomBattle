using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameMPE.Core;
using MonoGameMPE.Core.Modifiers;
using MonoGameMPE.Core.Modifiers.Container;
using MonoGameMPE.Core.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class Boule
    {
        Body bBoule;
        int idBoule = 0;
        int idText2D = 0;
        Random rdmPos;
        bool sleep = true;
        Texture2D t2D;
        Vector2 pos, bouleOrigin;
        const int BOULE_SIZE = 45;

        private ParticleEffect particleEffect;
        private SpriteBatchRenderer particleRenderer;
        Texture2D _blankTexture;

        public Boule(World world, Body sol, Texture2D blankTexture)
        {
            pos = new Vector2(0, 0);
            bouleOrigin = new Vector2(BOULE_SIZE / 2f, BOULE_SIZE / 2f);
            rdmPos = new Random();
            createBody(world, sol);
            _blankTexture = blankTexture;
            _blankTexture.SetData(new[] { Color.White });
        }

        private void createBody(World world, Body sol)
        {
            bBoule = BodyFactory.CreateCircle(world, 50 / (2f * Game1.METERINPIXEL), 1f, pos);
            bBoule.BodyType = BodyType.Dynamic;
            bBoule.Restitution = 0.1f;
            bBoule.Friction = 1f;
            bBoule.Mass = 0.2f;
            bBoule.IsBullet = true;

            bBoule.CollisionCategories = Category.Cat3;
            bBoule.CollidesWith = Category.All;
            bBoule.IgnoreCollisionWith(sol);
            standBy();
        }

        public void setTexture(Texture2D t2D)
        {
            this.t2D = t2D;
        }

        public int getId()
        {
            return idBoule;
        }

        public bool isSleeping()
        {
            return sleep;
        }

        public void standBy()
        {
            sleep = true;
            bBoule.Awake = false;
            bBoule.Enabled = false;
        }

        public void reset(Vector2 position, int id, int direction, int maxW, int maxH, Texture2D texture)
        {
            t2D = texture;
            idText2D = id;
            //ParticleInit();
            sleep = false;
            bBoule.Awake = true;
            bBoule.Enabled = true;

            bBoule.Position = position;

            bBoule.ApplyTorque(direction * 2f);

            bBoule.ApplyLinearImpulse(new Vector2(direction * maxW, -maxH));
        }

        public void update(Chariot chariot, GameTime gameTime, BouleManager parent)
        {
            // particleEffect.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            // particleEffect.Trigger(new Vector(100, 100));
            Body[] chariotBodies = chariot.getBodies();
            if (!sleep)
            {
                if (bBoule.Position.Y > (Game1.FENETRE.Height + BOULE_SIZE) / Game1.METERINPIXEL)
                {
                    standBy();
                }

                if (bBoule.Position.X > chariotBodies[1].Position.X && bBoule.Position.X < chariotBodies[2].Position.X && bBoule.Position.Y > chariotBodies[0].Position.Y - ((BOULE_SIZE + 10) / Game1.METERINPIXEL))
                {
                    standBy();

                    if (idText2D < 6)
                    {
                        parent.addPoint(idText2D);
                    }
                    else
                    {
                        if (idText2D == 6)
                        {
                            System.Diagnostics.Debug.WriteLine("bombe");
                            parent.gameOver();
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("bonus");
                            parent.addBonus();
                        }
                    }
                }
            }
        }

        public void draw(SpriteBatch spriteBatch, SpriteFont fontScore)
        {
            spriteBatch.Draw(t2D, bBoule.Position * Game1.METERINPIXEL, null, Color.White, bBoule.Rotation, bouleOrigin, 1f, SpriteEffects.None, 0f);
            
            //particleRenderer.Draw(particleEffect, spriteBatch);
            
        }

        private void ParticleInit()
        {
            particleRenderer = new SpriteBatchRenderer();

            particleEffect = new ParticleEffect
            {
                Emitters = new[]
                {
                    new Emitter(2000, TimeSpan.FromSeconds(2), Profile.Point())
                    {
                        Texture = _blankTexture,
                        BlendMode = BlendMode.Alpha,
                        Parameters = new ReleaseParameters
                        {
                            Speed = new RangeF(20f, 50f),
                            Quantity = 3,
                            
                        },
                        Modifiers = new IModifier[]
                        {
                            new ColourInterpolator2
                            {
                                InitialColour = new Colour(0.0f, 0.0f, 0.5f),
                                FinalColour = new Colour(0.0f, 0.0f, 0.5f)
                            },
                            new RotationModifier
                            {
                                RotationRate = 1f
                            },
                            new RectContainerModifier
                            {
                                Height = 500,
                                Width = 500,
                            },
                        }
                    }

                }
            };

        }
    }
}
