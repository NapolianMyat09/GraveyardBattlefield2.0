using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
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
    internal class Player
    {
        /*fields
        private string name;
        private int health;
        private Texture2D asset;
        private Vector2 position;
        

        //properties
        //constructor

        //methods
        /// <summary>
        /// allows the user to move with WASD 
        /// </summary>
        public void Movement()
        {

        }
        */
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
        public Player(int width, int height, Texture2D texture, Rectangle position)
            {
                this.width = width;
                this.height = height;
            }

            //methods
            public void Update(GameTime gameTime)
            {
                int speed = 4;
                KeyboardState kbstate = Keyboard.GetState();

                if (kbstate.IsKeyDown(Keys.W) || kbstate.IsKeyDown(Keys.Up))
                {
                    position.Y -= speed;
                    if (position.Y < 0)
                    {
                        position.Y = 1200;
                    }
                }
                if (kbstate.IsKeyDown(Keys.A) || kbstate.IsKeyDown(Keys.Left))
                {
                    position.X -= speed;
                    if (position.X < 0)
                    {
                        position.X = 1200;
                    }
                }
                if (kbstate.IsKeyDown(Keys.S) || kbstate.IsKeyDown(Keys.Down))
                {
                    position.Y += speed;
                    if (position.Y > 1200)
                    {
                        position.Y = 0;
                    }
                }
                if (kbstate.IsKeyDown(Keys.D) || kbstate.IsKeyDown(Keys.Right))
                {
                    position.X += speed;
                    if (position.X > 1200)
                    {
                        position.X = 0;
                    }
                }
            }
        public virtual void Draw(SpriteBatch sb)
        {

        }

    }
}
