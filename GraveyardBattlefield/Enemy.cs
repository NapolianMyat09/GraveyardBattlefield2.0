using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace GraveyardBattlefield
{
    /*
     * Project: Graveyard BattleField
     * Names: Tracy Chun, Jason Wang, Napolian Myat
     * Class: Enemy
     * Purpose: Handles anything involving enemies' damage, health, position, etc.
     * 
     * Updates:
     * 
     */
    internal class Enemy
    {
        //fields
        private int width;
        private int height;
        protected Rectangle position;
        protected Texture2D texture;

        //property
        public Rectangle Position
        {
            get { return position; }
        }

        //constructor
        public Enemy(int width, int height, Texture2D texture, Rectangle position)
        {
            this.width = width;
            this.height = height;
            this.position = position;
            this.texture = texture;
        }

        public void Update(GameTime gametime, Player player)
        {
            bool XequalPlayer = false;
            bool YequalPlayer = false;

            //change X value base on player's X value
            if (player.Position.X + player.Position.Width - 20 < position.X)
            {
                position.X -= 2;
                XequalPlayer = false;
            }
            else if (player.Position.X - player.Position.Width + 20 > position.X)
            {
                position.X += 2;
                XequalPlayer = false;
            }
            else XequalPlayer = true;

            //change Y value base on player's Y value
            if (player.Position.Y + player.Position.Height - 20 < position.Y)
            {
                position.Y -= 2;
                YequalPlayer = false;
            }
            else if (player.Position.Y - player.Position.Height + 20 > position.Y)
            {
                position.Y += 2;
                YequalPlayer = false;
            }
            else YequalPlayer = true;

            //if player's x and y value both intersect with zombie's x and y value, then take damage
            if(XequalPlayer == true && YequalPlayer == true)
            {
                player.takeDamage();
            }
        }

        public void Draw(SpriteBatch sb)
        {
             sb.Draw(texture, position, Color.White);
        }
    }
}
