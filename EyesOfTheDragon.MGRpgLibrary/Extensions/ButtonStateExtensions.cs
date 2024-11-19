using Microsoft.Xna.Framework.Input;

namespace EyesOfTheDragon.MGRpgLibrary.Extensions
{
    public static class ButtonStateExtensions
    {
        public static bool IsDown(this ButtonState buttonState) => 
            buttonState == ButtonState.Pressed;

        public static bool IsUp(this ButtonState buttonState) => 
            buttonState == ButtonState.Released;

        public static bool IsPressed(this ButtonState buttonState, ButtonState previousButtonState) => 
            buttonState.IsDown() && previousButtonState.IsUp();

        public static bool IsReleased(this ButtonState buttonState, ButtonState previousButtonState) =>
            buttonState.IsUp() && previousButtonState.IsDown();
    }
}
