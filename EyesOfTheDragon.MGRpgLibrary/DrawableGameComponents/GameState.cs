using EyesOfTheDragon.MGRpgLibrary.GameComponents;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace EyesOfTheDragon.MGRpgLibrary.DrawableGameComponents
{
    public abstract partial class GameState : DrawableGameComponent
    {
        #region Private Fields

        private List<GameComponent> _childComponents = [];

        private GameState _tag;

        #endregion

        #region Protected Members

        protected GameStateManager _stateManager;

        #endregion

        #region Constructor

        protected GameState(Game game, GameStateManager stateManager) : base(game)
        {
            _stateManager = stateManager;

            _tag = this;
        }

        #endregion

        #region Public Accessors For Private Fields

        public List<GameComponent> ChildComponents => _childComponents;

        public GameState Tag => _tag;

        #endregion

        #region MonoGame Drawable Game Component Methods

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _childComponents)
            {
                if (component is DrawableGameComponent c && c.Enabled)
                {
                    c.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var component in _childComponents)
            {
                if (component is DrawableGameComponent c && c.Visible)
                {
                    c.Draw(gameTime);
                }
            }

            base.Update(gameTime);
        }

        #endregion

        #region GameState Methods

        internal protected virtual void StateChange(object sender, EventArgs e)
        {
            // If CurrentState == This GameState, then show/enable this and all child components.
            // Else we will hide/disable all child components
            Show(_stateManager.CurrentState == Tag);
        }

        /// <summary>
        /// Enable/Disable this component and all child components. 
        /// If a Child Component is a DrawableGameComponent then we will set Visible to true/false using the boolean passed in.
        /// </summary>
        /// <param name="enabled">Whether to set Visible/Enabled to true/false</param>
        protected virtual void Show(bool enabled)
        {
            Visible = enabled;
            Enabled = enabled;
            
            foreach (var component in _childComponents)
            {
                component.Enabled = enabled;

                if (component is DrawableGameComponent c)
                {
                    c.Visible = enabled;
                }
            }
        }

        #endregion

    }
}
