using EyesOfTheDragon.GameApp.Constants.ContentPaths;
using EyesOfTheDragon.MGRpgLibrary.Controls;
using EyesOfTheDragon.MGRpgLibrary.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace EyesOfTheDragon.GameApp.GameScreens
{
    public class CharacterGeneratorScreen : BaseGameState
    {
        #region Private Fields

        private LeftRightSelector _nameSelector;
        private LeftRightSelector _genderSelector;
        private LeftRightSelector _classSelector;
        
        private PictureBox _backgroundImage;

        readonly string[] _genderOptions = ["Male", "Female", "Non-Binary"];
        readonly string[] _classOptions = ["Fighter", "Wizard", "Rogue", "Priest"];
        readonly string[] _maleNames = ["Balthazar", "Logan", "Alfred", "Johnson"];
        readonly string[] _femaleNames = ["Lucinda", "Cynthia", "Ezmarelda", "Millicent"];
        readonly string[] _nonBinaryNames = ["Jaime", "Kelly", "Jordan", "Pat"];

        #endregion

        #region Constructor

        public CharacterGeneratorScreen(Game game, GameStateManager stateManager) : base(game, stateManager)
        {
        }

        #endregion

        #region Method Overrides

        protected override void LoadContent()
        {
            base.LoadContent();

            var leftTexture = _game.Content.Load<Texture2D>(GUI.LeftArrow);
            var rightTexture = _game.Content.Load<Texture2D>(GUI.RightArrow);
            var stopTexture = _game.Content.Load<Texture2D>(GUI.StopBar);

            _backgroundImage = new PictureBox(_game.Content.Load<Texture2D>(BackgroundImages.TitleScreen), _game.ScreenRectangle);

            _controlManager.Add(_backgroundImage);

            var maxWidthOfLeftRightSelectors = 125;
            
            var controlsYPosition = 150;
            var verticalPaddingBetweenControls = 50;

            var titleLabel = new Label
            {
                Text = "Who will search for the Eyes of the Dragon?"
            };
            titleLabel.Size = titleLabel.SpriteFont.MeasureString(titleLabel.Text);
            // Center the position of titleLabel horizontally
            titleLabel.Position = new Vector2((_game.Window.ClientBounds.Width - titleLabel.Size.X) / 2, controlsYPosition);
            controlsYPosition += verticalPaddingBetweenControls;
            _controlManager.Add(titleLabel);

            _nameSelector = new LeftRightSelector(leftTexture, rightTexture, stopTexture);
            _nameSelector.SetItems(_maleNames, maxWidthOfLeftRightSelectors);
            _nameSelector.Position = new Vector2(titleLabel.Position.X, controlsYPosition);
            controlsYPosition += verticalPaddingBetweenControls;
            _controlManager.Add(_nameSelector);
            
            _genderSelector = new LeftRightSelector(leftTexture, rightTexture, stopTexture);
            _genderSelector.SetItems(_genderOptions, maxWidthOfLeftRightSelectors);
            _genderSelector.Position = new Vector2(titleLabel.Position.X, controlsYPosition);
            _genderSelector.SelectionChangedEventHandler += GenderSelectionChangedEventHandler;
            controlsYPosition += verticalPaddingBetweenControls;
            _controlManager.Add(_genderSelector);

            _classSelector = new LeftRightSelector(leftTexture, rightTexture, stopTexture);
            _classSelector.SetItems(_classOptions, maxWidthOfLeftRightSelectors);
            _classSelector.Position = new Vector2(titleLabel.Position.X, controlsYPosition);
            controlsYPosition += verticalPaddingBetweenControls;
            _controlManager.Add(_classSelector);

            // Add a little extra spacing above the 'Accept Character' control
            controlsYPosition += verticalPaddingBetweenControls;

            var acceptCharacterLinkLabel = new LinkLabel
            {
                Text = "Accept this character.",
                Position = new Vector2(titleLabel.Position.X, controlsYPosition)
            };

            acceptCharacterLinkLabel.Selected += new EventHandler(CharacterAcceptedEventHandler);
            
            _controlManager.Add(acceptCharacterLinkLabel);
            _controlManager.NextControl();
        }

        public override void Update(GameTime gameTime)
        {
            _controlManager.Update(gameTime, PlayerIndex.One);

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

        private void GenderSelectionChangedEventHandler(object sender, EventArgs e)
        {
            if (_genderSelector.IndexOfSelectedItem == 0)
            {
                _nameSelector.SetItems(_maleNames, 125);
            }
            else if (_genderSelector.IndexOfSelectedItem == 1)
            {
                _nameSelector.SetItems(_femaleNames, 125);
            }
            else
            {
                _nameSelector.SetItems(_nonBinaryNames, 125);
            }
        }

        private void CharacterAcceptedEventHandler(object sender, EventArgs e)
        {
            InputHandler.Flush();
            _stateManager.PopState();
            _stateManager.PushState(_game.GamePlayScreen);
        }

        #endregion
    }
}
