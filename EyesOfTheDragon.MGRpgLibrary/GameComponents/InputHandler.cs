using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EyesOfTheDragon.MGRpgLibrary.GameComponents
{
    public class InputHandler : GameComponent
    {
        #region Private Static Fields For KeyboardState

        private static KeyboardState _keyboardState;

        private static KeyboardState _previousKeyboardState;

        #endregion

        #region Public Accessors For Private Static Keyboard State Fields

        public static KeyboardState KeyboardState => _keyboardState;

        public static KeyboardState PreviousKeyboardState => _previousKeyboardState;

        #endregion

        #region Constructor

        public InputHandler(Game game) : base(game)
        {
            _keyboardState = Keyboard.GetState();
        }

        #endregion

        #region MonoGame Method Overrides

        public override void Update(GameTime gameTime)
        {
            _previousKeyboardState = _keyboardState;

            _keyboardState = Keyboard.GetState();
            
            base.Update(gameTime);
        }

        #endregion

        #region General Methods

        /// <summary>
        /// Sets the PreviousKeyboardState to the Current KeyboardState. This is like flushing an input buffer.
        /// This is helpful when you are changing state, to ensure there is no spill over into the next state.
        /// After this method is called, if IsKeyReleased() or IsKeyPressed() would have previously returned true, they no longer would.
        /// </summary>
        public static void Flush()
        {
            _previousKeyboardState = _keyboardState;
        }

        #endregion

        #region Keyboard Input Methods

        /// <summary>
        /// Returns true if the key is no longer being pressed, however the key was being pressed in the previous keyboard state.
        /// This would mean the user just stopped pressing the key.
        /// </summary>
        public static bool IsKeyReleased(Keys key) =>
            _keyboardState.IsKeyUp(key) && _previousKeyboardState.IsKeyDown(key);

        /// <summary>
        /// Returns true if the key is being pressed, and was also being pressed in the previous keyboard state.
        /// This would mean the user just started pressing the key.
        /// </summary>
        public static bool IsKeyPressed(Keys key) =>
            _keyboardState.IsKeyDown(key) && !_previousKeyboardState.IsKeyDown(key);

        /// <summary>
        /// Returns true if the key is being pressed.
        /// This would mean the user either just started pressing the key or the user has been holding the key for multiple frames/loops.
        /// </summary>
        public static bool IsKeyDown(Keys key) =>
            _keyboardState.IsKeyDown(key);

        #endregion

    }
}
