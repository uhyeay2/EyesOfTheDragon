using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EyesOfTheDragon.MGRpgLibrary.TileEngine
{
    public class Tileset
    {
        #region Private Fields

        private Texture2D _image;
        private int _tileWidthInPixels;
        private int _tileHeightInPixels;
        private int _countOfTilesWide;
        private int _countOfTilesHigh;
        private Rectangle[] _sourceRectangles;

        #endregion

        #region Constructor

        public Tileset(Texture2D image, int countofTilesWide, int countOfTilesHigh, int tileWidthInPixels, int tileHeightInPixels)
        {
            _image = image;
            _countOfTilesWide = countofTilesWide;
            _countOfTilesHigh = countOfTilesHigh;
            _tileWidthInPixels = tileWidthInPixels;
            _tileHeightInPixels = tileHeightInPixels;

            var tileCount = countofTilesWide * countOfTilesHigh;

            _sourceRectangles = new Rectangle[tileCount];

            var index = 0;

            for (int y = 0; y < countOfTilesHigh; y++)
            {
                for (int x = 0; x < countofTilesWide; x++)
                {
                    _sourceRectangles[index] = new Rectangle(
                        x * tileWidthInPixels, 
                        y * tileHeightInPixels, 
                        tileWidthInPixels, 
                        tileHeightInPixels
                    );                    

                    index++;
                }
            }
        }

        #endregion

        #region Public Accessors

        public Texture2D Image => _image;

        public int TileWidthInPixels => _tileWidthInPixels;

        public int TileHeightInPixels => _tileHeightInPixels;

        public int CountOfTilesWide => _countOfTilesWide;

        public int CountOfTilesHigh => _countOfTilesHigh;

        public Rectangle[] SourceRectangles => (Rectangle[])_sourceRectangles.Clone();

        #endregion

    }
}
