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

namespace Fallout_Chess
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D Wpawn1, Wpawn2, WRook, WQueen, WKnight, WKing, WBishops, Bpawn1, Bpawn2, BRook, BQueen, BKnight, BKing, BBishops,Board;
 


        Texture2D[] texture = new Texture2D[15];
        Rectangle[] rectangles = new Rectangle[32];

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 495;
            graphics.PreferredBackBufferHeight = 534;
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

            base.Initialize();
           
            
            for (int i = 0; i < 8; i++)
            {
                rectangles[i] = new Rectangle(5 + 62 * i, 10, 50, 50);
                rectangles[i + 8] = new Rectangle(5 + 62*i, 75, 50, 50);
                rectangles[i+16] = new Rectangle(5 + 62 * i, 405, 50, 50);
                rectangles[i + 24] = new Rectangle(5 + 62 * i, 475, 50, 50);
            }



            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Wpawn1 = this.Content.Load<Texture2D>("Wpawn1");
            Wpawn2= this.Content.Load<Texture2D>("Wpawn2");
            WRook = this.Content.Load<Texture2D>("WRook");
            WQueen = this.Content.Load<Texture2D>("WQueen");
            WKnight = this.Content.Load<Texture2D>("WKnight");
            WKing = this.Content.Load<Texture2D>("WKing");
            WBishops = this.Content.Load<Texture2D>("WBishop");
            Bpawn1 = this.Content.Load<Texture2D>("Pawn1");
            Bpawn2 = this.Content.Load<Texture2D>("Pawn2");
            BRook = this.Content.Load<Texture2D>("Rook");
            BQueen = this.Content.Load<Texture2D>("Queen");
            BKnight = this.Content.Load<Texture2D>("Knight");
            BKing = this.Content.Load<Texture2D>("King");
            BBishops = this.Content.Load<Texture2D>("Bishop");
            Board = this.Content.Load<Texture2D>("Chess Board");

            texture = new Texture2D[] { Wpawn1, Wpawn2, WRook, WQueen, WKnight, WKing, WBishops, Bpawn1, Bpawn2, BRook, BQueen, BKnight, BKing, BBishops };
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

            spriteBatch.Draw(Board, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),Color.White);


            for(int i = 0; i < 8; i++)
            {
                if (i % 2 == 0)
                {
                    spriteBatch.Draw(texture[0], rectangles[i+8], Color.White);
                    spriteBatch.Draw(texture[7], rectangles[i + 16], Color.White);
                }
                else
                {
                    spriteBatch.Draw(texture[1], rectangles[i+8], Color.White);
                    spriteBatch.Draw(texture[8], rectangles[i + 16], Color.White);
                }
                if (i == 0 || i == 7)
                {
                    spriteBatch.Draw(texture[2], rectangles[i], Color.White);
                    spriteBatch.Draw(texture[9], rectangles[i+24], Color.White);
                }
                if (i == 1 || i == 6)
                {
                    spriteBatch.Draw(texture[4], rectangles[i], Color.White);
                    spriteBatch.Draw(texture[11], rectangles[i + 24], Color.White);
                }

                if (i == 2 || i == 5)
                {
                    spriteBatch.Draw(texture[6], rectangles[i], Color.White);
                    spriteBatch.Draw(texture[13], rectangles[i + 24], Color.White);
                }
                if (i == 3)
                {
                    spriteBatch.Draw(texture[3], rectangles[i], Color.White);
                    spriteBatch.Draw(texture[10], rectangles[i + 24], Color.White);
                }
                if (i == 4)
                {
                    spriteBatch.Draw(texture[5], rectangles[i], Color.White);
                    spriteBatch.Draw(texture[12], rectangles[i + 24], Color.White);
                }

            }

            



            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
