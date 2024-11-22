using Microsoft.Xna.Framework;

namespace EyesOfTheDragon.MGRpgLibrary.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 NormalizeIfNotVector2Zero(this Vector2 vector)
        {
            if (vector != Vector2.Zero)
            {
                vector.Normalize();
            }

            return vector;
        }
    }
}
