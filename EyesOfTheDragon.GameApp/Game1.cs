using EyesOfTheDragon.GameApp.Constants;
using EyesOfTheDragon.GameApp.GameScreens;
using EyesOfTheDragon.MGRpgLibrary.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EyesOfTheDragon.GameApp
{
    public class Game1 : Game
    {
        #region Private Fields

        private GraphicsDeviceManager _graphics;        
        
        private SpriteBatch _spriteBatch;

        private GameStateManager _gameStateManager;

        private readonly Rectangle _screenRectangle = new (x: 0, y: 0, ScreenSize.DefaultWidth, ScreenSize.DefaultHeight);

        private TitleScreen _titleScreen;
        private StartMenuScreen _startMenuScreen;
        private GamePlayScreen _gamePlayScreen;
        private CharacterGeneratorScreen _characterGeneratorScreen;

        #endregion

        #region Constructor

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Components.Add(new InputHandler(this));

            _gameStateManager = new GameStateManager(this);
            Components.Add(_gameStateManager);

            _titleScreen = new(this, _gameStateManager);
            _startMenuScreen = new(this, _gameStateManager);
            _gamePlayScreen = new(this, _gameStateManager);
            _characterGeneratorScreen = new(this, _gameStateManager);

            _gameStateManager.ChangeState(TitleScreen);
        }

        #endregion

        #region Public Accessors

        public Rectangle ScreenRectangle => _screenRectangle;

        public SpriteBatch SpriteBatch => _spriteBatch;

        public TitleScreen TitleScreen => _titleScreen;
        public StartMenuScreen StartMenuScreen => _startMenuScreen;
        public GamePlayScreen GamePlayScreen => _gamePlayScreen;
        public CharacterGeneratorScreen CharacterGeneratorScreen => _characterGeneratorScreen;

        #endregion

        #region MonoGame Method Overrides

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = ScreenRectangle.Width;
            _graphics.PreferredBackBufferHeight = ScreenRectangle.Height;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);            

            base.Draw(gameTime);
        }

        #endregion
    }
}
