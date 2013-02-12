using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Surface;
using Microsoft.Surface.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TetriClimber
{
    /// <summary>
    /// This is the main type for your application.
    /// </summary>
    public class App : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager graphics;

        private static TouchTarget touchTarget;
        //private Color backgroundColor = new Color(81, 81, 81);
        //private Color backgroundColor = Color.Gray;
        private bool applicationLoadCompleteSignalled;

        private UserOrientation currentOrientation = UserOrientation.Bottom;

        public static Matrix screenTransform = Matrix.Identity;

        private static AUserInput ti;

        /// <summary>
        /// For singleton purpose. 
        /// </summary>
        private static SpriteBatch spriteBatch;
        private static ContentManager content;
        private static Game game;
        //

        /// <summary>
        /// The target receiving all surface input for the application.
        /// </summary>
        public static TouchTarget TouchTarget
        {
            get { return touchTarget; }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public App()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.SupportedOrientations = DisplayOrientation.Portrait;
           
            // FOR SINGLETON PURPOSE
            content = Content;
            game = this;
            if (SettingsManager.Instance.Device == SettingsManager.EDevice.SURFACE)
            {
                ti = new TouchInput();
                Constants.Measures.portraitHeight = 1080;
                Constants.Measures.portraitWidth = 1920;
                //Constants.Measures.leftBoardMargin = (float)Math.Round((Constants.Measures.portraitHeight - Constants.Measures.boardBlockWidth * Constants.Measures.blockSize) / 2f);
                //Constants.Measures.upBoardMargin = (float)Math.Round((Constants.Measures.portraitWidth - Constants.Measures.boardBlockHeight * Constants.Measures.blockSize) / 2f);
            }
            else
            {
                ti = new KeyboardInput();
                Constants.Measures.portraitHeight = 980;
                Constants.Measures.portraitWidth = 500;
                //Constants.Measures.leftBoardMargin = (float)Math.Round((Constants.Measures.portraitWidth - Constants.Measures.boardBlockWidth * Constants.Measures.blockSize) / 2f);
                //Constants.Measures.upBoardMargin = (float)Math.Round((Constants.Measures.portraitHeight - Constants.Measures.boardBlockHeight * Constants.Measures.blockSize) / 2f);
            }

            //graphics.ToggleFullScreen();
            //
        }

        #region Initialization

        /// <summary>
        /// Moves and sizes the window to cover the input surface.
        /// </summary>
        private void SetWindowOnSurface()
        {
            System.Diagnostics.Debug.Assert(Window != null && Window.Handle != IntPtr.Zero,
                "Window initialization must be complete before SetWindowOnSurface is called");
            if (Window == null || Window.Handle == IntPtr.Zero)
                return;

            // Get the window sized right.
            Program.InitializeWindow(Window);
            // Set the graphics device buffers.
            graphics.PreferredBackBufferWidth = Program.WindowSize.Width;
            //graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = Program.WindowSize.Height;
            //graphics.PreferredBackBufferHeight = 450;
            graphics.ApplyChanges();
            // Make sure the window is in the right location.
            Program.PositionWindow();
        }

        /// <summary>
        /// Initializes the surface input system. This should be called after any window
        /// initialization is done, and should only be called once.
        /// </summary>
        private void InitializeSurfaceInput()
        {
            System.Diagnostics.Debug.Assert(Window != null && Window.Handle != IntPtr.Zero,
                "Window initialization must be complete before InitializeSurfaceInput is called");
            if (Window == null || Window.Handle == IntPtr.Zero)
                return;
            System.Diagnostics.Debug.Assert(touchTarget == null,
                "Surface input already initialized");
            if (touchTarget != null)
                return;

            // Create a target for surface input.
            if (ti is TouchInput)
            {
                touchTarget = new TouchTarget(Window.Handle, EventThreadChoice.OnBackgroundThread);
                touchTarget.EnableInput();
                touchTarget.TouchMove += new EventHandler<TouchEventArgs>((ti as TouchInput).Move);
                touchTarget.TouchUp += new EventHandler<TouchEventArgs>((ti as TouchInput).Up);
                touchTarget.TouchTapGesture += new EventHandler<TouchEventArgs>((ti as TouchInput).Tap);
            }
        }

        #endregion

        #region Overridden Game Methods

        /// <summary>
        /// Allows the app to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            IsMouseVisible = true; // easier for debugging not to "lose" mouse
            SetWindowOnSurface();
            InitializeSurfaceInput();

            // Set the application's orientation based on the orientation at launch
            //currentOrientation = ApplicationServices.InitialOrientation;

            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;

            // Setup the UI to transform if the UI is rotated.
            // Create a rotation matrix to orient the screen so it is viewed correctly
            // when the user orientation is 180 degress different.
            //Matrix inverted = Matrix.CreateRotationZ(MathHelper.ToRadians(90)) *
            //           Matrix.CreateTranslation(graphics.GraphicsDevice.Viewport.Width,
            //                                     0,
            //                                     0);

            //if (currentOrientation == UserOrientation.Top)
            //{
              //  Console.Out.WriteLine("Vertical Orientataion");
            //screenTransform = inverted;
            //}
            MenuManager.Instance.Initialize();
           
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per app and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your application content here
        }

        /// <summary>
        /// UnloadContent will be called once per app and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the app to run logic such as updating the world,
        /// checking for collisions, gathering input and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (ApplicationServices.WindowAvailability != WindowAvailability.Unavailable)
            {
                if (ApplicationServices.WindowAvailability == WindowAvailability.Interactive)
                {
                    ti.Update(gameTime);
                    // TODO: Process touches, 
                    // use the following code to get the state of all current touch points.
                    // ReadOnlyTouchPointCollection touches = touchTarget.GetState();
                }

                SceneManager.Instance.Update(gameTime);
                MenuManager.Instance.Update(gameTime);
                // TODO: Add your update logic here
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the app should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (!applicationLoadCompleteSignalled)
            {
                // Dismiss the loading screen now that we are starting to draw
                ApplicationServices.SignalApplicationLoadComplete();
                applicationLoadCompleteSignalled = true;
            }

            //TODO: Rotate the UI based on the value of screenTransform here if desired

            GraphicsDevice.Clear(Constants.Color.background);


            //SpriteManager.Instance.begin();
            //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, screenTransform);
            SceneManager.Instance.Draw(gameTime);
            MenuManager.Instance.Draw(gameTime);
            //SpriteManager.Instance.drawAtPos(SpriteManager.ESprite.L, Vector2.Zero);
            SpriteManager.Instance.end();

            //TODO: Add your drawing code here
            //TODO: Avoid any expensive logic if application is neither active nor previewed

            base.Draw(gameTime);
        }

        #endregion

        #region Application Event Handlers

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: Enable audio, animations here

            //TODO: Optionally enable raw image here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: Optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: Disable audio, animations here

            //TODO: Disable raw image if it's enabled
        }

        #endregion

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Release managed resources.
                IDisposable graphicsDispose = graphics as IDisposable;
                if (graphicsDispose != null)
                {
                    graphicsDispose.Dispose();
                }
                if (touchTarget != null)
                {
                    touchTarget.Dispose();
                    touchTarget = null;
                }
            }

            // Release unmanaged Resources.

            // Set large objects to null to facilitate garbage collection.

            base.Dispose(disposing);
        }

        #endregion

        // FOR SINGLETON PURPOSE
        public static ContentManager ContentManager
        {
            get { return content; }
        }
        public static AUserInput UserInput
        {
            get { return ti; }
        }

        public static SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public static Game Game
        {
            get { return game; }
        }
        public GraphicsDevice GraphicDevice
        {
            get { return graphics.GraphicsDevice; }
        }
    }
}
