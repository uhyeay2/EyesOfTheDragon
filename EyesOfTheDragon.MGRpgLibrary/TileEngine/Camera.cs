using EyesOfTheDragon.MGRpgLibrary.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EyesOfTheDragon.MGRpgLibrary.TileEngine
{
    public class Camera
    {
        #region Private Fields

        private Vector2 _position;
        private float _speed;
        private float _zoom;
        private Rectangle _viewportRectangle;

        #endregion

        #region Constructor

        public Camera(Rectangle viewportRect)
        {
            _speed = 4f;
            _zoom = 1f;
            _viewportRectangle = viewportRect;
        }
        public Camera(Rectangle viewportRect, Vector2 position)
        {
            _speed = 4f;
            _zoom = 1f;
            _viewportRectangle = viewportRect;
            _position = position;
        }

        #endregion

        #region Public Accessors

        public Vector2 Position => _position;

        public float Speed => _speed;

        public float Zoom => _zoom;

        #endregion

        #region Public Methods

        public void Update(GameTime gameTime)
        {
            var motion = Vector2.Zero;

            if (InputHandler.IsKeyDown(Keys.Left))
            {
                motion.X -= _speed;
            }
            
            if (InputHandler.IsKeyDown(Keys.Right))
            {
                motion.X += _speed;
            }

            if (InputHandler.IsKeyDown(Keys.Up))
            {
                motion.Y -= _speed;
            }
            
            if (InputHandler.IsKeyDown(Keys.Down))
            {
                motion.Y += _speed;
            }

            if (motion != Vector2.Zero)
            {
                motion = Vector2.Normalize(motion);
            }

            _position += motion * _speed;

            ClampCamera();
        }

        private void ClampCamera()
        {
            _position.X = MathHelper.Clamp(_position.X, 0, TileMap.WidthInPixels - _viewportRectangle.Width);

            _position.Y = MathHelper.Clamp(_position.Y, 0, TileMap.HeightInPixels - _viewportRectangle.Height);
        }

        #endregion
    }
}
