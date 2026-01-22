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

namespace Space_Invaders_Part_1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D robot;

        int animationStage = 1, movement = 0;
        bool direction = true;
        int elevation = 100;
        double time, pastTime;
        List<Rectangle> animation = new List<Rectangle>();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "Invaders";
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.ApplyChanges();
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
            animation.Add(new Rectangle(0, 0, 50, 37));
            animation.Add(new Rectangle(50, 0, 50, 37));


            pastTime = 0;


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
            robot = this.Content.Load<Texture2D>("Space Invaders SS #1");


            // TODO: use this.Content to load your game content here
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
            time += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (animationStage > 0 && pastTime < time)
            {
                animationStage = 0;
            }
            else if (pastTime < time)
            {
                animationStage += 1;
            }


            // TODO: Add your update logic here
            if (pastTime < time)
            {
                pastTime = time + 300;
            }

            if(movement < 350 && direction)
            {
                movement += 5;
            }
            else 
            {
                if(direction == true && elevation < 950)
                {
                    elevation += 50;
                }
                
                direction = false;

            }

            if (movement > -50 && !direction)
            {
                movement -= 5;
            }
            else
            {
                

                if (direction == false && elevation < 950)
                {
                    elevation += 50;
                }

                direction = true;
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            for(int i = 0; i < 10; i++)
            {
                spriteBatch.Draw(robot, new Rectangle(100 + i*50+ movement, elevation, 50, 37), animation[animationStage], Color.White);
            }
            
            

            spriteBatch.End();


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
