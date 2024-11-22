using EyesOfTheDragon.MGRpgLibrary.TileEngine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using EyesOfTheDragon.MGRpgLibrary.Enums;
using EyesOfTheDragon.MGRpgLibrary.Extensions;

namespace EyesOfTheDragon.MGRpgLibrary.Sprites
{
    public class AnimatedSprite
    {
        #region Private Fields

        private Dictionary<AnimationKey, Animation> _animations;
        private AnimationKey _currentAnimationKey;
        private bool _isAnimating;
        private Texture2D _texture;
        private Vector2 _velocity;
        private float _speed = 2.0f;

        #endregion

        #region Constructor

        public AnimatedSprite(Texture2D sprite, Dictionary<AnimationKey, Animation> animation)
        {
            _texture = sprite;
            _animations = [];

            foreach (AnimationKey key in animation.Keys)
            {
                _animations.Add(key, (Animation)animation[key].Clone());
            }
        }

        #endregion

        #region Public Accessors

        public Vector2 Position;
        
        public AnimationKey CurrentAnimation
        {
            get { return _currentAnimationKey; }
            set { _currentAnimationKey = value; }
        }

        public bool IsAnimating
        {
            get { return _isAnimating; }
            set { _isAnimating = value; }
        }
        
        public int Width => _animations[_currentAnimationKey].FrameWidth; 

        public int Height => _animations[_currentAnimationKey].FrameHeight; 

        public float Speed
        {
            get { return _speed; }
            set { _speed = MathHelper.Clamp(_speed, 1.0f, 16.0f); }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set
            {
                _velocity = value;

                _velocity.NormalizeIfNotVector2Zero();
            }
        }

        #endregion
        
        #region Public Methods
        
        public void Update(GameTime gameTime)
        {
            if (_isAnimating)
            {
                _animations[_currentAnimationKey].Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(
                _texture,
                Position - camera.Position,
                _animations[_currentAnimationKey].CurrentFrameRect,
                Color.White
            );
        }

        public void LockToMap()
        {
            Position.X = MathHelper.Clamp(Position.X, 0, TileMap.WidthInPixels - Width);
            Position.Y = MathHelper.Clamp(Position.Y, 0, TileMap.HeightInPixels - Height);
        }
        #endregion
    }
}
