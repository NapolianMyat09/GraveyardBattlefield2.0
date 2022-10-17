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


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here\

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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            switch (gameState)
            {
                case Screen.MainMenu:
                    if (Process.SingleKeyPress(Keys.S, Process.CurrentKbState))
                    {
                        gameState = Screen.FirstWave;
                    }
                    break;
                case Screen.FirstWave:
                    if (Process.SingleKeyPress(Keys.F, Process.CurrentKbState))
                    {
                        gameState = Screen.SecondWave;
                    }
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
            Process.PreviouskbState = Process.CurrentKbState;
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
    }
}