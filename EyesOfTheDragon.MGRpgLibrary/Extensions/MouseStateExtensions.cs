using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EyesOfTheDragon.MGRpgLibrary.Extensions
{
    public static class MouseStateExtensions
    {
        public static Point ToPoint(this MouseState m) => new(m.X, m.Y);

        public static Vector2 ToVector2(this MouseState m) => new(m.X, m.Y);
    }
}
