using EyesOfTheDragon.GameApp.Constants.ContentPaths;
using EyesOfTheDragon.MGRpgLibrary.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EyesOfTheDragon.GameApp.GameScreens
{
    public class TitleScreen : BaseGameState
    {
        #region Private Members

        private Texture2D _backgroundImage;

        #endregion

        #region Constructor 
        
        public TitleScreen(Game game, GameStateManager stateManager) : base(game, stateManager)
        {
        }

        #endregion

        #region MonoGame Method Overloads

        protected override void LoadContent()
        {
            var content = _game.Content;

            _backgroundImage = content.Load<Texture2D>(BackgroundImages.TitleScreen);

            base.LoadContent();
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

            _game.SpriteBatch.End();
        }

        #endregion

    }
}
