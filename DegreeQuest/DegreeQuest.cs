using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DegreeQuest;
using System;
using System.Threading;
using System.Text;

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
        string lastAct = "nil";
        DQPostClient pclient = null;
        DQPostSrv psrv = null;

        /* Through trial and error, puts you at the "root" project directory (with the .sln, etc.) */
        public string root = System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..";

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        PC pc;

        public Room room;

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
            pc = new PC();
            room = new Room();
            room.members.Add(pc);

            // server init logic ;; always serving atm
            string config = System.IO.File.ReadAllText(root + @"/config.txt");
            if (config.Contains("server=true"))
                serverMode = true;

            if (serverMode)
            {
                srv = new DQServer(pc);

                Thread srvThread = new Thread(new ThreadStart(srv.ThreadRun));
                srvThread.Start();
                //srvThread.Join();
                Console.WriteLine("> Server Initialistion Complete!");

                //post
                psrv = new DQPostSrv(this);

                Thread psrvThread = new Thread(new ThreadStart(psrv.ThreadRun));
                psrvThread.Start();
                Console.WriteLine("> POST Server Initialisation Complete!");

            }

            Console.Write("File had: " + config);
            if (config.Contains("client=true"))
                clientMode = true;

            // client init logic
            if (clientMode)
            {
                /* temporary while overhauling :13337
                client = new DQClient(pc);

                Thread clientThread = new Thread(new ThreadStart(client.ThreadRun));
                clientThread.Start();
                Console.WriteLine("> Client Initialisation Complete!");
                */

                //post
                pclient = new DQPostClient(pc, lastAct);

                Thread pclientThread = new Thread(new ThreadStart(pclient.ThreadRun));
                pclientThread.Start();
                Console.WriteLine("> POST Client Initialisation Complete!");
            }

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

            //rather than disabling, should do a clientmode check here to queue keypresses/keyboard state as a request to the server

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            UpdatePlayer(gameTime);

            base.Update(gameTime);
        }


        private void UpdatePlayer(GameTime gameTime)
        {
            //lastAct = "nil";

            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                pc.Position.X -= playerMoveSpeed;
                lastAct = "Move";
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                pc.Position.X += playerMoveSpeed;
                lastAct = "Move";
            }

            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                pc.Position.Y -= playerMoveSpeed;
                lastAct = "Move";
            }

            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                pc.Position.Y += playerMoveSpeed;
                lastAct = "MOVE";
            }

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
            //pc.Draw(spriteBatch);
            int i;
            for (i = 0; i < room.members.ToArray().Length; i++)
            {
                ((PC) room.members.ToArray()[i]).Draw(spriteBatch);
            }

            base.Draw(gameTime);

            //stop draw
            spriteBatch.End();
        }

        public static byte[] stb(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static string bts(byte[] b)
        {
            return Encoding.ASCII.GetString(b);
        }

        /* loads another PC in */
        public void LoadPC(PC c)
        {
            // TODO: use this.Content to load your game content here

            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,
                GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            c.Initialize(Content.Load<Texture2D>(root + "\\Content\\Graphics\\player"), playerPosition);
        }
    }

    class Location
    {
        public float X;
        public float Y;

        public Location(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Location(Vector2 v)
        {
            X = v.X;
            Y = v.Y;
        }

        public Location(string s)
        {
            string[] str = s.Split(';');
            if(str.GetLength(0) != 2)
            {
                Console.WriteLine("Error converting string to Location!");
                X = 1;
                Y = 1;
            }
            else
            {
                X = float.Parse(str[0]);
                Y = float.Parse(str[1]);
            }
        }

        public override string ToString()
        {
            string str = "";
            str += X.ToString();
            str += ";";
            str += Y.ToString();

            return str;
        }

        public Vector2 toVector2()
        {
            Vector2 v = new Vector2(X, Y);
            return v;
        }

    }
}
