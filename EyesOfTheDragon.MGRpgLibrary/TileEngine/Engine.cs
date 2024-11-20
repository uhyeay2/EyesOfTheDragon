using Microsoft.Xna.Framework;

namespace EyesOfTheDragon.MGRpgLibrary.TileEngine
{
    public class Engine
    {
        #region Private Static Members

        private static int _tileWidth;

        private static int _tileHeight;

        #endregion

        #region Public Static Accessors

        public static int TileWidth => _tileWidth;

        public static int TileHeight => _tileHeight;

        #endregion

        #region Constructor

        public Engine(int tileWidth, int tileHeight)
        {
            _tileWidth = tileWidth;

            _tileHeight = tileHeight;
        }

        #endregion

        #region Public Static Methods

        public static Point GetTileCellPointByVector2(Vector2 position) =>
            new ((int)position.X / _tileWidth, (int)position.Y / _tileHeight);

        #endregion
    }
}
