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
        private KeyboardState previousKBState;
        private Random rdm = new Random();
        int width;
        int height;
        int wave = 1;

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
            width = _graphics.GraphicsDevice.Viewport.Width;
            height = _graphics.GraphicsDevice.Viewport.Height;
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 1200; 
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load the textures and rectangle and intialize the player object
            zombieAsset = this.Content.Load<Texture2D>("zombieSpriteSheet");
            playerAsset = this.Content.Load<Texture2D>("playerSpriteSheet");
            //menuScreen = this.Content.Load<Texture2D>("");
            //gameOverAsset = this.Content.Load<Texture2D>("");
            player = new Player(new Vector2(300, 300), playerAsset);

            //load bullet asset
            bulletTexture = this.Content.Load<Texture2D>("bulletPlaceHolder"); 

            //load font
            Font = Content.Load<SpriteFont>("Font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //check current keyboard state then update
            KeyboardState kbstate = Keyboard.GetState();
            switch (gameState)
            {
                case Stage.Main:
                    {
                        Zombies.Clear();
                        if (Process.SingleKeyPress(kbstate, Keys.Enter))
                        {
                            gameState = Stage.Wave1;
                        }
                        NextWave();
                        break;
                    }
                case Stage.Wave1:
                    {
                        player.Update(gameTime, kbstate);
                        //addBullet();
                        foreach(Enemy zombies in Zombies)
                        {
                            zombies.Update(gameTime, kbstate);
                        }
                        foreach (bullet bullets in bullets)
                        {
                            bullets.shootBullet();
                        }

                        //loop through the game
                        break;
                    }
                case Stage.GameOver:
                    {
                        if (Process.SingleKeyPress(kbstate, Keys.Enter))
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
                        _spriteBatch.DrawString(Font, $"Main menu", new Vector2(300, 600), Color.Black);
                        break;
                    }
                case Stage.Wave1:
                    {
                        GraphicsDevice.Clear(Color.LightGreen);
                        player.Draw(_spriteBatch);
                        _spriteBatch.DrawString(Font, $"player remaining health: {Player.Health}\n" +
                            $"Ammo: {playerBullet}/{playerBackupBullet}", new Vector2(0, 0), Color.Black);
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
                        //the main menu
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
                for (int i = 0; i < 2; i++)
                {
                    Zombies.Add(new Enemy(new Vector2(0, rdm.Next(0, 1120)), zombieAsset));
                    Zombies.Add(new Enemy(new Vector2(1120, rdm.Next(0, 1120)), zombieAsset));
                    Zombies.Add(new Enemy(new Vector2(rdm.Next(0, 1120), 0), zombieAsset));
                    Zombies.Add(new Enemy(new Vector2(rdm.Next(0, 1120), 1120), zombieAsset));

                }
            }
            //sprint 4
            else if(wave == 2) { }
            else if(wave == 3) { } 
        }

        //need to convert 
        /*
        private void addBullet()
        {
            KeyboardState kbstate = Keyboard.GetState();

            //while player did not run out of bullets
            if (playerBullet > 0)
            {
                if (kbstate.IsKeyDown(Keys.Up))
                {
                    bullets.Add(new bullet(width, height, new Rectangle(Player.Position.X, player.Position.Y, bulletTexture.Width, bulletTexture.Height), bulletTexture, "up"));
                    playerBullet--;
                }
                if (kbstate.IsKeyDown(Keys.Left))
                {
                    bullets.Add(new bullet(width, height, new Rectangle(player.Position.X, player.Position.Y, bulletTexture.Width, bulletTexture.Height), bulletTexture, "left"));
                    playerBullet--;
                }
                if (kbstate.IsKeyDown(Keys.Down))
                {
                    bullets.Add(new bullet(width, height, new Rectangle(player.Position.X, player.Position.Y, bulletTexture.Width, bulletTexture.Height), bulletTexture, "down"));
                    playerBullet--;
                }
                if (kbstate.IsKeyDown(Keys.Right))
                {
                    bullets.Add(new bullet(width, height, new Rectangle(player.Position.X, player.Position.Y, bulletTexture.Width, bulletTexture.Height), bulletTexture, "right"));
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
        */
        
    }
}