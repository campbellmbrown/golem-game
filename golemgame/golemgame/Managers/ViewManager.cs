using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golemgame.Managers
{
    public class ViewManager
    {
        public OrthographicCamera camera { get; set; }

        public Vector2 topLeft { get { return camera.ScreenToWorld(Vector2.Zero); } }

        public Vector2 mousePosition
        {
            get
            {
                Point _mousePos = Mouse.GetState().Position;
                return camera.ScreenToWorld(_mousePos.X, _mousePos.Y);
            }
        }
        public static Vector2 screenSize
        {
            get
            {
                return new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            }
        }
        private GraphicsDevice _graphicsDevice;
        public Vector2 windowSize
        {
            get
            {
                return new Vector2(_graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height);
            }
        }

        public ViewManager(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager, GameWindow gameWindow)
        {
            _graphicsDevice = graphicsDevice;
            graphicsDeviceManager.PreferredBackBufferWidth = (int)screenSize.X;
            graphicsDeviceManager.PreferredBackBufferHeight = (int)screenSize.Y;
            BoxingViewportAdapter viewportAdapter = new BoxingViewportAdapter(gameWindow, graphicsDevice, (int)screenSize.X, (int)screenSize.Y);
            camera = new OrthographicCamera(viewportAdapter);
            camera.ZoomIn(2);

            graphicsDeviceManager.IsFullScreen = true;
            graphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
            graphicsDeviceManager.ApplyChanges();
        }

        public void UpdateCameraPosition(Vector2 desiredCenterOfScreen)
        {
            camera.LookAt(desiredCenterOfScreen);
        }
    }
}
