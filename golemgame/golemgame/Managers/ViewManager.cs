using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golemgame.Managers
{
    public class ViewManager
    {
        public Camera2D camera { get; set; }
        public Vector2 zoomedScreenSize { get { return screenSize / camera.Zoom; } }
        public Vector2 topLeft { get { return Vector2.Transform(Vector2.Zero, camera.GetInverseViewMatrix()); } }
        public Vector2 mousePosition
        {
            get
            {
                Point _mousePos = Mouse.GetState().Position;
                return Vector2.Transform(new Vector2(_mousePos.X, _mousePos.Y), camera.GetInverseViewMatrix());
            }
        }
        public static Vector2 screenSize
        {
            get
            {
                return new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            }
        }
        public Vector2 windowSize
        {
            get
            {
                return new Vector2(_graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height);
            }
        }
        private GraphicsDevice _graphicsDevice;

        public ViewManager(Game game, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager)
        {
            _graphicsDevice = graphicsDevice;
            camera = new Camera2D(_graphicsDevice)
            {
                Zoom = 1,
                Position = Vector2.Zero
            };
            graphicsDeviceManager.PreferredBackBufferWidth = (int)screenSize.X;
            graphicsDeviceManager.PreferredBackBufferHeight = (int)screenSize.Y;
            graphicsDeviceManager.IsFullScreen = false;
            graphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
            graphicsDeviceManager.ApplyChanges();
        }
    }
}
