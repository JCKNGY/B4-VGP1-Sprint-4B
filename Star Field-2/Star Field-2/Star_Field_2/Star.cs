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
    class Star
    {
        public Texture2D textures;
        public Rectangle rectangles;
        public Color colors;
        public Vector2 positions;
        public double Velocities;


        public Star(Texture2D newTex, Rectangle newRect,Color newColor,Vector2 newVector,double Velocity)
        {
            textures = newTex;
            rectangles = newRect;
            colors = newColor;
            positions = newVector;
            Velocities = Velocity;
        } 

        public void updateTex(Texture2D tex)
        {
            textures = tex; 
        }
        public void updateRect(Rectangle rect)
        {
            rectangles = rect;
        }
        public void updateColor(Color color)
        {
            colors = color;
        }
        public void updatePos(Vector2 pos)
        {
            positions = pos;
        }
        public void updateVelocity(double velo)
        {
            Velocities = velo;
        }


       







    }
}
