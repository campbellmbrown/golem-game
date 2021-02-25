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
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics { get; set; }
        private SpriteBatch _spriteBatch { get; set; }
        private Color _backgroundColor { get; set; }

        public static Dictionary<string, Texture2D> textures { get; set; }
        public static Dictionary<string, Animation> animations { get; set; }

        public static Camera2D camera { get; set; }
        public static Vector2 screenSize { get { return new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height); } }
        public Vector2 windowSize { get { return new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height); } }
        public static Vector2 zoomedScreenSize { get { return screenSize / camera.Zoom; } }
        public static Vector2 topLeft { get { return Vector2.Transform(Vector2.Zero, camera.GetInverseViewMatrix()); } }
        public static Vector2 mousePosition
        {
            get
            {
                Point _mousePos = Mouse.GetState().Position;
                return Vector2.Transform(new Vector2(_mousePos.X, _mousePos.Y), camera.GetInverseViewMatrix());
            }
        }
        public static Random random;

        private PlayerManager _playerManager { get; set; } // TODO - move to game manager?
        private CursorManager _cursorManager { get; set; } // TODO - move to game manager?

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = (int)screenSize.X;
            _graphics.PreferredBackBufferHeight = (int)screenSize.Y;
            _graphics.IsFullScreen = false;
            random = new Random();
        }

        protected override void Initialize()
        {
            IsMouseVisible = false;
            IsFixedTimeStep = true;
            _graphics.SynchronizeWithVerticalRetrace = true;
            _backgroundColor = new Color(48, 48, 61);
            // TODO - change this
            camera = new Camera2D(GraphicsDevice) { Zoom = 3, Position = (new Vector2(300, 210) - windowSize) / 2f };
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            textures = new Dictionary<string, Texture2D>()
            {
                { "cursor", Content.Load<Texture2D>("gui/cursor") },
            };
            animations = new Dictionary<string, Animation>()
            {
                { "player_idle_left", new Animation(Content.Load<Texture2D>("animations/player/player_idle_left"), 4, 0.2f ) },
            };

            _playerManager = new PlayerManager();
            _cursorManager = new CursorManager();
        }


        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            _playerManager.Update(gameTime);
            _cursorManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, transformMatrix: camera.GetViewMatrix()); GraphicsDevice.Clear(_backgroundColor);
            _playerManager.Draw(_spriteBatch);
            _cursorManager.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
