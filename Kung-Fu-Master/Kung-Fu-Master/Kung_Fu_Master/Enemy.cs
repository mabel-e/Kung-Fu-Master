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
    class Enemy
    {
        public int enemyX;
        public int timer;
        public Rectangle rect;
        public Boolean movingLeft, movingRight;
        public Texture2D text;
        public Enemy(int x, Player p, Texture2D t)
        {
            enemyX = x;
            text = t;
            rect = new Rectangle(x, 200, 75, 200);
            timer = 0;

        }

        public void update(Player p, List<Enemy> e)
        {
           
            if (enemyX < p.location+75 && isColliding(e) == false)
            {
                rect.X+=2;
                enemyX+=2;
            }

            if (enemyX > p.location+275 && isColliding(e) == false)
            {
                rect.X-=2;
                enemyX -= 2;
            }
            if(isColliding(e))
            {
                if(e[1].enemyX < p.location + 75)
                {
                    e[1].rect.X += 2;
                    e[1].enemyX += 2;
                    e[0].rect.X += 2;
                    e[0].enemyX += 2;
                }
                if (e[0].enemyX > p.location + 275)
                {
                    e[1].rect.X -= 2;
                    e[1].enemyX -= 2;
                    e[0].rect.X -= 2;
                    e[0].enemyX -= 2;
                }
            }
        }
        public Boolean isColliding(List<Enemy> e)
        {
            if(e.Count < 2)
            {
                return false;
            }
            for(int i = 0; i < e.Count; i++)
            {
                if(e[0].rect.Intersects(e[1].rect))
                {
                    return true;
                }
              
            }
            return false;

        }
        public void kickL(Texture2D kick)
        {

            text = kick;

        }
        public void kickR(Texture2D kick)
        {
            text = kick;

        }
    }
}
