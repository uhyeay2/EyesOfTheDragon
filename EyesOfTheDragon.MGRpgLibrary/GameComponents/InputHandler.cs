using EyesOfTheDragon.MGRpgLibrary.Enums;
using EyesOfTheDragon.MGRpgLibrary.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace EyesOfTheDragon.MGRpgLibrary.GameComponents
{
    public class InputHandler : GameComponent
    {
        #region Private Static Fields For Keyboard, Mouse, and GamePad State

        private static KeyboardState _keyboardState;
        private static KeyboardState _previousKeyboardState;

        private static MouseState _mouseState;
        private static MouseState _previousMouseState;

        private static GamePadState[] _gamePadStates;
        private static GamePadState[] _previousGamePadStates;

        #endregion

        #region Public Accessors For Private Static Keyboard, Mouse, and GamePad State Fields

        public static KeyboardState KeyboardState => _keyboardState;
        public static KeyboardState PreviousKeyboardState => _previousKeyboardState;

        public static MouseState MouseState => _mouseState;
        public static MouseState PreviousMouseState => _previousMouseState;

        public static GamePadState[] GamePadStates => _gamePadStates;
        public static GamePadState[] PreviousGamePadStates => _previousGamePadStates;

        #endregion

        #region Constructor

        public InputHandler(Game game) : base(game)
        {
            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();
            _gamePadStates = new GamePadState[Enum.GetValues<PlayerIndex>().Length];

            SetGamePadStates();
        }

        #endregion

        #region MonoGame Method Overrides

        public override void Update(GameTime gameTime)
        {
            // Update Keyboard States
            _previousKeyboardState = _keyboardState;
            _keyboardState = Keyboard.GetState();

            // Update Mouse States
            _previousMouseState = _mouseState;
            _mouseState = Mouse.GetState();

            // Update GamePad States
            _previousGamePadStates = (GamePadState[])_gamePadStates.Clone();
            SetGamePadStates();

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
            _previousMouseState = _mouseState;
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

        #region Mouse Input Methods

        public static bool IsMousePressed(MouseButton button) => 
            button switch
            {
                MouseButton.Left => _mouseState.LeftButton.IsPressed(_previousMouseState.LeftButton),
                MouseButton.Middle => _mouseState.MiddleButton.IsPressed(_previousMouseState.MiddleButton),
                MouseButton.Right => _mouseState.RightButton.IsPressed(_previousMouseState.RightButton),
                _ => false
            };

        public static bool IsMouseReleased(MouseButton button) =>
            button switch
            {
                MouseButton.Left => _mouseState.LeftButton.IsReleased(_previousMouseState.LeftButton),
                MouseButton.Middle => _mouseState.MiddleButton.IsReleased(_previousMouseState.MiddleButton),
                MouseButton.Right => _mouseState.RightButton.IsReleased(_previousMouseState.RightButton),
                _ => false
            };

        public static bool IsMouseUp(MouseButton button) =>
            button switch
            {
                MouseButton.Left => _mouseState.LeftButton.IsUp(),
                MouseButton.Middle => _mouseState.MiddleButton.IsUp(),
                MouseButton.Right => _mouseState.RightButton.IsUp(),
                _ => false
            };

        public static bool IsMouseDown(MouseButton button) =>
            button switch
            {
                MouseButton.Left => _mouseState.LeftButton.IsDown(),
                MouseButton.Middle => _mouseState.MiddleButton.IsDown(),
                MouseButton.Right => _mouseState.RightButton.IsDown(),
                _ => false
            };

        public static Point MouseAsPoint() => _mouseState.ToPoint();

        #endregion

        #region GamePad Input Methods

        public static bool IsGamePadButtonReleased(Buttons button, PlayerIndex index) =>
            _gamePadStates[(int)index].IsButtonUp(button) && _previousGamePadStates[(int)index].IsButtonDown(button);

        public static bool IsGamePadButtonPressed(Buttons button, PlayerIndex index) =>
            _gamePadStates[(int)index].IsButtonDown(button) && _previousGamePadStates[(int)index].IsButtonUp(button);

        public static bool IsGamePadButtonDown(Buttons button, PlayerIndex index) =>
            _gamePadStates[(int)index].IsButtonDown(button);

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Helper method to loop through and update the GamePadState for each PlayerIndex.
        /// Does not set the PreviousGamePadState before updating.
        /// </summary>
        private void SetGamePadStates()
        {
            foreach (var i in Enum.GetValues<PlayerIndex>())
            {
                _gamePadStates[(int)i] = GamePad.GetState(i);
            }
        }

        #endregion
    }
}
