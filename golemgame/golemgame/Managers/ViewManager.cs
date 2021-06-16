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
        public Vector2 bottomLeft { get { return camera.ScreenToWorld(0, screenSize.Y); } }
        public static int scaleFactor = 2;
        public float desiredCameraZoom = 2;

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
            // Setting up the screen size
            _graphicsDevice = graphicsDevice;
            graphicsDeviceManager.PreferredBackBufferWidth = (int)screenSize.X;
            graphicsDeviceManager.PreferredBackBufferHeight = (int)screenSize.Y;
            graphicsDeviceManager.IsFullScreen = true;

            // Some other graphics device settings
            graphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
            graphicsDeviceManager.ApplyChanges();

            // Creating camera
            BoxingViewportAdapter viewportAdapter = new BoxingViewportAdapter(gameWindow, graphicsDevice,
                _graphicsDevice.PresentationParameters.BackBufferWidth / scaleFactor,
                _graphicsDevice.PresentationParameters.BackBufferHeight / scaleFactor);
            camera = new OrthographicCamera(viewportAdapter);
            camera.ZoomIn(desiredCameraZoom / scaleFactor);
        }

        public void UpdateCameraPosition(Vector2 desiredCenterOfScreen)
        {
            camera.LookAt(desiredCenterOfScreen);
        }
    }
}
