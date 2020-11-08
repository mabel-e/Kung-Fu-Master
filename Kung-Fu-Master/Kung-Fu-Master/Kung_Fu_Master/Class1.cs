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


namespace Kung_Fu_Master
{
    class Player
    {
        public int location;
        public Rectangle rect;
        public Texture2D text;
        public Player(int l, Rectangle r, Texture2D t)
        {
            location = l;
            rect = r;
            text = t;
        }
        public Player(int l)
        {
            location = l;
        }
    }
}
