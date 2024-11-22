using Microsoft.Xna.Framework;
using System;

namespace EyesOfTheDragon.MGRpgLibrary.Sprites
{
    public class Animation : ICloneable
    {
        #region Private Members        

        private int _frameWidth;
        private int _frameHeight;

        private Rectangle[] _frames;
        private int _indexOfCurrentFrame;

        private TimeSpan _timeSpanToKeepFrameSetFor;
        private TimeSpan _timeSpanCurrentFrameHasBeenSet;

        private double _startingFramesPerSecond;
        private double _currentFramesPerSecond;
        private double _maxFramesPerSecond;

        #endregion

        #region Constructors

        public Animation(int frameCount, int frameWidth, int frameHeight, int xOffset, int yOffset, double framesPerSecond = 4.5, double maxFramesPerSecond = 60)
        : this(frameWidth, frameHeight, framesPerSecond, maxFramesPerSecond)
        {
            _frames = new Rectangle[frameCount];

            for (int i = 0; i < frameCount; i++)
            {
                _frames[i] = new Rectangle(
                xOffset + (frameWidth * i),
                yOffset,
                frameWidth,
                frameHeight);
            }
        }

        /// <summary>
        /// Private constructor for instantiating new Animation during Clone()
        /// </summary>
        private Animation(Rectangle[] frames, int frameWidth, int frameHeight, double framesPerSecond, double maxFramesPerSecond) 
        : this(frameWidth, frameHeight, framesPerSecond, maxFramesPerSecond)
        {
            _frames = frames;
        }

        /// <summary>
        /// Private Constructor for setting fields that are used by the Public constructor and the other Private constructor that is used for Clone()
        /// </summary>
        private Animation(int frameWidth, int frameHeight, double framesPerSecond, double maxFramesPerSecond)
        {
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;

            _startingFramesPerSecond = framesPerSecond;
            _currentFramesPerSecond = framesPerSecond;
            _maxFramesPerSecond = maxFramesPerSecond;
            FramesPerSecond = framesPerSecond;

            _timeSpanCurrentFrameHasBeenSet = TimeSpan.Zero;
        }

        #endregion

        #region Public Accessors

        public double FramesPerSecond
        {
            get { return _currentFramesPerSecond; }
            set
            {
                if (value < 0 || _currentFramesPerSecond < 0.01)
                {
                    _currentFramesPerSecond = 0.01;
                }
                else if (value > _maxFramesPerSecond)
                {
                    _currentFramesPerSecond = _maxFramesPerSecond;
                }
                else
                {
                    _currentFramesPerSecond = value;
                }

                _timeSpanToKeepFrameSetFor = TimeSpan.FromSeconds(1 / _currentFramesPerSecond);
            }
        }

        public Rectangle CurrentFrameRect => _frames[_indexOfCurrentFrame]; 

        public int IndexOfCurrentFrame
        {
            get { return _indexOfCurrentFrame; }
            set { _indexOfCurrentFrame = MathHelper.Clamp(value, 0, _frames.Length - 1); }
        }

        public int FrameWidth => _frameWidth; 

        public int FrameHeight => _frameHeight;

        #endregion

        #region Public Methods

        public void Update(GameTime gameTime)
        {
            _timeSpanCurrentFrameHasBeenSet += gameTime.ElapsedGameTime;

            if (_timeSpanCurrentFrameHasBeenSet >= _timeSpanToKeepFrameSetFor)
            {
                _timeSpanCurrentFrameHasBeenSet = TimeSpan.Zero;

                /*
                    Calculate which frame to move to. 
                    If we have three frames (index 0, 1, and 2), then add 1 to that and you will have the values 1, 2, and 3. 
                    Get the Modulus/Remainder of dividing those by the number of frames, which would give the values 1, 2, and 0. 
                    So if you are on frame 0 you will move to frame 1, from frame 1 to frame 2, and from frame 2 back to frame 0.
                 */
                _indexOfCurrentFrame = (_indexOfCurrentFrame + 1) % _frames.Length;
            }
        }

        public void ResetAnimation()
        {
            _indexOfCurrentFrame = 0;

            _timeSpanCurrentFrameHasBeenSet = TimeSpan.Zero;
        }

        #endregion

        #region ICloneable Interface Method

        public object Clone() => new Animation(_frames, _frameWidth, _frameHeight, _startingFramesPerSecond, _maxFramesPerSecond);

        #endregion
    }
}
