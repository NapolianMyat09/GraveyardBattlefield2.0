using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GraveyardBattlefield
{
    /*
     * Project: Graveyard BattleField
     * Names: Tracy Chun, Jason Wang, Napolian Myat
     * Class: Player
     * Purpose: - handles player class and their interactions with npcs
     *          - Has stats of players, health, damage, etc.
     * 
     * Updates:
     * 
     */
    internal class Player : GameObject
    {
        //fields
        private int width;
        private int height;
        private int health;

        //property
        public int Health
        {
            get { return health; }
        }

        //constructor
        public Player(int width, int height, Texture2D texture, Rectangle position) : base(position, texture)
        {
            this.width = width;
            this.height = height;
            health = 1000;
        }

        //methods
        public override void Update(GameTime gameTime)
        {
            int speed = 4;
            KeyboardState kbstate = Keyboard.GetState();

            if (kbstate.IsKeyDown(Keys.W))
            {
                position.Y -= speed;
                if (position.Y < 0)
                {
                    position.Y = 0;
                }
            }
            if (kbstate.IsKeyDown(Keys.A))
            {
                position.X -= speed;
                if (position.X < 0)
                {
                    position.X = 0;
                }
            }
            if (kbstate.IsKeyDown(Keys.S))
            {
                position.Y += speed;
                if (position.Y > 1120)
                {
                    position.Y = 1120;
                }
            }
            if (kbstate.IsKeyDown(Keys.D))
            {
                position.X += speed;
                if (position.X > 1120)
                {
                    position.X = 1120;
                }
            }
        }
        public void takeDamage()
        {
            health -= 1;
        }
    }
}
