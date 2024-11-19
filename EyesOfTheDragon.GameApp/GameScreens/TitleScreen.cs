using EyesOfTheDragon.GameApp.Constants.ContentPaths;
using EyesOfTheDragon.MGRpgLibrary.Controls;
using EyesOfTheDragon.MGRpgLibrary.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace EyesOfTheDragon.GameApp.GameScreens
{
    public class TitleScreen : BaseGameState
    {
        #region Private Members

        private Texture2D _backgroundImage;

        private LinkLabel _startMenuLabel;

        #endregion

        #region Constructor 
        
        public TitleScreen(Game game, GameStateManager stateManager) : base(game, stateManager)
        {
        }

        #endregion

        #region MonoGame Method Overloads

        protected override void LoadContent()
        {
            base.LoadContent();

            _backgroundImage = _game.Content.Load<Texture2D>(BackgroundImages.TitleScreen);

            _startMenuLabel = new LinkLabel()
            {
                Position = new Vector2(350, 600),
                Text = "Press ENTER to begin",
                Color = Color.White,
                TabStop = true,
                HasFocus = true                
            };

            _startMenuLabel.Selected += new EventHandler(StartMenuLabelSelectedEventHandler);

            _controlManager.Add(_startMenuLabel);
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

            _game.SpriteBatch.Draw(
                _backgroundImage,
                _game.ScreenRectangle,
                Color.White
            );

            _controlManager.Draw(_game.SpriteBatch);

            _game.SpriteBatch.End();
        }

        #endregion

        #region Private Functions For Event Handlers

        private void StartMenuLabelSelectedEventHandler(object sender, EventArgs e) =>
            _stateManager.PushState(_game.StartMenuScreen);
        
        #endregion
    }
}
