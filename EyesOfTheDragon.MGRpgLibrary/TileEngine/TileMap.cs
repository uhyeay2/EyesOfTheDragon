using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace EyesOfTheDragon.MGRpgLibrary.TileEngine
{
    public class TileMap
    {
        #region Private Fields

        private List<Tileset> _tilesets;

        private List<MapLayer> _mapLayers;

        #endregion

        #region Constructor

        public TileMap(List<Tileset> tilesets, List<MapLayer> layers)
        {
            _tilesets = tilesets;
            _mapLayers = layers;
        }

        public TileMap(Tileset tileset, MapLayer layer) : this([tileset],[layer]) 
        { 
        }

        #endregion

        #region Public Methods

        public void Draw(SpriteBatch spriteBatch)
        {
            var destination = new Rectangle(0, 0, Engine.TileWidth, Engine.TileHeight);

            Tile tile;

            foreach (var layer in _mapLayers)
            {
                for (int y = 0; y < layer.Height; y++)
                {
                    destination.Y = y * Engine.TileHeight;

                    for (int x = 0; x < layer.Width; x++)
                    {
                        tile = layer.GetTile(x, y);
                    
                        destination.X = x * Engine.TileWidth;
                        
                        spriteBatch.Draw(
                            texture: _tilesets[tile.Tileset].Image,
                            destinationRectangle: destination,
                            sourceRectangle: _tilesets[tile.Tileset].SourceRectangles[tile.TileIndex],
                            Color.White
                        );
                    }
                }
            }
        }

        #endregion

    }
}
