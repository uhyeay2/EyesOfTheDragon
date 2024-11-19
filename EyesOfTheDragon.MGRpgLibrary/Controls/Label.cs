using Microsoft.Xna.Framework.Graphics;

namespace EyesOfTheDragon.MGRpgLibrary.Controls
{
    public class Label : Control
    {
        public Label()
        {
            _tabStop = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(SpriteFont, Text, Position, Color);
        }
    }
}
