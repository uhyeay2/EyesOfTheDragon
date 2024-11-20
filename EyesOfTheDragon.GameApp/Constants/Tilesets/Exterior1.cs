using EyesOfTheDragon.MGRpgLibrary.TileEngine;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EyesOfTheDragon.GameApp.Constants.Tilesets
{
    public static class Exterior1
    {
        public const string ContentPath = @"Tilesets/tileset1";

        public const int TileWidthInPixels = 32;

        public const int TileHeightInPixels = 32;

        public const int CountOfTilesWide = 8;

        public const int CountOfTilesHigh = 8;

        public static Tileset LoadTileset(ContentManager c)
        {
            var image = c.Load<Texture2D>(ContentPath);

            return new Tileset(image, CountOfTilesWide, CountOfTilesHigh, TileWidthInPixels, TileHeightInPixels);
        }
    }
}
