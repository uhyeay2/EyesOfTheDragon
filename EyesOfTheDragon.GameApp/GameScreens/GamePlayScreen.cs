using EyesOfTheDragon.GameApp.Constants.Tilesets;
using EyesOfTheDragon.MGRpgLibrary.Components;
using EyesOfTheDragon.MGRpgLibrary.Enums;
using EyesOfTheDragon.MGRpgLibrary.GameComponents;
using EyesOfTheDragon.MGRpgLibrary.Sprites;
using EyesOfTheDragon.MGRpgLibrary.TileEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace EyesOfTheDragon.GameApp.GameScreens
{
    public class GamePlayScreen : BaseGameState
    {
        #region Private Fields

        private Engine engine = new Engine(32, 32);
        private TileMap _map;
        private Player _player;
        private AnimatedSprite _sprite;

        #endregion

        #region Constructor

        public GamePlayScreen(Game game, GameStateManager stateManager) : base(game, stateManager)
        {
            _player = new Player(_game);
        }

        #endregion

        #region Method Overrides

        protected override void LoadContent()
        {
            Texture2D spriteSheet = Game.Content.Load<Texture2D>(@"PlayerSprites\malefighter");
            Dictionary<AnimationKey, Animation> animations = [];

            Animation animation = new Animation(3, 32, 32, 0, 0);
            
            animations.Add(AnimationKey.DirectionDown, animation);
            animation = new Animation(3, 32, 32, 0, 32);
            
            animations.Add(AnimationKey.DirectionLeft, animation);
            animation = new Animation(3, 32, 32, 0, 64);
            
            animations.Add(AnimationKey.DirectionRight, animation);
            animation = new Animation(3, 32, 32, 0, 96);
            
            animations.Add(AnimationKey.DirectionUp, animation);
            _sprite = new AnimatedSprite(spriteSheet, animations);

            base.LoadContent();

            var tileset1 = Exterior1.LoadTileset(_game.Content);
            var tileset2 = Exterior2.LoadTileset(_game.Content);

            var tilesets = new List<Tileset>() { tileset1, tileset2 };

            var groundLayer = new MapLayer(40, 40);

            for (int y = 0; y < groundLayer.Height; y++)
            {
                for (int x = 0; x < groundLayer.Width; x++)
                {
                    Tile tile = new Tile(0, 0);
                    groundLayer.SetTile(x, y, tile);
                }
            }

            MapLayer splatter = new MapLayer(40, 40);

            Random random = new Random();
            
            for (int i = 0; i < 80; i++)
            {
                int x = random.Next(0, 40);
                int y = random.Next(0, 40);
                int index = random.Next(2, 14);
                Tile tile = new Tile(index, 0);
                splatter.SetTile(x, y, tile);
            }

            splatter.SetTile(1, 0, new Tile(0, 1));
            splatter.SetTile(2, 0, new Tile(2, 1));
            splatter.SetTile(3, 0, new Tile(0, 1));

            _map = new TileMap(tilesets, [groundLayer, splatter]);
        }

        public override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);
            _sprite.Update(gameTime);

            var motion = new Vector2();

            // Up/Down Input
            if (InputHandler.IsKeyDown(Keys.W) 
            || InputHandler.IsGamePadButtonDown(Buttons.LeftThumbstickUp, PlayerIndex.One))
            {
                _sprite.CurrentAnimation = AnimationKey.DirectionUp;
                motion.Y = -1;
            }
            else if (InputHandler.IsKeyDown(Keys.S) 
            || InputHandler.IsGamePadButtonDown(Buttons.LeftThumbstickDown, PlayerIndex.One))
            {
                _sprite.CurrentAnimation = AnimationKey.DirectionDown;
                motion.Y = 1;
            }

            // Left/Right Input
            if (InputHandler.IsKeyDown(Keys.A) 
            || InputHandler.IsGamePadButtonDown(Buttons.LeftThumbstickLeft, PlayerIndex.One))
            {
                _sprite.CurrentAnimation = AnimationKey.DirectionLeft;
                motion.X = -1;
            }
            else if (InputHandler.IsKeyDown(Keys.D) 
            || InputHandler.IsGamePadButtonDown(Buttons.LeftThumbstickRight, PlayerIndex.One))
            {
                _sprite.CurrentAnimation = AnimationKey.DirectionRight;
                motion.X = 1;
            }

            // Apply Movement
            if (motion != Vector2.Zero)
            {
                _sprite.IsAnimating = true;
                motion.Normalize();
                _sprite.Position += motion * _sprite.Speed;
                _sprite.LockToMap();

                if (_player.Camera.CameraMode == CameraMode.Follow)
                {
                    _player.Camera.LockPositionToSprite(_sprite);
                }
            }
            else
            {
                // Turn off Animation when not moving
                _sprite.IsAnimating = false;
            }

            // Toggle CameraMode
            if (InputHandler.IsKeyReleased(Keys.F) 
            || InputHandler.IsGamePadButtonReleased(Buttons.RightStick, PlayerIndex.One))
            {
                if (_player.Camera.CameraMode == CameraMode.Follow)
                {
                    _player.Camera.CameraMode = CameraMode.Free;
                }
                else
                {
                    _player.Camera.CameraMode = CameraMode.Follow;
                    
                    _player.Camera.LockPositionToSprite(_sprite);
                }
            }

            // Return Camera To Player
            if (_player.Camera.CameraMode != CameraMode.Follow)
            {
                if (InputHandler.IsKeyReleased(Keys.C) ||
                InputHandler.IsGamePadButtonReleased(Buttons.LeftStick, PlayerIndex.One))
                {
                    _player.Camera.LockPositionToSprite(_sprite);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _game.SpriteBatch.Begin(
                SpriteSortMode.Immediate,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                Matrix.Identity
            );

            _map.Draw(_game.SpriteBatch, _player.Camera);
            _sprite.Draw(gameTime, _game.SpriteBatch, _player.Camera);

            base.Draw(gameTime);

            _game.SpriteBatch.End();
        }

        #endregion
    }
}
