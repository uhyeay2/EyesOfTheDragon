namespace EyesOfTheDragon.MGRpgLibrary.TileEngine
{
    public class MapLayer
    {
        #region Private Fields

        private Tile[,] _map;

        #endregion

        #region Constructor

        public MapLayer(Tile[,] map)
        {
            _map = (Tile[,])map.Clone();
        }

        public MapLayer(int width, int height)
        {
            _map = new Tile[height, width];
            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    _map[y, x] = new Tile(0, 0);
                }
            }
        }

        #endregion

        #region Public Accessors

        public int Width => _map.GetLength(1);

        public int Height => _map.GetLength(0);

        #endregion

        #region Public Methods

        public Tile GetTile(int x, int y) => _map[y, x];

        public void SetTile(int x, int y, Tile tile)
        {
            _map[y, x] = tile;
        }

        public void SetTile(int x, int y, int tileIndex, int tileset)
        {
            _map[y, x] = new Tile(tileIndex, tileset);
        }

        #endregion
    }
}
