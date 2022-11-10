using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace GraveyardBattlefield
{
    /*
     * Project: Graveyard BattleField
     * Names: Tracy Chun, Jason Wang, Napolian Myat
     * Class: Game
     * Purpose: handles monogame's update and draws
     * 
     * Updates:
     * 
     */
    public enum Stage
    {
        Main,
        Wave1,
        Wave2,
        FinalWave,
        GameOver
    }
    public class Game1 : Game
    {
        //fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Stage gameState = Stage.Main;

        private Player player;
        private List<Enemy> Zombies = new List<Enemy>();

        //PREVIOUS KEYBOARD/MOUSE STATES
        private KeyboardState previousKBState;
        private MouseState previousMState;

        private Random rdm = new Random();
        private static int width;
        private static int height;
        int wave = 1;

        public static int Width
        {
            get { return width; }
        }
        public static int Height
        {
            get { return height; }
        }

        //ammo assets
        Texture2D bulletTexture;
        private int playerBullet = 150;
        private int playerBackupBullet = 600;
        private List<bullet> bullets = new List<bullet>();

        //the player and zombie assets
        private Texture2D menuScreen;
        private Texture2D zombieAsset;
        private Texture2D playerAsset;
        private Texture2D gameOverAsset;
        private Texture2D background;
        private Texture2D startButton;
        //StartButton Rect
        private Rectangle startButtonRect;

        //fonts
        private SpriteFont Font;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1500;
            _graphics.PreferredBackBufferHeight = 950;
            width = _graphics.PreferredBackBufferWidth;
            height = _graphics.PreferredBackBufferHeight;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load the textures and rectangle and intialize the player object
            //GAMEPLAY
            zombieAsset = this.Content.Load<Texture2D>("zombieKid");
            playerAsset = this.Content.Load<Texture2D>("playerSpriteSheet");
            background = this.Content.Load<Texture2D>("graveyard");

            //MENU
            menuScreen = this.Content.Load<Texture2D>("mainMenuScreen");
            startButton = this.Content.Load<Texture2D>("StartButton");
            startButtonRect = new Rectangle((width - 350)/2, 600, 350, 250);

            //gameOverAsset = this.Content.Load<Texture2D>("");
            player = new Player(new Vector2(300, 300), playerAsset);


            //load bullet asset
            bulletTexture = this.Content.Load<Texture2D>("bullet"); 

            //load font
            Font = Content.Load<SpriteFont>("Font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //check current keyboard state then update
            KeyboardState kbstate = Keyboard.GetState();
            MouseState mState = Mouse.GetState(); //get mouseState
            switch (gameState)
            {
                case Stage.Main:
                    {
                        Zombies.Clear(); //Reset zombies
                        resetGame(); //Reset previous progress
                        //if (Process.SingleKeyPress(kbstate, Keys.Enter))
                        //{
                        //    gameState = Stage.Wave1;
                        //}
                        if(Process.MouseClick(mState, startButtonRect))
                        {
                            gameState = Stage.Wave1;
                        }
                        NextWave();
                        break;
                    }
                case Stage.Wave1:
                    {
                        player.Update(gameTime, kbstate);
                        addBullet();
                        foreach(Enemy zombies in Zombies)
                        {
                            zombies.Update(gameTime, player);
                        }
                        foreach (bullet bullets in bullets)
                        {
                            bullets.shootBullet();
                        }

                        if(player.Health <= 0)
                        {
                            gameState = Stage.GameOver;
                        }
                        //loop through the game
                        break;
                    }
                case Stage.GameOver:
                    {
                        if (Process.SingleKeyPress(kbstate, Keys.Space))
                        {
                            gameState = Stage.Main;
                            wave = 1;
                        }
                        break;
                    }
            }
            Process.PreviousKbState = kbstate;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            switch (gameState)
            {
                case Stage.Main:
                    {
                        //the main menu
                        _spriteBatch.Draw(menuScreen, new Rectangle(0, 0, width, height), Color.White);
                        //_spriteBatch.DrawString(Font, $"Press 'Enter' to start the game", new Vector2(300, 650), Color.White);
                        //Will have startButton start the game instead of pressing enter
                        _spriteBatch.Draw(startButton, startButtonRect, Color.DarkRed);
                        break;
                    }
                case Stage.Wave1:
                    {
                        _spriteBatch.Draw(background, new Rectangle(0, 0, width, height), Color.White);
                        player.Draw(_spriteBatch);
                        _spriteBatch.DrawString(Font, $"player remaining health: {player.Health}\n" +
                            $"Ammo: {playerBullet}/{playerBackupBullet}", new Vector2(0, 0), Color.White);
                        player.Draw(_spriteBatch);
                        foreach(Enemy zombies in Zombies)
                        {
                            zombies.Draw(_spriteBatch);
                        } 
                        foreach(bullet bullets in bullets)
                        {
                            bullets.Draw(_spriteBatch);
                        }
                        break;
                    }
                case Stage.GameOver:
                    {
                        _spriteBatch.DrawString(Font, $"Press Space to go back to Main menu", new Vector2(300, 600), Color.Black);
                        break;
                    }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void NextWave()
        {
            if (wave == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    Zombies.Add(new Enemy(new Vector2(0, rdm.Next(0, height)), zombieAsset));
                    Zombies.Add(new Enemy(new Vector2(width, rdm.Next(0, height)), zombieAsset));
                    Zombies.Add(new Enemy(new Vector2(rdm.Next(0, width), 0), zombieAsset));
                    Zombies.Add(new Enemy(new Vector2(rdm.Next(0, width), height), zombieAsset));

                }
            }
            //sprint 4
            else if(wave == 2) { }
            else if(wave == 3) { } 
        }

        //need to convert 
        private void addBullet()
        {
            KeyboardState kbstate = Keyboard.GetState();

            //while player did not run out of bullets
            if (playerBullet > 0)
            {
                if (kbstate.IsKeyDown(Keys.Up))
                {
                    bullets.Add(new bullet(width, height, new Rectangle(Convert.ToInt32(player.Position.X), Convert.ToInt32(player.Position.Y), bulletTexture.Width, bulletTexture.Height), bulletTexture, "up"));
                    playerBullet--;
                }
                if (kbstate.IsKeyDown(Keys.Left))
                {
                    bullets.Add(new bullet(width, height, new Rectangle(Convert.ToInt32(player.Position.X), Convert.ToInt32(player.Position.Y), bulletTexture.Width, bulletTexture.Height), bulletTexture, "left"));
                    playerBullet--;
                }
                if (kbstate.IsKeyDown(Keys.Down))
                {
                    bullets.Add(new bullet(width, height, new Rectangle(Convert.ToInt32(player.Position.X), Convert.ToInt32(player.Position.Y), bulletTexture.Width, bulletTexture.Height), bulletTexture, "down"));
                    playerBullet--;
                }
                if (kbstate.IsKeyDown(Keys.Right))
                {
                    bullets.Add(new bullet(width, height, new Rectangle(Convert.ToInt32(player.Position.X), Convert.ToInt32(player.Position.Y), bulletTexture.Width, bulletTexture.Height), bulletTexture, "right"));
                    playerBullet--;
                }
            }
            //if we still have the backup bullets
            if(playerBackupBullet >= 150)
            {
                if (playerBullet == 0)
                {
                    playerBullet = 150;
                    playerBackupBullet -= 150;
                }
            }
        }

        //reset game method
        public void resetGame()
        {
            player.Health = 300;
            playerBullet = 150;
            playerBackupBullet = 600;
        }
        
    }
}