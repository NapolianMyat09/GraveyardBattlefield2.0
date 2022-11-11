using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
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
       //FIELDS
        private Rectangle position;
        private Texture2D asset;
        private bool isAlive = true;

        //PROPERTIES
        public int Health { get; set; }
        public bool IsAlive { get { return isAlive; } set{ isAlive = value;}}
        public Rectangle Position { get { return position; } set { position = value; } }
        public Enemy(Rectangle position, Texture2D asset)

        {
            Position = position;
            this.asset = asset;
            Health = 100;
        }
        public void Update(GameTime gametime, Player player)
        {
            bool XequalPlayer = false;
            bool YequalPlayer = false;
            int zombieSpeed = 3;

            //change X value base on player's X value
            if (player.Position.X + 30 < position.X) 
            {
                position.X -= zombieSpeed;
                XequalPlayer = false;
            }
            else if (player.Position.X - 30 > position.X)
            {
                position.X += zombieSpeed;
                XequalPlayer = false;
            }
            else XequalPlayer = true;

            //change Y value base on player's Y value
            if (player.Position.Y + 30 < position.Y)
            {
                position.Y -= zombieSpeed;
                YequalPlayer = false;
            }
            else if (player.Position.Y - 30> position.Y)
            {
                position.Y += zombieSpeed;
                YequalPlayer = false;
            }
            else YequalPlayer = true;

            //if player's x and y value both intersect with zombie's x and y value, then take damage
            if (XequalPlayer == true && YequalPlayer == true)
            {
                player.TakeDamage();
            }
            
        }
        public void TakeDamage()
        {
            this.Health --; 
            if(this.Health <= 0)
            {
                IsAlive = false;
            }
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(asset, Position, Color.White);
        }

    }
}
