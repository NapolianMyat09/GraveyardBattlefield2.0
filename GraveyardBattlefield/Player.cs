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
    enum Walking
    {
        Left,
        Right,
        Up,
        Down
    }
    public class Player : GameObject
    {

        private int frame;
        private double timeCounter;
        private double timePerFrame;

        // Constants for spriteSheet
        const int WalkFrameCount = 5;
        const int VerticalPlayerOffsetY = 10;
        const int HorizontalPlayerOffsetY = 80;
        const int PlayerHeight = 60;     // The height of a single frame
        const int PlayerWidth = 64;      // The width of a single frame

        Walking walkingState;
        private int health;
        public int Health
        {
            get { return health; }
            set
            {
                health = value;
            }
        }

        //constructor
        public Player(Vector2 position, Texture2D asset)
            : base(position, asset)
        {
            frame = 0;
            timeCounter = 0;
            timePerFrame = 0.1;
            Health = 300;
        }


        public void Movement(KeyboardState currentKbState)
        {
            if (currentKbState.IsKeyDown(Keys.W))
            {
                position.Y -= 4f;
                if (position.Y < 0)
                {
                    position.Y = 0;
                }
                walkingState = Walking.Up;

            }
            if (currentKbState.IsKeyDown(Keys.A))
            {
                position.X -= 4f;
                if (position.X < 0)
                {
                    position.X = 0;
                }
                walkingState = Walking.Left;

            }
            if (currentKbState.IsKeyDown(Keys.S))
            {
                position.Y += 4f;
                if (position.Y > 1120)
                {
                    position.Y = 1120;
                }
                walkingState = Walking.Down;

            }
            if (currentKbState.IsKeyDown(Keys.D))
            {
                position.X += 4f;
                if (position.X > 1120)
                {
                    position.X = 1120;
                }
                walkingState = Walking.Right;
            }
        }
        public override void Update(GameTime gametime, KeyboardState currentKbState)
        {
            Movement(currentKbState);
            UpdateAnimation(gametime);
        }
        public override void Draw(SpriteBatch sb)
        {
            switch (walkingState)
            {
                case Walking.Up:
                    DrawWalkingVertical(SpriteEffects.None, sb);
                    break;
                case Walking.Left:
                    DrawWalkingHorizontal(SpriteEffects.None, sb);
                    break;
                case Walking.Down:
                    DrawWalkingVertical(SpriteEffects.None, sb);
                    break;
                case Walking.Right:
                    DrawWalkingHorizontal(SpriteEffects.FlipHorizontally, sb);
                    break;
            }
        }
        private void DrawWalkingHorizontal(SpriteEffects flipSprite, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Texture,                         // - The texture to draw
                Position,                               // - Where to draw it
                new Rectangle(                          // - The rectangle to draw
                    frame * PlayerWidth,
                    HorizontalPlayerOffsetY,
                    PlayerWidth,
                    PlayerHeight),
                Color.White,                            // - No color
                0,                                      // - No Rotation
                Vector2.Zero,                           // - Start counting in the second row
                1.0f,                                   // - no scale change
                flipSprite, 0);                         // - flip if necessary
        }

        private void DrawWalkingVertical(SpriteEffects flipSprite, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Texture,                         // - The texture to draw
                Position,                               // - Where to draw it
                new Rectangle(                          // - The rectangle to draw
                    frame * PlayerWidth,
                    VerticalPlayerOffsetY,
                    PlayerWidth,
                    PlayerHeight),
                Color.White,                            // - No color
                0,                                      // - No Rotation
                Vector2.Zero,                           // - Start counting in the second row
                1.0f,                                   // - no scale change
                flipSprite,                             // - flip if necessary
                0);
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
    
    public void TakeDamage()
        {
            Health -= 1;
        }
    }
}
