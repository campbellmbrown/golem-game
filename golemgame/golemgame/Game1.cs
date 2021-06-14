using golemgame.GameStates;
using golemgame.Managers;
using golemgame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace golemgame
{
    public enum GameState
    {
        Playing
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Color _backgroundColor;

        public static Dictionary<string, Texture2D> textures { get; set; }
        public static Dictionary<string, Animation> animations { get; set; }
        public static Random random;

        private ViewManager _viewManager;
        private GameState _gameState;
        private PlayingState _playingState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            random = new Random();
        }

        protected override void Initialize()
        {
            IsMouseVisible = false;
            IsFixedTimeStep = true;
            _backgroundColor = new Color(48, 48, 61);
            _gameState = GameState.Playing;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            textures = new Dictionary<string, Texture2D>()
            {
                { "cursor", Content.Load<Texture2D>("gui/cursor") },
                { "tilesheet", Content.Load<Texture2D>("textures/tilesheets/tilesheet") },
            };
            animations = new Dictionary<string, Animation>()
            {
                { "player_idle_left", new Animation(Content.Load<Texture2D>("animations/player/player_idle_left"), 4, 0.2f ) },
            };

            _viewManager = new ViewManager(GraphicsDevice, _graphics, Window);
            _playingState = new PlayingState(_viewManager);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: move to input manager in the playing state
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            switch (_gameState)
            {
                case GameState.Playing:
                    _playingState.Update(gameTime);
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, transformMatrix: _viewManager.camera.GetViewMatrix()); GraphicsDevice.Clear(_backgroundColor);

            switch (_gameState)
            {
                case (GameState.Playing):
                    _playingState.Draw(_spriteBatch);
                    break;
                default:
                    break;
            }

            base.Draw(gameTime);
            _spriteBatch.End();
        }
    }
}
