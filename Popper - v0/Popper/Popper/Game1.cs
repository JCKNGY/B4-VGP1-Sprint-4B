using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Popper
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rectangle window;
        Texture2D unpoppedTex;
        Texture2D poppedTex;

        List<Rectangle> kernels;
        List<Vector2> velocities;
        List<Texture2D> images;


        List<int> popTimers;
        Random rng;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            int screenWidth = graphics.GraphicsDevice.Viewport.Width;
            int screenHeight = graphics.GraphicsDevice.Viewport.Height;
            window = new Rectangle(0, 0, screenWidth, screenHeight);

            popTimers = new List<int>();
            rng = new Random();

            kernels = new List<Rectangle>();
            velocities = new List<Vector2>();
            images = new List<Texture2D>();

            kernels.Add(new Rectangle(70, 50, 15, 15));
            velocities.Add(new Vector2(2, 3));
            popTimers.Add(0);

            kernels.Add(new Rectangle(70, 110, 15, 15));
            velocities.Add(new Vector2(2, 3));
            popTimers.Add(0);

            

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

            // TODO: use this.Content to load your game content here
            unpoppedTex = Content.Load<Texture2D>("unpopped");
            poppedTex = Content.Load<Texture2D>("popped");

            images.Add(unpoppedTex);
            images.Add(unpoppedTex);

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            for (int i = 0; i < kernels.Count; i++)
            {
                int x = kernels[i].X + (int)velocities[i].X;
                int y = kernels[i].Y + (int)velocities[i].Y;
                kernels[i] = new Rectangle(x, y, kernels[i].Width, kernels[i].Height);
                if (kernels[i].Y + kernels[i].Height >= window.Bottom)
                {
                    kernels[i] = new Rectangle(kernels[i].X, window.Bottom - kernels[i].Height, kernels[i].Width, kernels[i].Height);
                    velocities[i] = new Vector2(velocities[i].X, velocities[i].Y * -1);
                }
                if (kernels[i].Top <= window.Top)
                {
                    kernels[i] = new Rectangle(kernels[i].X, window.Top, kernels[i].Width, kernels[i].Height);
                    velocities[i] = new Vector2(velocities[i].X, velocities[i].Y * -1);
                }

                if (kernels[i].Right >= window.Right)
                {
                    kernels[i] = new Rectangle(window.Right - kernels[i].Width, kernels[i].Y, kernels[i].Width, kernels[i].Height);
                    velocities[i] = new Vector2(velocities[i].X * -1, velocities[i].Y);
                }

                if (kernels[i].Left <= window.Left)
                {
                    kernels[i] = new Rectangle(window.Left, kernels[i].Y, kernels[i].Width, kernels[i].Height);
                    velocities[i] = new Vector2(velocities[i].X * -1, velocities[i].Y);
                }



            }
            for (int a = 0; a < kernels.Count; a++)
            {
                for (int b = a + 1; b < kernels.Count; b++)
                {
                    if (kernels[a].Intersects(kernels[b]))
                    {
                        if (popTimers[a] == 0 && popTimers[b] == 0)
                        {
                            popTimers[a] = 45;
                            popTimers[b] = 45;
                            images[a] = poppedTex;
                            images[b] = poppedTex;
                        }
                    }
                }
            }

            for (int i = kernels.Count - 1; i >= 0; i--)
            {
                if (popTimers[i] == 1)
                {
                    kernels.RemoveAt(i);
                    velocities.RemoveAt(i);
                    images.RemoveAt(i);
                    popTimers.RemoveAt(i);
                }
                else if (popTimers[i] > 1)
                {
                    popTimers[i] = popTimers[i] - 1;
                }
            }

            double dt = gameTime.ElapsedGameTime.TotalSeconds;
            double spawnChance = dt / 2;

            if (rng.NextDouble() < spawnChance)
            {
                int kernelWidth = 15;
                int kernelHeight = 15;

                int spawnX = rng.Next(window.Left, window.Right - kernelWidth);
                int spawnY = rng.Next(window.Top, window.Bottom - kernelHeight);

                int vx = rng.Next(-3, 4);
                int vy = rng.Next(-3, 4);

                if (vx == 0 && vy == 0)
                {
                    vx = 1;
                }

                kernels.Add(new Rectangle(spawnX, spawnY, kernelWidth, kernelHeight));
                velocities.Add(new Vector2(vx, vy));
                images.Add(unpoppedTex);
                popTimers.Add(0);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            for (int i = 0; i < kernels.Count; i++)
            {
                spriteBatch.Draw(images[i], kernels[i], Color.White);
            }
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
