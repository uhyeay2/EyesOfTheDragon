using EyesOfTheDragon.MGRpgLibrary.DrawableGameComponents;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace EyesOfTheDragon.MGRpgLibrary.GameComponents
{
    public class GameStateManager : GameComponent
    {
        #region Private Fields

        private Stack<GameState> _gameStates = new Stack<GameState>();

        private const int _startDrawOrder = 5000;
        private const int _drawOrderInc = 100;
        private int _drawOrder;

        #endregion

        #region Constructor

        public GameStateManager(Game game) : base(game)
        {
            _drawOrder = _startDrawOrder;
        }

        #endregion

        #region Public Accessors

        public event EventHandler OnStateChange;

        public GameState CurrentState => _gameStates.Peek();

        #endregion

        #region GameStateManager Methods

        public void PopState()
        {
            if (_gameStates.Count > 0)
            {
                RemoveState();

                _drawOrder -= _drawOrderInc;

                OnStateChange?.Invoke(this, null);
            }
        }
        public void PushState(GameState state)
        {
            _drawOrder += _drawOrderInc;

            state.DrawOrder = _drawOrder;

            AddState(state);
            
            OnStateChange?.Invoke(this, null);
        }

        public void ChangeState(GameState state)
        {
            while (_gameStates.Count > 0)
            {
                RemoveState();
            }

            state.DrawOrder = _startDrawOrder;

            AddState(state);

            OnStateChange?.Invoke(this, null);
        }

        #endregion

        #region Private Methods

        public void RemoveState()
        {
            var state = _gameStates.Peek();

            OnStateChange -= state.StateChange;

            Game.Components.Remove(state);

            _gameStates.Pop();
        }

        public void AddState(GameState state)
        {
            _gameStates.Push(state);

            Game.Components.Add(state);

            OnStateChange += state.StateChange;
        }

        #endregion

    }
}
