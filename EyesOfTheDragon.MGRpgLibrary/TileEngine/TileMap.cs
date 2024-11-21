using EyesOfTheDragon.MGRpgLibrary.Exceptions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace EyesOfTheDragon.MGRpgLibrary.TileEngine
{
    public class TileMap
    {
        #region Private Static Members

        private static int _mapWidth;
        private static int _mapHeight;

        #endregion

        #region Private Fields

        private List<Tileset> _tilesets;
        private List<MapLayer> _mapLayers;

        #endregion

        #region Constructor

        public TileMap(List<Tileset> tilesets, List<MapLayer> layers)
        {
            _tilesets = tilesets;
            _mapLayers = layers;

            //TODO: I don't feel great about setting static fields in an instance constructor. Look into refactoring this down the line
            _mapWidth = _mapLayers[0].Width;
            _mapHeight = _mapLayers[0].Height;

            for (int i = 1; i < layers.Count; i++)
            {
                // Throws exception when there is a discrepancy between any MapLayer width/height
                if (_mapWidth != _mapLayers[i].Width || _mapHeight != _mapLayers[i].Height)
                    throw new MapLayerSizeException();
            }
        }

        public TileMap(Tileset tileset, MapLayer layer) : this([tileset],[layer]) 
        { 
        }

        #endregion

        #region Public Static Accessors

        public static int WidthInPixels => _mapWidth * Engine.TileWidth;
        
        public static int HeightInPixels => _mapHeight * Engine.TileHeight; 

        #endregion

        #region Public Methods

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            var destination = new Rectangle(0, 0, Engine.TileWidth, Engine.TileHeight);

            Tile tile;

            foreach (var layer in _mapLayers)
            {
                for (int y = 0; y < layer.Height; y++)
                {
                    destination.Y = y * Engine.TileHeight - (int)camera.Position.Y;

                    for (int x = 0; x < layer.Width; x++)
                    {
                        tile = layer.GetTile(x, y);
                    
                        if (tile == null || tile.TileIndex == -1 || tile.Tileset == -1)
                        {
                            continue;
                        }

                        destination.X = x * Engine.TileWidth - (int)camera.Position.X;
                        
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

        public void AddLayer(MapLayer mapLayer)
        {
            if (mapLayer.Width != _mapWidth || mapLayer.Height != _mapHeight)
            {
                throw new MapLayerSizeException();
            }

            _mapLayers.Add(mapLayer);
        }

        #endregion

    }
}
