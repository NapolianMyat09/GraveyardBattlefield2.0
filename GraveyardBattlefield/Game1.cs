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
    enum GameMode
    {
        Menu,
        Waves,
        GameOver
    }
    public class Game1 : Game
    {
        //fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameMode currentMode = GameMode.Menu;
        private Player player;
        private List<Enemy> Zombies = new List<Enemy>();
        private KeyboardState previousKBState;
        private Random rdm = new Random();
        int width;
        int height;
        int wave = 1;

        //the player and zombie assets
        Texture2D playerTexture;
        Rectangle playerPosition;
        Texture2D zombieTexture;
        Rectangle zombiePosition;

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
            playerTexture = this.Content.Load<Texture2D>("playerPlaceHolder1");
            playerPosition = new Rectangle(width / 2, height / 2, playerTexture.Width, playerTexture.Height);
            player = new Player(width, height, playerTexture, playerPosition);
            zombieTexture = this.Content.Load<Texture2D>("zombiePlaceHolder1");
            zombiePosition = new Rectangle(width / 2, height / 2, zombieTexture.Width, zombieTexture.Height);

            //load font
            Font = Content.Load<SpriteFont>("Font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //check current keyboard state then update
            KeyboardState kbstate = Keyboard.GetState();
            switch (currentMode)
            {
                case GameMode.Menu:
                    {
                        if (SingleKeyPress(Keys.Enter, kbstate) == true)
                        {
                            currentMode = GameMode.Waves;
                            previousKBState = kbstate;
                        }
                        NextWave();
                        break;
                    }
                case GameMode.Waves:
                    {
                        player.Update(gameTime);

                        //loop through the game
                        break;
                    }
                case GameMode.GameOver:
                    {
                        if (SingleKeyPress(Keys.Enter, kbstate) == true)
                        {
                            currentMode = GameMode.Menu;
                            previousKBState = kbstate;
                            wave = 1;
                        }
                        break;
                    }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            switch (currentMode)
            {
                case GameMode.Menu:
                    {
                        //the main menu
                        _spriteBatch.DrawString(Font, $"Main menu", new Vector2(300, 600), Color.Black);
                        break;
                    }
                case GameMode.Waves:
                    {
                        _spriteBatch.Draw(playerTexture, player.Position, Color.White);
                        player.Draw(_spriteBatch);
                        foreach(Enemy zombies in Zombies)
                        {
                            zombies.Draw(_spriteBatch);
                        }
                        break;
                    }
                case GameMode.GameOver:
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
                for (int i = 0; i < 3; i++)
                {
                    Zombies.Add(new Enemy(width, height, zombieTexture, new Rectangle(0, rdm.Next(0, 1200), zombieTexture.Width, zombieTexture.Height)));
                    Zombies.Add(new Enemy(width, height, zombieTexture, new Rectangle(1200, rdm.Next(0, 1200), zombieTexture.Width, zombieTexture.Height)));
                    Zombies.Add(new Enemy(width, height, zombieTexture, new Rectangle(rdm.Next(0, 1200), 0, zombieTexture.Width, zombieTexture.Height)));
                    Zombies.Add(new Enemy(width, height, zombieTexture, new Rectangle(rdm.Next(0, 1200), 1200, zombieTexture.Width, zombieTexture.Height)));
                }
            }
            //sprint 3
            else if(wave == 2) { }
            else if(wave == 3) { } 
        }

        //check for single key
        private bool SingleKeyPress(Keys key, KeyboardState kbState)
        {
            if (kbState.IsKeyDown(key) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}