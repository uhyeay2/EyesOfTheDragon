using System;

namespace EyesOfTheDragon.MGRpgLibrary.Exceptions
{
    /// <summary>
    /// MapLayers are required to be the same Width/Height. This exception is thrown in places where this rule is enforced.
    /// </summary>
    public class MapLayerSizeException : Exception
    {
    }
}
