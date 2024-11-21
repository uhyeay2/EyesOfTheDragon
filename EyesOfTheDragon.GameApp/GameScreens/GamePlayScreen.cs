using EyesOfTheDragon.GameApp.Constants.Tilesets;
using EyesOfTheDragon.MGRpgLibrary.Components;
using EyesOfTheDragon.MGRpgLibrary.GameComponents;
using EyesOfTheDragon.MGRpgLibrary.TileEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace EyesOfTheDragon.GameApp.GameScreens
{
    public class GamePlayScreen : BaseGameState
    {
        #region Private Fields

        private Engine engine = new Engine(32, 32);
        private TileMap _map;
        private Player _player;

        #endregion

        #region Constructor

        public GamePlayScreen(Game game, GameStateManager stateManager) : base(game, stateManager)
        {
            _player = new Player(_game);
        }

        #endregion

        #region Method Overrides

        protected override void LoadContent()
        {
            base.LoadContent();

            var tileset1 = Exterior1.LoadTileset(_game.Content);
            var tileset2 = Exterior2.LoadTileset(_game.Content);

            var tilesets = new List<Tileset>() { tileset1, tileset2 };

            var groundLayer = new MapLayer(40, 40);

            for (int y = 0; y < groundLayer.Height; y++)
            {
                for (int x = 0; x < groundLayer.Width; x++)
                {
                    Tile tile = new Tile(0, 0);
                    groundLayer.SetTile(x, y, tile);
                }
            }

            MapLayer splatter = new MapLayer(40, 40);

            Random random = new Random();
            
            for (int i = 0; i < 80; i++)
            {
                int x = random.Next(0, 40);
                int y = random.Next(0, 40);
                int index = random.Next(2, 14);
                Tile tile = new Tile(index, 0);
                splatter.SetTile(x, y, tile);
            }

            splatter.SetTile(1, 0, new Tile(0, 1));
            splatter.SetTile(2, 0, new Tile(2, 1));
            splatter.SetTile(3, 0, new Tile(0, 1));

            _map = new TileMap(tilesets, [groundLayer, splatter]);
        }

        public override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _game.SpriteBatch.Begin(
                SpriteSortMode.Immediate,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                Matrix.Identity
            );

            _map.Draw(_game.SpriteBatch, _player.Camera);

            base.Draw(gameTime);

            _game.SpriteBatch.End();
        }

        #endregion
    }
}
