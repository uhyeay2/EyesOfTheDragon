using EyesOfTheDragon.GameApp;
using EyesOfTheDragon.MGRpgLibrary.TileEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EyesOfTheDragon.MGRpgLibrary.Components
{
    public class Player
    {
        #region Private Fields

        private Camera _camera;

        private Game1 _game;

        #endregion

        #region Constructor

        public Player(Game1 game)
        {
            _game = game;
            _camera = new Camera(_game.ScreenRectangle);
        }

        #endregion

        #region Public Accessors

        public Camera Camera => _camera;

        #endregion

        #region Public Methods

        public void Update(GameTime gameTime)
        {
            _camera.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //TODO: Draw the player!
        }

        #endregion
    }
}
