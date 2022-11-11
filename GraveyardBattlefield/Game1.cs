using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Linq;

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
        //FIELDS
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Stage gameState = Stage.Main;

        //ASSET THAT MOVES
        private Player player;
        private List<Enemy> zombies = new List<Enemy>();
        //ammo assets
        Texture2D bulletTexture;
        private int playerBullet = 150;
        private int playerBackupBullet = 600;
        private List<bullet> bullets = new List<bullet>();

        //PREVIOUS KEYBOARD/MOUSE STATES
        //private KeyboardState previousKBState;
        //private MouseState previousMState;

        private Random rdm = new Random();
        private static int width; //screen width
        private static int height; //screen height

        //Number of Waves
        int wave = 1;


        //the player and zombie assets
        private Texture2D menuScreen;
        private Texture2D zombieAsset;
        private Texture2D playerAsset;
        private Texture2D gameOverAsset;
        private Texture2D background;
        private Texture2D startButton;
        //StartButton Rect
        private Rectangle startButtonRect;
        private int countdown;

        //fonts\\\\\\
        private SpriteFont font;
        private SpriteFont titleFont;

        //PROPERTIES
        public static int Width
        {
            get { return width; }
        }
        public static int Height
        {
            get { return height; }
        }

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

            //Timer
            countdown = 5*60;
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
            startButtonRect = new Rectangle((width - 350)/2, 700, 350, 150);

            //gameOverAsset = this.Content.Load<Texture2D>("");
            player = new Player(new Vector2(300, 300), playerAsset);


            //load bullet asset
            bulletTexture = this.Content.Load<Texture2D>("bullet"); 

            //load font
            font = Content.Load<SpriteFont>("Font");
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
                        zombies.Clear(); //Reset zombies
                        ResetGame(); //Reset previous progress

                        //for keyboard press enter, will start game
                        //if (Process.SingleKeyPress(kbstate, Keys.Enter))
                        //{
                        //    gameState = Stage.Wave1;
                        //}

                        //Leftclick start button, will start game
                        if(Process.MouseClick(mState, startButtonRect))
                        {
                            gameState = Stage.Wave1; //progresses to wave1
                        }
                        NextWave();
                        break;
                    }
                case Stage.Wave1:
                    {
                        countdown--;
                        //Player can still move even if countdown is not 0
                        player.Update(gameTime, kbstate);

                        if (countdown <= 0) //implement a countdown, will load zombies when countdown reaches 0
                        {
                            addBullet();
                            //foreach (Enemy zombie in zombies)
                            //{
                            //    if (zombie.IsAlive == false)
                            //    {
                            //        zombies.Remove(zombie); //remove him, hes dead
                            //    }
                            //    else
                            //    {
                            //        zombie.Update(gameTime, player);
                            //    }
                            //}
                            for (int i = 0; i < zombies.Count; i++)
                            {
                                if (zombies[i].IsAlive == false)
                                {
                                    zombies.Remove(zombies[i]);
                                }
                                else
                                {
                                    zombies[i].Update(gameTime, player);
                                }
                            }
                        }

                        //can still shoot even if countdown does not reach 0
                        //foreach (bullet bullet in bullets)
                        //{
                        //    bullet.shootBullet();
                        //    //foreach (Enemy zombie in zombies)
                        //    //{
                        //    //    if(bullets.Position.Contains(zombie.Position))
                        //    //    {
                        //    //        zombie.TakeDamage();
                        //    //    }
                        //    //}
                        //}
                        for (int i = 0; i < bullets.Count; i++)
                        {
                            bullets[i].shootBullet();
                            for (int j = 0; j < zombies.Count; j++)
                            {
                                if (bullets[i].Position.Contains(zombies[j].Position.X + 15, zombies[j].Position.Y + 15))
                                {
                                    zombies[j].TakeDamage();
                                }
                            }
                        }

                        if (player.Health <= 0) //if player health reaches 0 or less
                        {
                            player.Health = 0; //we want to display health = 0 instead of negatives for whatever reason
                            gameState = Stage.GameOver; //display gamestate gameover
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
                        //Before we start the game, we want to have a countdown to get players to be ready
                        countdown--;

                        //DONT DRAW ANYTHING BEFORE BACKGROUND OTHERWISE IT WONT SHOW
                        //DrawBackGround
                        _spriteBatch.Draw(background, new Rectangle(0, 0, width, height), Color.White);

                        //Draw STATS
                        _spriteBatch.DrawString(font, $"player remaining health: {player.Health}\n" + //Health
                            $"Ammo: {playerBullet}/{playerBackupBullet}", new Vector2(0, 0), Color.White); //Ammos
                        if (countdown > 0) //draw countdown
                        {
                            _spriteBatch.DrawString(font, $"Controls:" +
                                $"\nW - Up          UpArrowKey - Shoot Upward" +
                                $"\nA - Left        LeftArrowKey - Shoot Left" +
                                $"\nS - Down        DownArrowKey - Shoot Downward" +
                                $"\nD - Right       RightArrowKey - Shoot Right"
                                , new Vector2(400, (height - 100) / 2), Color.White);
                            _spriteBatch.DrawString(font, $"{countdown / 60} seconds before zombies break in."
                                , new Vector2(500, (height-200)/2), Color.White);//num of seconds remaining
                        }

                        //DrawPLayer&Enemy Asset
                        player.Draw(_spriteBatch);
                        foreach (Enemy zombies in zombies)
                        {
                            zombies.Draw(_spriteBatch);
                        }

                        foreach (bullet bullets in bullets)
                        {
                            bullets.Draw(_spriteBatch);
                        }

                        break;
                    }
                case Stage.GameOver:
                    {
                        _spriteBatch.DrawString(font, $"Press Space to go back to Main menu", new Vector2(300, 600), Color.Black);
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
                for (int i = 0; i < 20; i++)
                {
                    int randNum = rdm.Next(0, 4);
                    if (randNum == 0)
                        zombies.Add(new Enemy(new Rectangle(0, rdm.Next(0, height), 30, 30), zombieAsset));
                    else if (randNum == 1)
                        zombies.Add(new Enemy(new Rectangle(width, rdm.Next(0, height), 30, 30), zombieAsset));
                    else if (randNum == 2)
                        zombies.Add(new Enemy(new Rectangle(rdm.Next(0, width), 0, 30, 30), zombieAsset));
                    else if (randNum == 3)
                        zombies.Add(new Enemy(new Rectangle(rdm.Next(0, width), height, 30, 30), zombieAsset));
                    else
                    { //nth happens
                    }
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
                //Upward
                if (kbstate.IsKeyDown(Keys.Up))
                {
                    //Upward Left
                    if (kbstate.IsKeyDown(Keys.Left) && kbstate.IsKeyDown(Keys.Up))
                    {
                        bullets.Add(new bullet(width, height, new Rectangle(Convert.ToInt32(player.Position.X), Convert.ToInt32(player.Position.Y), bulletTexture.Width, bulletTexture.Height), bulletTexture, "upleft"));
                        playerBullet--;
                    }
                    //Upward Right
                    else if (kbstate.IsKeyDown(Keys.Right) && kbstate.IsKeyDown(Keys.Up))
                    {
                        bullets.Add(new bullet(width, height, new Rectangle(Convert.ToInt32(player.Position.X), Convert.ToInt32(player.Position.Y), bulletTexture.Width, bulletTexture.Height), bulletTexture, "upright"));
                        playerBullet--;
                    }
                    else //just upward
                    {
                        bullets.Add(new bullet(width, height, new Rectangle(Convert.ToInt32(player.Position.X), Convert.ToInt32(player.Position.Y), bulletTexture.Width, bulletTexture.Height), bulletTexture, "up"));
                        playerBullet--;
                    }
                }
                //Downward
                else if (kbstate.IsKeyDown(Keys.Down))
                {
                    //Downward Left
                    if (kbstate.IsKeyDown(Keys.Down) && kbstate.IsKeyDown(Keys.Left))
                    {
                        bullets.Add(new bullet(width, height, new Rectangle(Convert.ToInt32(player.Position.X), Convert.ToInt32(player.Position.Y), bulletTexture.Width, bulletTexture.Height), bulletTexture, "downleft"));
                        playerBullet--;
                    }
                    //Downward Right
                    else if (kbstate.IsKeyDown(Keys.Down) && kbstate.IsKeyDown(Keys.Right))
                    {
                        bullets.Add(new bullet(width, height, new Rectangle(Convert.ToInt32(player.Position.X), Convert.ToInt32(player.Position.Y), bulletTexture.Width, bulletTexture.Height), bulletTexture, "downright"));
                        playerBullet--;
                    }
                    else //just downward
                    {
                        bullets.Add(new bullet(width, height, new Rectangle(Convert.ToInt32(player.Position.X), Convert.ToInt32(player.Position.Y), bulletTexture.Width, bulletTexture.Height), bulletTexture, "down"));
                        playerBullet--;
                    }
                }
                //Right
                else if (kbstate.IsKeyDown(Keys.Right))
                {
                    bullets.Add(new bullet(width, height, new Rectangle(Convert.ToInt32(player.Position.X), Convert.ToInt32(player.Position.Y), bulletTexture.Width, bulletTexture.Height), bulletTexture, "right"));
                    playerBullet--;
                }
                //Left
                else if (kbstate.IsKeyDown(Keys.Left))
                {
                    bullets.Add(new bullet(width, height, new Rectangle(Convert.ToInt32(player.Position.X), Convert.ToInt32(player.Position.Y), bulletTexture.Width, bulletTexture.Height), bulletTexture, "left"));
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
        public void ResetGame()
        {
            player.Health = 300;
            playerBullet = 150;
            playerBackupBullet = 600;
            countdown = 5*60;
        }
        
    }
}