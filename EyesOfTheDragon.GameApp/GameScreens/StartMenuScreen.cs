using EyesOfTheDragon.MGRpgLibrary.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EyesOfTheDragon.GameApp.GameScreens
{
    public class StartMenuScreen : BaseGameState
    {
        public StartMenuScreen(Game game, GameStateManager stateManager) : base(game, stateManager)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            if (InputHandler.IsKeyReleased(Keys.Escape))
            {
                Game.Exit();
            }
            base.Draw(gameTime);
        }
    }
}
