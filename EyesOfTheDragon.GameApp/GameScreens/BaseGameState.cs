using EyesOfTheDragon.GameApp.Constants.ContentPaths;
using EyesOfTheDragon.MGRpgLibrary.Controls;
using EyesOfTheDragon.MGRpgLibrary.DrawableGameComponents;
using EyesOfTheDragon.MGRpgLibrary.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace EyesOfTheDragon.GameApp.GameScreens
{
    public abstract partial class BaseGameState : GameState
    {
        #region Protected Members

        protected Game1 _game;

        protected ControlManager _controlManager;

        protected PlayerIndex _playerIndexInControl;

        #endregion

        protected BaseGameState(Game game, GameStateManager stateManager) : base(game, stateManager)
        {
            if (game is Game1 g)
            {
                _game = g;
            }
            else
            {
                throw new ApplicationException($"Game must be type of {typeof(Game1)} - Game Type Received: {game?.GetType()}");
            }

            _playerIndexInControl = PlayerIndex.One;
        }

        #region Method Overrides

        protected override void LoadContent()
        {
            var menuFont = Game.Content.Load<SpriteFont>(Fonts.ControlFont);

            _controlManager = new ControlManager(menuFont);

            base.LoadContent();
        }

        #endregion
    }
}
