using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DegreeQuest;
using System;
using System.Threading;

namespace DegreeQuest
{
    /// <summary>
    /// 2D rogue-like game for CS 309 with Mitra
    /// By Sean, Zach B., Zach T., and Dennis
    /// Team 102
    /// </summary>
    public class DegreeQuest : Game
    {
        DQServer srv = null;
        DQClient client = null;
        bool clientMode = false;
        bool serverMode = false;

        /* Through trial and error, puts you at the "root" project directory (with the .sln, etc.) */
        public string root = System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..";

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        PC pc;

        //states to determine keypresses
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        //movement speed of player
        float playerMoveSpeed;

        /** End Variables **/

        public DegreeQuest()
        {
            graphics = new GraphicsDeviceManager(this);

            /* window resize code */
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 1024;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // server init logic
            serverMode = true;

            if (serverMode)
            {
                srv = new DQServer();

                Thread srvThread = new Thread(new ThreadStart(srv.ThreadRun));
                srvThread.Start();
                //srvThread.Join();
                Console.WriteLine("> Server Initialistion Complete!");
            }

            // client init logic
            clientMode = true;

            if (clientMode)
            {
                client = new DQClient();

                Thread clientThread = new Thread(new ThreadStart(client.ThreadRun));
                clientThread.Start();
                Console.WriteLine("> Client Initialisation Complete!");
            }

            //initialize the player
            pc = new PC();

            playerMoveSpeed = 8.0f;      

            base.Initialize();
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,
                GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            pc.Initialize(Content.Load<Texture2D>(root + "\\Content\\Graphics\\player"), playerPosition);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            UpdatePlayer(gameTime);

            base.Update(gameTime);
        }


        private void UpdatePlayer(GameTime gameTime)
        {
            if (currentKeyboardState.IsKeyDown(Keys.Left))
                pc.Position.X -= playerMoveSpeed;

            if (currentKeyboardState.IsKeyDown(Keys.Right))
                pc.Position.X += playerMoveSpeed;

            if (currentKeyboardState.IsKeyDown(Keys.Up))
                pc.Position.Y -= playerMoveSpeed;

            if (currentKeyboardState.IsKeyDown(Keys.Down))
                pc.Position.Y += playerMoveSpeed;

            pc.Position.X = MathHelper.Clamp(pc.Position.X, 0, GraphicsDevice.Viewport.Width - pc.Width);
            pc.Position.Y = MathHelper.Clamp(pc.Position.Y, 0, GraphicsDevice.Viewport.Height - pc.Height);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            // start drawing
            spriteBatch.Begin();

            //draw player
            pc.Draw(spriteBatch);

            base.Draw(gameTime);

            //stop draw
            spriteBatch.End();
        }

        public Vector2 GetPCPos()
        {
            return pc.Position;
        }
    }
}
