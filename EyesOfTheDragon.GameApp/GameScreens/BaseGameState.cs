using EyesOfTheDragon.MGRpgLibrary.DrawableGameComponents;
using EyesOfTheDragon.MGRpgLibrary.GameComponents;
using Microsoft.Xna.Framework;
using System;

namespace EyesOfTheDragon.GameApp.GameScreens
{
    public abstract partial class BaseGameState : GameState
    {
        protected Game1 _game;

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
        }
    }
}
