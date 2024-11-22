using EyesOfTheDragon.MGRpgLibrary.Enums;
using EyesOfTheDragon.MGRpgLibrary.GameComponents;
using EyesOfTheDragon.MGRpgLibrary.Sprites;
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
        
        public CameraMode CameraMode = CameraMode.Follow;

        public Vector2 Position => _position;

        public float Speed => _speed;

        public float Zoom => _zoom;

        #endregion

        #region Public Methods

        public void Update(GameTime gameTime)
        {
            if (CameraMode == CameraMode.Follow)
            {
                return;
            }

            var motion = Vector2.Zero;

            if (InputHandler.IsKeyDown(Keys.Left) 
            || InputHandler.IsGamePadButtonDown(Buttons.RightThumbstickLeft, PlayerIndex.One))
            {
                motion.X -= _speed;
            }
            
            if (InputHandler.IsKeyDown(Keys.Right)
            || InputHandler.IsGamePadButtonDown(Buttons.RightThumbstickRight, PlayerIndex.One))
            {
                motion.X += _speed;
            }

            if (InputHandler.IsKeyDown(Keys.Up)
            || InputHandler.IsGamePadButtonDown(Buttons.RightThumbstickUp, PlayerIndex.One))
            {
                motion.Y -= _speed;
            }
            
            if (InputHandler.IsKeyDown(Keys.Down)
            || InputHandler.IsGamePadButtonDown(Buttons.RightThumbstickDown, PlayerIndex.One))
            {
                motion.Y += _speed;
            }

            if (motion != Vector2.Zero)
            {
                motion.Normalize();

                _position += motion * _speed;

                ClampCamera();
            }
        }

        //TODO: base Sprite instead of AnimatedSprite?
        public void LockPositionToSprite(AnimatedSprite sprite)
        {
            _position.X = sprite.Position.X + sprite.Width / 2 - (_viewportRectangle.Width / 2);

            _position.Y = sprite.Position.Y + sprite.Height / 2 - (_viewportRectangle.Height / 2);

            ClampCamera();
        }

        #endregion

        #region Private Methods

        private void ClampCamera()
        {
            _position.X = MathHelper.Clamp(_position.X, 0, TileMap.WidthInPixels - _viewportRectangle.Width);

            _position.Y = MathHelper.Clamp(_position.Y, 0, TileMap.HeightInPixels - _viewportRectangle.Height);
        }

        #endregion
    }
}
