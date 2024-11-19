using EyesOfTheDragon.MGRpgLibrary.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace EyesOfTheDragon.MGRpgLibrary.Controls
{
    public class ControlManager : List<Control>
    {
        #region Private Members

        private static SpriteFont _spriteFont;

        private int _selectedControl = 0;

        #endregion

        #region Constructors

        public ControlManager(SpriteFont spriteFont) : base()
        {
            _spriteFont = spriteFont;
        }
        public ControlManager(SpriteFont spriteFont, int capacity) : base(capacity)
        {
            _spriteFont = spriteFont;
        }
        public ControlManager(SpriteFont spriteFont, IEnumerable<Control> collection) : base(collection)
        {
            _spriteFont = spriteFont;
        }

        #endregion

        #region Event Handlers

        public event EventHandler FocusChangedEventHandler;

        #endregion

        #region Public Accessors

        public static SpriteFont SpriteFont => _spriteFont;

        #endregion

        #region Public Methods

        public void Update(GameTime gameTime, PlayerIndex playerIndex)
        {
            if (Count == 0)
            {
                return;
            }

            foreach (var control in this)
            {
                if (control.Enabled)
                {
                    control.Update(gameTime);
                }

                if (control.HasFocus)
                {
                    control.HandleInput(playerIndex);
                }

                if (InputHandler.IsGamePadButtonPressed(Buttons.LeftThumbstickUp, playerIndex)
                || InputHandler.IsGamePadButtonPressed(Buttons.DPadUp, playerIndex)
                || InputHandler.IsKeyPressed(Keys.Up))
                {
                    PreviousControl();
                }

                if (InputHandler.IsGamePadButtonPressed(Buttons.LeftThumbstickDown, playerIndex)
                || InputHandler.IsGamePadButtonPressed(Buttons.DPadDown, playerIndex)
                || InputHandler.IsKeyPressed(Keys.Down))
                {
                    NextControl();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var control in this)
            {
                if (control.Visible)
                {
                    control.Draw(spriteBatch);
                }
            }
        }

        public void NextControl()
        {
            if (Count == 0)
            {
                return;
            }

            int currentControl = _selectedControl;

            this[_selectedControl].HasFocus = false;

            do
            {
                _selectedControl--;

                if (_selectedControl < 0)
                {
                    _selectedControl = Count - 1;
                }

                if (this[_selectedControl].TabStop && this[_selectedControl].Enabled)
                {
                    FocusChangedEventHandler?.Invoke(this[_selectedControl], null);

                    break;
                }

            } while (currentControl != _selectedControl);

            this[_selectedControl].HasFocus = true;
        }

        public void PreviousControl()
        {
            if (Count == 0)
            {
                return;
            }

            var currentControl = _selectedControl;

            this[_selectedControl].HasFocus = false;

            do
            {
                _selectedControl++;

                if (_selectedControl == Count)
                {
                    _selectedControl = 0;
                }

                if (this[_selectedControl].TabStop && this[_selectedControl].Enabled)
                {
                    FocusChangedEventHandler?.Invoke(this[_selectedControl], null);

                    break;
                }

            } while (currentControl != _selectedControl);

            this[_selectedControl].HasFocus = true;
        }

        #endregion
    }
}
