namespace EyesOfTheDragon.MGRpgLibrary.TileEngine
{
    public class Tile
    {
        #region Private Fields

        private int _tileIndex;

        private int _tileset;

        #endregion

        #region Constructor
        public Tile(int tileIndex, int tileset)
        {
            _tileIndex = tileIndex;

            _tileset = tileset;
        }
        #endregion

        #region Public Accessors

        public int TileIndex => _tileIndex;

        public int Tileset => _tileset;

        #endregion
    }
}
