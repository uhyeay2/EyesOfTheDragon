using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EyesOfTheDragon.MGRpgLibrary.Controls
{
    public class PictureBox : Control
    {
        #region Private Members

        private Texture2D _image;

        private Rectangle _sourceRect;

        private Rectangle _destinationRect;

        #endregion

        #region Constructors

        public PictureBox(Texture2D image, Rectangle destination)
        {
            Image = image;
            DestinationRectangle = destination;
            SourceRectangle = new Rectangle(0, 0, image.Width, image.Height);
            Color = Color.White;
        }
        public PictureBox(Texture2D image, Rectangle destination, Rectangle source)
        {
            Image = image;
            DestinationRectangle = destination;
            SourceRectangle = source;

            Color = Color.White;
        }

        #endregion

        #region Public Accessors

        public Texture2D Image
        {
            get { return _image; }
            set { _image = value; }
        }
        public Rectangle SourceRectangle
        {
            get { return _sourceRect; }
            set { _sourceRect = value; }
        }
        public Rectangle DestinationRectangle
        {
            get { return _destinationRect; }
            set { _destinationRect = value; }
        }

        #endregion

        #region Method Overrides

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_image, _destinationRect, _sourceRect, Color);
        }

        #endregion

        #region Public Methods

        public void SetPosition(Vector2 newPosition) =>
            _destinationRect = new Rectangle(
                (int)newPosition.X,
                (int)newPosition.Y,
                _sourceRect.Width,
                _sourceRect.Height
            );

        #endregion
    }
}
