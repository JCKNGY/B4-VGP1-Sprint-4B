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

namespace DeathBlossom
{
    class torpedoes
    {
        public Rectangle rect;

        Vector2 pos;
        Vector2 vel;
        int width;
        int height;

        public torpedoes(Vector2 startCenter, double headingDeg)
        {
            width = 32;
            height = 16;

            pos = startCenter;

            float radians = MathHelper.ToRadians((float)headingDeg);


            float speed = 12f;
            vel = new Vector2((float)Math.Cos(radians), (float)Math.Sin(radians)) * speed;

            rect = new Rectangle((int)(pos.X - width / 2), (int)(pos.Y - height / 2), width, height);
        }

        public void Update()
        {
            pos += vel;
            rect.X = (int)(pos.X - width / 2);
            rect.Y = (int)(pos.Y - height / 2);
        }
    }
}
