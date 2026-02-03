using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TRON_Assignment___v4
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum GameState
        {
            StartScreen,
            Countdown,
            Playing,
            GameOver
        }
        enum Direction
        {
            Up, 
            Down, 
            Left, 
            Right
        }
        GameState currentState;
        SpriteFont font;
        Vector2 bike1Pos;
        Vector2 bike2Pos;
        Direction bike1Dir;
        Direction bike2Dir;
        const float BIKE_SPEED = 3f;
        float countDownTimer;
        int countDownValue;
        Random rng;

        List<Rectangle> bike1Trail;
        List<Rectangle> bike2Trail;

        const int TRAIL_SIZE = 5;

        const int BIKE_WIDTH = 18;
        const int BIKE_HEIGHT = 8;
        const float BIKE_COLLISION_DISTANCE = 10f;


        Texture2D pixelTexture;

        Rectangle bike1Rect;
        Rectangle bike2Rect;

        int IGNORE_SEGMENTS;



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
            currentState = GameState.StartScreen;
            rng = new Random();
            countDownTimer = 0f;
            countDownValue = 3;
            IGNORE_SEGMENTS = 5;

            int side = rng.Next(2);

            if (side == 0)
            {
                bike1Pos = new Vector2(rng.Next(GraphicsDevice.Viewport.Width), 0);
                bike2Pos = new Vector2(rng.Next(GraphicsDevice.Viewport.Width), GraphicsDevice.Viewport.Height - 1);


                bike1Dir = Direction.Down;
                bike2Dir = Direction.Up;
            }
            else
            {
                bike1Pos = new Vector2(0, rng.Next(GraphicsDevice.Viewport.Height));
                bike2Pos = new Vector2(GraphicsDevice.Viewport.Width - 1, rng.Next(GraphicsDevice.Viewport.Height));


                bike1Dir = Direction.Right;
                bike2Dir = Direction.Left;
            }

            bike1Trail = new List<Rectangle>();
            bike2Trail = new List<Rectangle>();


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
            font = this.Content.Load<SpriteFont>("SpriteFont1");
            // TODO: use this.Content to load your game content here


            pixelTexture = this.Content.Load<Texture2D>("white_1x1");
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
            KeyboardState ks = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || ks.IsKeyDown(Keys.Escape))
                this.Exit();

            if (currentState == GameState.StartScreen)
            {
                if (ks.IsKeyDown(Keys.Enter))
                {
                    currentState = GameState.Countdown;
                    countDownValue = 3;
                    countDownTimer = 0f;
                }
            }
            else if (currentState == GameState.Countdown)
            {
                countDownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (countDownTimer >= 1f)
                {
                    countDownValue--;
                    countDownTimer = 0f;

                    if (countDownValue <= 0)
                    {
                        currentState = GameState.Playing;
                    }
                }
            }
            else if (currentState == GameState.Playing)
            {
                if (bike1Dir == Direction.Up)
                {
                    bike1Pos.Y -= BIKE_SPEED;
                }
                if (bike1Dir == Direction.Down)
                {
                    bike1Pos.Y += BIKE_SPEED;
                }
                if (bike1Dir == Direction.Left)
                {
                    bike1Pos.X -= BIKE_SPEED;
                }
                if (bike1Dir == Direction.Right)
                {
                    bike1Pos.X += BIKE_SPEED;
                }



                if (bike2Dir == Direction.Up) {
                    bike2Pos.Y -= BIKE_SPEED;
                }
                if (bike2Dir == Direction.Down)
                {
                    bike2Pos.Y += BIKE_SPEED;
                }


                if (bike2Dir == Direction.Left) {
                    bike2Pos.X -= BIKE_SPEED;
                }
                if (bike2Dir == Direction.Right)
                {
                    bike2Pos.X += BIKE_SPEED;
                }




                if (bike1Dir == Direction.Up || bike1Dir == Direction.Down)
                {
                    if (ks.IsKeyDown(Keys.A)) {
                        bike1Dir = Direction.Left;
                    }
                    if (ks.IsKeyDown(Keys.D))
                    {
                        bike1Dir = Direction.Right;
                    }


                }
                else
                {
                    if (ks.IsKeyDown(Keys.W)) {
                        bike1Dir = Direction.Up;
                    }
                    if (ks.IsKeyDown(Keys.S))
                    {
                        bike1Dir = Direction.Down;
                    }
                }


                if (bike2Dir == Direction.Up || bike2Dir == Direction.Down)
                {
                    if (ks.IsKeyDown(Keys.Left)) {
                        bike2Dir = Direction.Left;
                    }
                    if (ks.IsKeyDown(Keys.Right))
                    {
                        bike2Dir = Direction.Right;
                    }


                }
                else
                {
                    if (ks.IsKeyDown(Keys.Up)) {
                        bike2Dir = Direction.Up;
                    }
                    if (ks.IsKeyDown(Keys.Down))
                    {
                        bike2Dir = Direction.Down;
                    }


                }


                bike1Trail.Add(new Rectangle((int)bike1Pos.X, (int)bike1Pos.Y, TRAIL_SIZE, TRAIL_SIZE));
                bike2Trail.Add(new Rectangle((int)bike2Pos.X, (int)bike2Pos.Y, TRAIL_SIZE, TRAIL_SIZE));


                bike1Rect = new Rectangle((int)bike1Pos.X, (int)bike1Pos.Y, TRAIL_SIZE, TRAIL_SIZE);
                bike2Rect = new Rectangle((int)bike2Pos.X, (int)bike2Pos.Y, TRAIL_SIZE, TRAIL_SIZE);

                if (bike1Pos.X < 0 || bike1Pos.X >= GraphicsDevice.Viewport.Width ||bike1Pos.Y < 0 || bike1Pos.Y >= GraphicsDevice.Viewport.Height)
                {
                    currentState = GameState.GameOver;
                }

                if (bike2Pos.X < 0 || bike2Pos.X >= GraphicsDevice.Viewport.Width || bike2Pos.Y < 0 || bike2Pos.Y >= GraphicsDevice.Viewport.Height)
                {
                    currentState = GameState.GameOver;
                }


                if (Vector2.Distance(bike1Pos, bike2Pos) < BIKE_COLLISION_DISTANCE)
                {
                    currentState = GameState.GameOver;
                }


                for (int i = 0; i < Math.Max(0, bike1Trail.Count - IGNORE_SEGMENTS); i++)
                {
                    if (bike1Rect.Intersects(bike1Trail[i]))
                    {
                        currentState = GameState.GameOver;
                        break;
                    }
                }

                for (int i = 0; i < Math.Max(0, bike2Trail.Count - IGNORE_SEGMENTS); i++)
                {
                    if (bike1Rect.Intersects(bike2Trail[i]))
                    {
                        currentState = GameState.GameOver;
                        break;
                    }
                }

                for (int i = 0; i < Math.Max(0, bike2Trail.Count - IGNORE_SEGMENTS); i++)
                {
                    if (bike2Rect.Intersects(bike2Trail[i]))
                    {
                        currentState = GameState.GameOver;
                        break;
                    }
                }

                for (int i = 0; i < Math.Max(0, bike1Trail.Count - IGNORE_SEGMENTS); i++)
                {
                    if (bike2Rect.Intersects(bike1Trail[i]))
                    {
                        currentState = GameState.GameOver;
                        break;
                    }
                }
            }
            else if (currentState == GameState.GameOver)
            {

                if (ks.IsKeyDown(Keys.Enter))
                {
                    bike1Trail.Clear();
                    bike2Trail.Clear();

                    int side = rng.Next(2);

                    if (side == 0)
                    {
                        bike1Pos = new Vector2(rng.Next(GraphicsDevice.Viewport.Width), 0);
                        bike2Pos = new Vector2(rng.Next(GraphicsDevice.Viewport.Width), GraphicsDevice.Viewport.Height - 1);

                        bike1Dir = Direction.Down;
                        bike2Dir = Direction.Up;
                    }
                    else
                    {
                        bike1Pos = new Vector2(0, rng.Next(GraphicsDevice.Viewport.Height));
                        bike2Pos = new Vector2(GraphicsDevice.Viewport.Width - 1, rng.Next(GraphicsDevice.Viewport.Height));

                        bike1Dir = Direction.Right;
                        bike2Dir = Direction.Left;
                    }

                    countDownValue = 3;
                    countDownTimer = 0f;
                    currentState = GameState.Countdown;
                }
            }


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if (currentState == GameState.StartScreen)
            {
                GraphicsDevice.Clear(Color.White);
                spriteBatch.DrawString(font, "TRON\n\nPlayer 1: WASD\nPlayer 2: Arrow Keys\n\nPress Enter to Start", new Vector2(50, 50), Color.Black);
            }
            else if (currentState == GameState.Countdown)
            {
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.DrawString(font, "Get Ready!", new Vector2(50, 50), Color.White);

                spriteBatch.DrawString(font, countDownValue.ToString(), new Vector2(GraphicsDevice.Viewport.Width / 2 - 10, GraphicsDevice.Viewport.Height / 2 - 20), Color.White);
            }
            else
            {

                foreach (Rectangle rect in bike1Trail)
                {
                    spriteBatch.Draw(pixelTexture, rect, Color.Blue);
                }

                foreach (Rectangle rect in bike2Trail)
                {
                    spriteBatch.Draw(pixelTexture, rect, Color.Red);
                }

                spriteBatch.Draw(pixelTexture,bike1Pos, null, Color.Blue, GetRotation(bike1Dir), Vector2.Zero, new Vector2(BIKE_WIDTH, BIKE_HEIGHT), SpriteEffects.None, 0f);
                spriteBatch.Draw(pixelTexture,bike2Pos, null, Color.Red, GetRotation(bike2Dir), Vector2.Zero, new Vector2(BIKE_WIDTH, BIKE_HEIGHT), SpriteEffects.None, 0f);

                if (currentState == GameState.GameOver)
                {
                    spriteBatch.DrawString(font, "GAME OVER\nPress Enter to Restart", new Vector2(50, 50), Color.White);
                }
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }

        float GetRotation(Direction dir)
        {
            if (dir == Direction.Right)
            {
                return 0f;
            }
            if (dir == Direction.Down)
            {
                return (float)(3.1459/2);
            }
            if (dir == Direction.Left)
            {
                return MathHelper.Pi;
            }
            return (float)(3.1459);
        }

    }
}
