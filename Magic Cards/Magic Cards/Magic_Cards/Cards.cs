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

namespace Magic_Cards
{
    class Cards
    {
        Texture2D cardTex;
        Rectangle cardRect;

        public void changeTex(Texture2D tex)
        {
            cardTex = tex;
        }

        public void changeRect(Rectangle rect)
        {
            cardRect = rect;
        }
        public Texture2D getText()
        {
            return cardTex;
        }
        public Rectangle getRect()
        {
            return cardRect;
        }
    }

    
}
