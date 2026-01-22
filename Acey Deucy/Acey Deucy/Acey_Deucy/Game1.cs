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

namespace Acey_Deucy
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        bool Shuffled;

        List<Cards> Cards = new List<Cards>(52);
        List<int> Numbers = new List<int>();
        List<Cards> Textures = new List<Cards>(52);


        SpriteFont font;

        String suit = "c";
        String actualSuit = "Clubs";
        int ranking = 0;

        Cards higherCard;
        Cards lowerCard;
        Cards newCard;
        KeyboardState oldkb = Keyboard.GetState();
        bool thirdCard = false; 

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
            for (int j = 0; j < 4; j++)
            {
                switch (j)
                {
                    case 0:
                        suit = "c";
                        actualSuit = "Clubs";
                        ranking = 0;
                        break;
                    case 1:
                        suit = "d";
                        actualSuit = "Diamonds";
                        ranking = 1;
                        break;
                    case 2:
                        suit = "h";
                        actualSuit = "Heart";
                        ranking = 2;
                        break;
                    case 3:
                        suit = "s";
                        actualSuit = "Spades";
                        ranking = 3;
                        break;
                    default:
                        suit = "c";
                        break;
                }

                for (int i = 1; i < 13; i++)
                {

                    Textures.Add(new Cards(actualSuit, i, this.Content.Load<Texture2D>(suit + i + ""),ranking));
                }
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
            KeyboardState kb = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || (kb.IsKeyDown(Keys.Escape) && !oldkb.IsKeyDown(Keys.Escape)))
                this.Exit();

            if (kb.IsKeyDown(Keys.Space) && !oldkb.IsKeyDown(Keys.Space))
            {
                Shuffled = false;
            }


            if (!Shuffled)
                Cards = Shuffle_Cards(Textures[0], Textures);
            else
                Numbers = Shuffle_Cards(-1, Numbers);

            if (Cards[0].getRank() > Cards[1].getRank())
            {
                higherCard = Cards[0];
                lowerCard = Cards[1];
            }
            else if (Cards[0].getRank() > Cards[1].getRank())
            {
                higherCard = Cards[1];
                lowerCard = Cards[0];
            }
            else
            {
                if (Cards[0].getValue() > Cards[1].getValue())
                {
                    higherCard = Cards[0];
                    lowerCard = Cards[1];
                }
                else
                {
                    higherCard = Cards[1];
                    lowerCard = Cards[0];
                }
            }

            if (kb.IsKeyDown(Keys.B) && !oldkb.IsKeyDown(Keys.B))
            {
                thirdCard = true;
                newCard = new Cards(lowerCard.getSuit(), lowerCard.getValue() - 1, this.Content.Load<Texture2D>(lowerCard.getSuit().Substring(0,1).ToLower()+ (lowerCard.getValue() - 1) + ""), lowerCard.getRank());
            }
            if (kb.IsKeyDown(Keys.A) && !oldkb.IsKeyDown(Keys.A))
            {
                thirdCard = true;
                newCard = new Cards(higherCard.getSuit(), higherCard.getValue() + 1, this.Content.Load<Texture2D>(higherCard.getSuit().Substring(0, 1).ToLower() + (higherCard.getValue() + 1) + ""), higherCard.getRank());
            }
            if (kb.IsKeyDown(Keys.I) && !oldkb.IsKeyDown(Keys.I))
            {
                thirdCard = true;
                newCard = new Cards(higherCard.getSuit(), higherCard.getValue() + 1, this.Content.Load<Texture2D>(higherCard.getSuit().Substring(0, 1).ToLower() + (higherCard.getValue() - 1) + ""), higherCard.getRank());
            }


            // TODO: Add your update logic here

            base.Update(gameTime);
            oldkb = kb;
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

            spriteBatch.Draw(Cards[0].getTexture(), new Rectangle(300, 100, 100, 138), Color.White);
            spriteBatch.DrawString(font, "" + Cards[0].getSuit() + "/" + Cards[0].getValue(),new Vector2(175,165) , Color.Black);



            spriteBatch.Draw(Cards[1].getTexture(), new Rectangle(400, 100, 100, 138), Color.White);
            spriteBatch.DrawString(font, "" + Cards[1].getSuit() + "/" + Cards[1].getValue(), new Vector2(510, 165), Color.Black);

            if (thirdCard == true)
            {
                spriteBatch.Draw(newCard.getTexture(), new Rectangle(350, 200, 100, 138), Color.White);
                spriteBatch.DrawString(font, "" + newCard.getSuit() + "/" + newCard.getValue(), new Vector2(450, 265), Color.Black);
            }
            


            spriteBatch.DrawString(font, "Pick/Press the corresponding button:\n B)elow the lower Card \n A)bove the higher card \n I)nside the two cards", new Vector2(0, 375), Color.Black);


            spriteBatch.End();



            base.Draw(gameTime);
        }





        public List<T> Shuffle_Cards<T>(T Value, List<T> CList)
        {
            // Local Vars
            int I, R;
            bool Flag;
            Random Rand = new Random();
            // Local List of T type
            var CardList = new List<T>();
            // Build Local List as big as passed in list and fill it with default value
            for (I = 0; I < CList.Count; I++)
                CardList.Add(Value);
            // Shuffle the list of cards
            for (I = 0; I < CList.Count; I++)
            {
                Flag = false;
                // Loop until an empty spot is found
                do
                {
                    R = Rand.Next(0, CList.Count);
                    if (CardList[R].Equals(Value))
                    {
                        Flag = true;
                        CardList[R] = CList[I];
                    }
                } while (!Flag);
            }
            // Set global var Shuffled to true
            Shuffled = true;
            // Return the shuffled list
            return CardList;
        }



    }
}
