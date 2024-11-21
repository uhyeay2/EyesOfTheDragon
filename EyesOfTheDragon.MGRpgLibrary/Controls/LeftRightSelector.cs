using EyesOfTheDragon.MGRpgLibrary.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace EyesOfTheDragon.MGRpgLibrary.Controls
{
    public class LeftRightSelector : Control
    {
        #region Private Fields

        private const float _paddingBetweenTextures = 5f;

        private Texture2D _leftTexture;
        private Texture2D _rightTexture;
        private Texture2D _stopTexture;
        
        private List<string> _items = [];

        private Color _selectedColor = Color.Red;
        
        private int _maxItemWidth;
        private int _indexOfSelectedItem;

        #endregion

        #region Constructor

        public LeftRightSelector(Texture2D leftArrow, Texture2D rightArrow, Texture2D stop)
        {
            _leftTexture = leftArrow;
            _rightTexture = rightArrow;
            _stopTexture = stop;
            TabStop = true;
            Color = Color.White;
        }

        #endregion

        #region Event Handler

        public event EventHandler SelectionChangedEventHandler;
        
        #endregion

        #region Public Accessors

        public Color SelectedColor => _selectedColor;

        public int IndexOfSelectedItem => _indexOfSelectedItem;

        public string SelectedItem => _items[_indexOfSelectedItem];

        public List<string> Items => _items;

        #endregion

        #region Public Methods

        public void SetItems(string[] items, int maxWidth)
        {
            _items.Clear();

            _items.AddRange(items);

            _maxItemWidth = maxWidth;
        }

        protected void OnSelectionChanged()
        {
            SelectionChangedEventHandler?.Invoke(this, null);
        }

        #endregion

        #region Public Method Overrides

        public override void Draw(SpriteBatch spriteBatch)
        {
            var drawDestination = _position;

            // Draw leftTexture unless we are on first item, in which case we draw stopTexture
            spriteBatch.Draw(_indexOfSelectedItem != 0 ? _leftTexture : _stopTexture, drawDestination, Color.White);

            drawDestination.X += _leftTexture.Width + _paddingBetweenTextures;

            var itemWidth = _spriteFont.MeasureString(SelectedItem).X;
            var offset = (_maxItemWidth - itemWidth) / 2;

            drawDestination.X += offset;

            // Draw item/string with _selectedColor or default _color depending on if this control _hasFocus
            spriteBatch.DrawString(_spriteFont, SelectedItem, drawDestination, _hasFocus ? _selectedColor : _color);

            drawDestination.X += -1 * offset + _maxItemWidth + _paddingBetweenTextures;

            // Draw rightTexture unless we are on the last item, in which case we draw stopTexture
            spriteBatch.Draw(_indexOfSelectedItem != _items.Count - 1 ? _rightTexture : _stopTexture, drawDestination, Color.White);
        }

        public override void HandleInput(PlayerIndex playerIndex)
        {
            if (_items.Count == 0)
            {
                return;
            }

            if (InputHandler.IsGamePadButtonReleased(Buttons.LeftThumbstickLeft, playerIndex) 
            || InputHandler.IsGamePadButtonReleased(Buttons.DPadLeft, playerIndex) 
            || InputHandler.IsKeyReleased(Keys.Left))
            {
                _indexOfSelectedItem--;

                if (_indexOfSelectedItem < 0)
                {
                    _indexOfSelectedItem = 0;
                }

                OnSelectionChanged();
            }
            
            if (InputHandler.IsGamePadButtonReleased(Buttons.LeftThumbstickRight, playerIndex) 
            || InputHandler.IsGamePadButtonReleased(Buttons.DPadRight, playerIndex) 
            || InputHandler.IsKeyReleased(Keys.Right))
            {
                _indexOfSelectedItem++;

                if (_indexOfSelectedItem >= _items.Count)
                {
                    _indexOfSelectedItem = _items.Count - 1;
                }

                OnSelectionChanged();
            }
        }

        #endregion
    }
}
