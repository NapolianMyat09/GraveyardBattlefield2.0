using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
    public enum Screen
    {
        MainMenu,
        FirstWave,
        SecondWave,
        FinalWave,
        CharSelection,
        Controls,
        AboutUs
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D menu;
        private SpriteFont font;
        public Screen gameState = Screen.MainMenu;
        public KeyboardState prevKbState;
        public KeyboardState currentkbState;
        private Player player;
        private Texture2D playerTexture;
        Rectangle position;
        int width;
        int height;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here\
            width = _graphics.GraphicsDevice.Viewport.Width;
            height = _graphics.GraphicsDevice.Viewport.Height;
            //changed window size to 1920 x 1080
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            menu = Content.Load<Texture2D>("MainMenu");
            font = Content.Load<SpriteFont>("Font");
            playerTexture = this.Content.Load<Texture2D>("file");
            position = new Rectangle(width / 2, height / 2, playerTexture.Width, playerTexture.Height);
            player = new Player(width, height, playerTexture, position);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            currentkbState = Keyboard.GetState();
            // TODO: Add your update logic here
            switch (gameState)
            {
                case Screen.MainMenu:
                    if (SingleKeyPress(Keys.S, currentkbState))
                    {
                        gameState = Screen.FirstWave;
                    }
                    break;
                case Screen.FirstWave:
                    player.Update(gameTime);
                    if (SingleKeyPress(Keys.F, currentkbState))
                    {
                        gameState = Screen.SecondWave;
                    }
                    break;
                case Screen.SecondWave:
                    if (SingleKeyPress(Keys.S, currentkbState))
                    {
                        gameState = Screen.FinalWave;
                    }
                    break;
                case Screen.FinalWave:
                    if (SingleKeyPress(Keys.M, currentkbState))
                    {
                        gameState = Screen.MainMenu;
                    }
                    break;
                case Screen.CharSelection:
                    break;
                case Screen.Controls:
                    break;
                case Screen.AboutUs:
                    break;
                   
            }
            prevKbState = currentkbState;
            Process.prevKbState = Process.currentkbState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            switch (gameState)
            {
                case Screen.MainMenu:
                    _spriteBatch.Draw(menu, new Vector2(0, 0), Color.White);
                    break;
                case Screen.FirstWave:
                    _spriteBatch.Draw(playerTexture, player.Position, Color.White);
                    player.Draw(_spriteBatch);
                    _spriteBatch.DrawString(font, "first wave screen", new Vector2(300, 300), Color.Black);

                    break;
                case Screen.SecondWave:
                    _spriteBatch.DrawString(font, "second wave screen", new Vector2(300, 300), Color.Black);
                    break;
                case Screen.FinalWave:
                    _spriteBatch.DrawString(font, "second wave screen", new Vector2(300, 300), Color.Black);

                    break;
                case Screen.CharSelection:
                    break;
                case Screen.Controls:
                    break;
                case Screen.AboutUs:
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

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