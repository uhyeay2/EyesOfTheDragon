using EyesOfTheDragon.GameApp.Constants.ContentPaths;
using EyesOfTheDragon.MGRpgLibrary.Controls;
using EyesOfTheDragon.MGRpgLibrary.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace EyesOfTheDragon.GameApp.GameScreens
{
    public class StartMenuScreen : BaseGameState
    {
        #region Private Members

        private PictureBox _backgroundImage;

        private PictureBox _arrowImage;

        private LinkLabel _startGameLinkLabel;

        private LinkLabel _loadGameLinkLabel;

        private LinkLabel _exitGameLinkLabel;

        private float _maxItemWidth = 0f;

        #endregion

        #region Constructor

        public StartMenuScreen(Game game, GameStateManager stateManager) : base(game, stateManager)
        {
        }

        #endregion

        #region Method Overrides

        protected override void LoadContent()
        {
            base.LoadContent();

            var content = _game.Content;

            _backgroundImage = new PictureBox(content.Load<Texture2D>(BackgroundImages.TitleScreen), _game.ScreenRectangle);

            _controlManager.Add(_backgroundImage);

            var arrowTexture = content.Load<Texture2D>(GUI.LeftArrow);
            _arrowImage = new PictureBox(arrowTexture, new Rectangle(0, 0, arrowTexture.Width, arrowTexture.Height));

            _controlManager.Add(_arrowImage);

            _startGameLinkLabel = new();
            _startGameLinkLabel.Text = "The story begins";
            _startGameLinkLabel.Size = _startGameLinkLabel.SpriteFont.MeasureString(_startGameLinkLabel.Text);
            _startGameLinkLabel.Selected += new EventHandler(MenuItemSelectedEventHandler);
            _controlManager.Add(_startGameLinkLabel);

            _loadGameLinkLabel = new();
            _loadGameLinkLabel.Text = "The story continues";
            _loadGameLinkLabel.Size = _loadGameLinkLabel.SpriteFont.MeasureString(_loadGameLinkLabel.Text);
            _loadGameLinkLabel.Selected += new EventHandler(MenuItemSelectedEventHandler);
            _controlManager.Add(_loadGameLinkLabel);

            _exitGameLinkLabel = new();
            _exitGameLinkLabel.Text = "The story ends";
            _exitGameLinkLabel.Size = _exitGameLinkLabel.SpriteFont.MeasureString(_exitGameLinkLabel.Text);
            _exitGameLinkLabel.Selected += new EventHandler(MenuItemSelectedEventHandler);
            _controlManager.Add(_exitGameLinkLabel);

            _controlManager.NextControl();
            _controlManager.FocusChangedEventHandler += new EventHandler(ControlManagerChangeFocusEventHandler);

            var position = new Vector2(350, 500);

            foreach (var c in _controlManager)
            {
                if (c is LinkLabel l)
                {
                    if (c.Size.X > _maxItemWidth)
                    {
                        _maxItemWidth = c.Size.X;
                    }

                    c.Position = position;

                    position.Y += c.Size.Y + 5f;
                }
            }

            ControlManagerChangeFocusEventHandler(_startGameLinkLabel, null);
        }

        public override void Update(GameTime gameTime)
        {
            _controlManager.Update(gameTime, _playerIndexInControl);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _game.SpriteBatch.Begin();

            base.Draw(gameTime);

            _controlManager.Draw(_game.SpriteBatch);

            _game.SpriteBatch.End();
        }

        #endregion

        #region Private Event Handler Methods

        private void MenuItemSelectedEventHandler(object sender, EventArgs e)
        {
            if (sender == _startGameLinkLabel)
            {
                _stateManager.PushState(_game.CharacterGeneratorScreen);
            }
            else if (sender == _loadGameLinkLabel)
            {
                _stateManager.PushState(_game.GamePlayScreen);
            }
            else if (sender == _exitGameLinkLabel)
            {
                _game.Exit();
            }
        }

        private void ControlManagerChangeFocusEventHandler(object sender, EventArgs e)
        {
            if (sender is Control c)
            {
                var position = new Vector2(c.Position.X + _maxItemWidth + 10f, c.Position.Y);

                _arrowImage.SetPosition(position);
            }
        }

        #endregion
    }
}
