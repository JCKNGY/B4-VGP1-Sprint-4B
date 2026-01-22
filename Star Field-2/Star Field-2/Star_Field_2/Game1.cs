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

namespace Star_Field_2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        List<Star> stars = new List<Star>();

        bool inScreen1 = true;
        bool inScreen2 = true;
        bool inScreen3 = true;

        Texture2D starTex;


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

            stars.Add(new Star(starTex, new Rectangle(0,0,0,0),new Color(0,0,0),new Vector2(0,0),0));
            stars.Add(new Star(starTex, new Rectangle(0, 0, 0, 0), new Color(0, 0, 0), new Vector2(0, 0), 0));
            stars.Add(new Star(starTex, new Rectangle(0, 0, 0, 0), new Color(0, 0, 0), new Vector2(0, 0), 0));



           


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

            starTex = this.Content.Load<Texture2D>("Star-1");




            if (!inScreen1)
            {

            }


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

            Random random = new Random();

            if (inScreen1)
            {
                int size = random.Next(20, 85);
                int speed1 = random.Next(10, 40);
                stars[0] = new Star(starTex, new Rectangle(-100, 100, size, size), new Color(random.Next(255), random.Next(255), random.Next(255)), new Vector2(0, 0), speed1);
                inScreen1 = false;          
            }

            if (inScreen2)
            {
                int size2 = random.Next(20, 85);
                int speed2 = random.Next(10, 40);
                stars[1] = new Star(starTex, new Rectangle(-100, 200, size2, size2), new Color(random.Next(255), random.Next(255), random.Next(255)), new Vector2(0, 0), speed2);
                inScreen2 = false;
            }
            if (inScreen3)
            {
                int size3 = random.Next(20, 85);
                int speed3 = random.Next(10, 40);
                stars[2] = new Star(starTex, new Rectangle(-100, 300, size3, size3), new Color(random.Next(255), random.Next(255), random.Next(255)), new Vector2(0, 0), speed3);
                inScreen3 = false;
            }


            stars[0].rectangles.X += (int)stars[0].Velocities;

            stars[1].rectangles.X += (int)stars[1].Velocities;

            stars[2].rectangles.X += (int)stars[2].Velocities;

            if(stars[0].rectangles.X > 800)
            {
                inScreen1 = true;
            }
            if (stars[1].rectangles.X > 800)
            {
                inScreen2 = true;
            }
            if (stars[2].rectangles.X > 800)
            {
                inScreen3 = true;
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
            spriteBatch.Begin();

            spriteBatch.Draw(stars[0].textures,stars[0].rectangles,stars[0].colors);
            spriteBatch.Draw(stars[1].textures, stars[1].rectangles, stars[1].colors);
            spriteBatch.Draw(stars[2].textures, stars[2].rectangles, stars[2].colors);


            spriteBatch.End();
            // TODO: Add your drawing code here
            
            base.Draw(gameTime);
        }
    }
}
