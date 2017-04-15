using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DegreeQuest;
using System;
using System.Threading;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace DegreeQuest
{
    /// <summary>
    /// 2D rogue-like game for CS 309 with Mitra
    /// By Sean Hinchee, Zach Boe, Zach Turley, and Dennis Xu
    /// Team 102
    /// </summary>

    public class DegreeQuest : Game
    {
        DQServer srv = null;
        DQClient client = null;
        bool clientMode = false;
        bool serverMode = false;
        bool debugMode = false;
        string debugString = "nil";
        public Queue actions = new Queue();
        DQPostClient pclient = null;
        DQPostSrv psrv = null;
        public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

        public static string root = System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..";
        public static string state = "start";

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        PC pc;

        public volatile Dungeon dungeon;



        //states to determine keypresses
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        //states for mouse
        MouseState currentMouseState;
        MouseState previousMouseState;

        int lastNum = -1;

        SpriteFont sf;

        /** End Variables **/

        public DegreeQuest()
        {
            graphics = new GraphicsDeviceManager(this);

            /* window resize code */
            IsMouseVisible = true;
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
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

            dungeon = new Dungeon(pc);
            dungeon.AddRoom("secondary");

            // initialise texture index
            sf = Content.Load<SpriteFont>("mono");

            string[] files = System.IO.Directory.GetFiles(root + "\\Content\\Bin\\DesktopGL\\Images");

            Console.WriteLine("Loading Textures...");
            foreach (string fname in files)
            {
                //Console.WriteLine(fname);
                var s = fname.Split('\\');
                var n = s[s.Length - 1].Split('.');
                var t = Content.Load<Texture2D>(root + "\\Content\\Bin\\DesktopGL\\Images\\" + n[0]);
                Textures.Add(n[0], t);
            }
            Console.WriteLine("Done loading Textures...");


            // server init logic ;; always serving atm
            Config conf = new Config();

            serverMode = conf.bget("server");
            clientMode = !serverMode;

            if (serverMode)
            {
                srv = new DQServer(this, conf);

                Thread srvThread = new Thread(new ThreadStart(srv.ThreadRun));
                srvThread.IsBackground = true;
                srvThread.Start();
                //srvThread.Join();
                Console.WriteLine("> Server Initialistion Complete!");

                //post
                psrv = new DQPostSrv(this, conf);

                Thread psrvThread = new Thread(new ThreadStart(psrv.ThreadRun));
                psrvThread.IsBackground = true;
                psrvThread.Start();
                Console.WriteLine("> POST Server Initialisation Complete!");

            }

            // client init logic
            if (clientMode)
            {
                client = new DQClient(this, conf);

                Thread clientThread = new Thread(new ThreadStart(client.ThreadRun));
                clientThread.Start();
                Console.WriteLine("> Client Initialisation Complete!");

                //post
                pclient = new DQPostClient(pc, this, conf);
                Thread pclientThread = new Thread(new ThreadStart(pclient.ThreadRun));
                pclientThread.Start();
                Console.WriteLine("> POST Client Initialisation Complete!");
            }

            pc.MoveSpeed = 8.0f;

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

            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + GraphicsDevice.Viewport.TitleSafeArea.Width / 2,
                GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            pc.Initialize("player", playerPosition);
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
                Halt();


            // TODO: Add your update logic here

            if (state == "start")
            {
                Rectangle hostClick = new Rectangle(629, 400, 343, 67);
                Rectangle joinClick = new Rectangle(629, 500, 343, 67);
                previousMouseState = currentMouseState;
                currentMouseState = Mouse.GetState();
                if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                {
                    Point mouseloc = new Point(currentMouseState.X, currentMouseState.Y);
                    if (hostClick.Contains(mouseloc))
                    {
                        state = "game";
                    }
                    else if (joinClick.Contains(mouseloc))
                    {
                        state = "game";
                    }

                }
            }
            if (state == "login")
            {
                //TODO
            }
            if (state == "signup")
            {
                //TODO
            }
            if (state == "game")
            {
                previousKeyboardState = currentKeyboardState;
                currentKeyboardState = Keyboard.GetState();
                UpdatePlayer(gameTime);
            }
            if (state == "inventory")
            {
                //TODO
            }

            base.Update(gameTime);
        }


        private void UpdatePlayer(GameTime gameTime)
        {
            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                pc.Position.X -= pc.MoveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                pc.Position.X += pc.MoveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                pc.Position.Y -= pc.MoveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                pc.Position.Y += pc.MoveSpeed;
            }

            // toggle player and npc sprites (for testing)
            if (currentKeyboardState.IsKeyDown(Keys.F5) && !previousKeyboardState.IsKeyDown(Keys.F5))
            {
                if (pc.Texture == "player")
                    pc.Texture = "npc";
                else
                    pc.Texture = "player";
            }

            if (currentKeyboardState.IsKeyDown(Keys.F2) && !previousKeyboardState.IsKeyDown(Keys.F2))
            {
                if (debugMode)
                    debugMode = false;
                else
                    debugMode = true;
            }
            //for changing rooms
            if (currentKeyboardState.IsKeyDown(Keys.F12) && !previousKeyboardState.IsKeyDown(Keys.F12))
            {
                //for testing purposes
                if (dungeon.currentRoom.id == "default")
                {
                    dungeon.switchRooms("secondary");
                }
                else
                {
                    dungeon.switchRooms("default");
                }
            }
            if (currentKeyboardState.IsKeyDown(Keys.F3) && !previousKeyboardState.IsKeyDown(Keys.F3))
            {
                Item item = new Item();
                item.Initialize(item.Texture, pc.Position.toVector2());
                dungeon.currentRoom.Add(item);
            }

            if (currentKeyboardState.IsKeyDown(Keys.F4) && !previousKeyboardState.IsKeyDown(Keys.F4))
            {
                NPC npc = new NPC();
                npc.Initialize(npc.Texture, pc.Position.toVector2());
                dungeon.currentRoom.Add(npc);
            }


            pc.kbState = currentKeyboardState.GetPressedKeys();

            pc.Position.X = MathHelper.Clamp(pc.Position.X, 160, 1440 - LoadTexture(pc).Width);
            pc.Position.Y = MathHelper.Clamp(pc.Position.Y, 90, 810 - LoadTexture(pc).Height);

            /* system checks */
            if (serverMode)
            {
                if (psrv._halt || srv._halt)
                {
                    Halt();
                }
            }
            else if (clientMode)
            {
                if (pclient._halt || client._halt)
                {
                    Halt();
                }
            }
            
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // start drawing
            spriteBatch.Begin();

            if (state == "start")
            {
                Texture2D backgound = Textures["ISU-campus"];
                spriteBatch.Draw(backgound, new Vector2(0, 0), Color.White);
                Texture2D hostb = Textures["HostButton"];
                Texture2D joinb = Textures["joinButton"];
                spriteBatch.Draw(hostb, new Vector2(629, 400), Color.White);
                spriteBatch.Draw(joinb, new Vector2(629, 500), Color.White);
            }
            if (state == "login")
            {
                //TODO
            }
            if (state == "signup")
            {
                //TODO
            }
            if (state == "inventory")
            {
                //TODO
            }
            if (state == "game")
            {
                Texture2D rect = new Texture2D(graphics.GraphicsDevice, 1280, 720);
                Color[] data = new Color[1280 * 720];
                for (int j = 0; j < data.Length; j++) data[j] = Color.Green;
                rect.SetData(data);
                spriteBatch.Draw(rect, new Vector2(160, 90), Color.White);

            lock (dungeon.currentRoom)
            {
                //draw player
                //pc.Draw(spriteBatch);
                int i;
                for (i = 0; i < dungeon.currentRoom.num_item && i < dungeon.currentRoom.items.Length; i++) { DrawSprite(dungeon.currentRoom.items[i], spriteBatch); }
                for (i = 0; i < dungeon.currentRoom.num && i < dungeon.currentRoom.members.Length; i++){ DrawSprite(dungeon.currentRoom.members[i], spriteBatch); }
            }

            /* debug mode draw */
            if(debugMode)
            {
                string str = "";
                if (clientMode)
                    str += "\nMode: Client";
                if (serverMode)
                    str += "\nMode: Server";
                
                debugString += str + "\nRoom Id: " + dungeon.currentRoom.id;
                
                foreach(var rom in dungeon.Rooms)
                {
                    debugString += "\n" + rom.Key + " item #: " + rom.Value.num_item + "\nactors: " + rom.Value.num;
                }

                    spriteBatch.DrawString(sf, debugString, new Vector2(0, 2), Color.Black);

                    debugString = "nil";
                }

            }
            base.Draw(gameTime);

            //stop draw
            spriteBatch.End();
        }


        /* Performs the shutdown routines */
        public void Halt()
        {

            if (serverMode)
            {
                psrv.Halt();
                srv.Halt();
            }
            if (clientMode)
            {
                client.Halt();
                pclient.Halt();
            }


            Exit();
        }


        /* loads another PC in */
        public void LoadPC(PC c, string texture)
        {
            // TODO: use this.Content to load your game content here

            Vector2 playerPosition;

            try
            {
                playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + GraphicsDevice.Viewport.TitleSafeArea.Width / 2,
                GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Faulty LoadPC, null exception, not loading requested PC...");
                return;
            }



            c.Initialize(texture, playerPosition);
        }


        /* fetches the relevant Texture2D for a Texture string */
        public Texture2D LoadTexture(Actor a)
        {
            //this works, probably
            //return Content.Load<Texture2D>(root + "\\Content\\Graphics\\" + a.Texture);
            return Textures[a.Texture];
        }

        /* acquires width of a sprite */
        public int Width(Actor a)
        {
            return LoadTexture(a).Width;
        }

        /* acquires width of a sprite */
        public int Height(Actor a)
        {
            return LoadTexture(a).Height;
        }

        /* Draw method for a Sprite */
        public void DrawSprite(Actor a, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(LoadTexture(a), a.Position.toVector2(), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

    }

    [Serializable()]
    public class Location
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
            if (str.GetLength(0) != 2)
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
