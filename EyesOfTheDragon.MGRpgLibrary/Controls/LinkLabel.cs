using EyesOfTheDragon.MGRpgLibrary.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EyesOfTheDragon.MGRpgLibrary.Controls
{
    public class LinkLabel : Control
    {
        private Color _selectedColor = Color.Red;

        public Color SelectedColor => _selectedColor;

        public LinkLabel()
        {
            TabStop = true;
            HasFocus = false;
            Position = Vector2.Zero;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var color = HasFocus ? SelectedColor : Color;

            spriteBatch.DrawString(SpriteFont, Text, Position, color);

            base.Draw(spriteBatch);
        }

        public override void HandleInput(PlayerIndex playerIndex)
        {
            if (!HasFocus)
            {
                return;
            }

            if (InputHandler.IsKeyReleased(Keys.Enter)
            || InputHandler.IsGamePadButtonReleased(Buttons.A, playerIndex))
            {
                base.OnSelected(null);
            }

            if (InputHandler.IsMouseReleased(Enums.MouseButton.Left))
            {
                _size = SpriteFont.MeasureString(Text);

                var rectWhereLabelIs = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

                if (rectWhereLabelIs.Contains(InputHandler.MouseAsPoint()))
                {
                    base.OnSelected(null);
                }
            }

            base.HandleInput(playerIndex);
        }
    }
}
