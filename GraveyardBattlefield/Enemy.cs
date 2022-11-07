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
        private int frame;
        private double timeCounter;
        private double timePerFrame;

        // Constants for spriteSheet
        const int WalkFrameCount = 3;
        const int VerticalPlayerOffsetY = 90;
        const int ZombieHeight = 170;     // The height of a single frame
        const int ZombieWidth = 205;      // The width of a single frame


        private Vector2 position;
        public int Health { get; set; }

        public Vector2 Position { get { return position; } set { position = value; } }

        public Texture2D Asset { get; set; }
        public Enemy(Vector2 position, Texture2D asset)

        {
            Position = position;
            Asset = asset;
            Health = 3;
            frame = 0;
            timePerFrame = 0.1f;
        }
        public void Movement()
        {

            if (Position != Player.Position)
            {
                if (Position.X < Player.Position.X)
                {
                    position.X += 2;
                }
                if (Position.X > Player.Position.X)
                {
                    position.X -= 2;

                }
                if (position.Y < Player.Position.Y)
                {
                    position.Y += 2;

                }
                if (position.Y > Player.Position.Y)
                {
                    position.Y--;
                }
            }

        }
        public void Update(GameTime gametime, KeyboardState currentKbState)
        {
            Movement();
            UpdateAnimation(gametime);
            //if player's x and y value both intersect with zombie's x and y value, then take damage
            if (Player.Position == this.Position )
            {
                Player.TakeDamage();
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Asset,
                Position,
                new Rectangle(frame * ZombieWidth,
                VerticalPlayerOffsetY,
                ZombieWidth,
                ZombieHeight),
                Color.White,                                     // - No Rotation
                0,
                Vector2.Zero,                           // - start counting in first row
                0.5f,                                   // - 50% scale change
                SpriteEffects.None,
                0); ;
        }
        public void UpdateAnimation(GameTime gameTime)
        {
            timeCounter += gameTime.ElapsedGameTime.TotalSeconds;

            // If enough time has passed:
            if (timeCounter >= timePerFrame)
            {
                frame += 1;                     // Adjust the frame to the next image

                if (frame > WalkFrameCount)    //double check bounds of frames
                {
                    frame = 0;
                }

                timeCounter -= timePerFrame;
            }
        }
    }
}
