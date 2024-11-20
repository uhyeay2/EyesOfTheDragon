using EyesOfTheDragon.GameApp.Constants.Tilesets;
using EyesOfTheDragon.MGRpgLibrary.GameComponents;
using EyesOfTheDragon.MGRpgLibrary.TileEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EyesOfTheDragon.GameApp.GameScreens
{
    public class GamePlayScreen : BaseGameState
    {
        #region Private Fields

        private Engine engine = new Engine(32, 32);
        private Tileset _tileset;
        private TileMap _map;

        #endregion

        #region Constructor

        public GamePlayScreen(Game game, GameStateManager stateManager) : base(game, stateManager)
        {
        }

        #endregion

        #region Method Overrides

        protected override void LoadContent()
        {
            _tileset = Exterior1.LoadTileset(_game.Content);

            var groundLayer = new Tile[32, 32];

            for (int y = 0; y < groundLayer.GetLength(0); y++)
            {
                for (int x = 0; x < groundLayer.GetLength(1); x++)
                {
                    groundLayer[y, x] = new Tile(1, 0);
                }
            }

            _map = new TileMap(_tileset, new MapLayer(groundLayer));

            base.LoadContent();
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

            _map.Draw(_game.SpriteBatch);

            base.Draw(gameTime);

            _game.SpriteBatch.End();
        }

        #endregion
    }
}
