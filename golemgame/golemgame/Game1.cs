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
        GraphicsDeviceManager graphics { get; set; }
        private SpriteBatch spriteBatch { get; set; }
        private Color backgroundColor { get; set; }

        public static Dictionary<string, Texture2D> textures { get; set; }

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
        public static Random r;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = (int)screenSize.X;
            graphics.PreferredBackBufferHeight = (int)screenSize.Y;
            graphics.IsFullScreen = false;
            r = new Random();
        }

        protected override void Initialize()
        {
            IsMouseVisible = false;
            IsFixedTimeStep = true;
            graphics.SynchronizeWithVerticalRetrace = true;
            backgroundColor = new Color(48, 48, 61);
            // TODO - change this
            camera = new Camera2D(GraphicsDevice) { Zoom = 3, Position = (new Vector2(300, 210) - windowSize) / 2f };
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textures = new Dictionary<string, Texture2D>()
            {
            };
        }


        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, transformMatrix: camera.GetViewMatrix()); GraphicsDevice.Clear(backgroundColor);
            // TODO: replace this with a cursor
            spriteBatch.DrawPoint(mousePosition, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
