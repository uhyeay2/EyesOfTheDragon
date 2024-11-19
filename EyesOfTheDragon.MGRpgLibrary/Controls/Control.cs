using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace EyesOfTheDragon.MGRpgLibrary.Controls
{
    public abstract class Control
    {
        #region Protected Members

        protected string _name;
        protected string _text;
        protected Vector2 _size;
        protected Vector2 _position;
        protected object _value;
        protected bool _hasFocus;
        protected bool _enabled;
        protected bool _visible;
        protected bool _tabStop;
        protected SpriteFont _spriteFont;
        protected Color _color;
        protected string _type;

        #endregion

        #region Constructor

        protected Control()
        {
            Color = Color.White;
            Enabled = true;
            Visible = true;
            SpriteFont = ControlManager.SpriteFont;
        }

        #endregion

        public event EventHandler Selected;

        #region Public Accessors

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        
        public Vector2 Size
        {
            get { return _size; }
            set { _size = value; }
        }
        
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                // Cast Y to an Int because MonoGame can have issues when drawing positions where Y isn't an integer value.
                _position.Y = (int)_position.Y;
            }
        }
        
        public object Value
        {
            get { return _value; }
            set { this._value = value; }

        }

        public bool HasFocus
        {
            get { return _hasFocus; }
            set { _hasFocus = value; }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public bool TabStop
        {
            get { return _tabStop; }
            set { _tabStop = value; }
        }

        public SpriteFont SpriteFont
        {
            get { return _spriteFont; }
            set { _spriteFont = value; }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        #endregion

        #region Virtual Methods
        
        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(SpriteBatch spriteBatch) { }

        public virtual void HandleInput(PlayerIndex playerIndex) { }

        protected virtual void OnSelected(EventArgs e)
        {
            if (Selected != null)
            {
                Selected(this, e);
            }
        }
        
        #endregion
    }
}
