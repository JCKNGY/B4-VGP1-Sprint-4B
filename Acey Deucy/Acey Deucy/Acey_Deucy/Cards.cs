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
    class Cards
    {
        public String suits = "";
        public int numericalVals = 0;
        public Texture2D textures;
        public int ranking;



        public Cards(String suit, int numbericalVal, Texture2D texture,int Rank)
        {
            suits = suit;
            numericalVals = numbericalVal;
            textures = texture;
            ranking = Rank; 
        }

        public int getValue()
        {
            return numericalVals; 
        }

        public String getSuit()
        {
            return suits;

        }

        public Texture2D getTexture()
        {
            return textures;
        }
        public int getRank()
        {
            return ranking; 
        }
    }
}
