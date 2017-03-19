﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DegreeQuest;
using System;
using System.Threading;
using System.Text;
using System.Collections;

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
        //public string lastAct = "nil";
        public Queue actions = new Queue();
        DQPostClient pclient = null;
        DQPostSrv psrv = null;

        public static string root = System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..";

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        PC pc;

        public Room room;

        //states to determine keypresses
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        //movement speed of player
        //to Actor

        /** End Variables **/

        public DegreeQuest()
        {
            graphics = new GraphicsDeviceManager(this);

            /* window resize code */
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
            room = new Room();
            room.Add(pc);

            // server init logic ;; always serving atm
            Config conf = new Config();

            if (conf.bget("server"))
                serverMode = true;

            if (serverMode)
            {
                srv = new DQServer(this);

                Thread srvThread = new Thread(new ThreadStart(srv.ThreadRun));
               srvThread.IsBackground = true;
                srvThread.Start();
                //srvThread.Join();
                Console.WriteLine("> Server Initialistion Complete!");

                //post
                psrv = new DQPostSrv(this);

                Thread psrvThread = new Thread(new ThreadStart(psrv.ThreadRun));
               psrvThread.IsBackground = true;
                psrvThread.Start();
                Console.WriteLine("> POST Server Initialisation Complete!");

            }

            
            if (conf.bget("client"))
                clientMode = true;

            // client init logic
            if (clientMode)
            {
                client = new DQClient(this);

                Thread clientThread = new Thread(new ThreadStart(client.ThreadRun));
                clientThread.Start();
                Console.WriteLine("> Client Initialisation Complete!");

                //post
                pclient = new DQPostClient(pc, this);
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

            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,
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
            if (!clientMode)
            {
                if (currentKeyboardState.IsKeyDown(Keys.Left))
                {
                    pc.Position.X -= pc.MoveSpeed;
                    //actions.Enqueue("MOVE");
                }

                if (currentKeyboardState.IsKeyDown(Keys.Right))
                {
                    pc.Position.X += pc.MoveSpeed;
                    //actions.Enqueue("MOVE");
                }

                if (currentKeyboardState.IsKeyDown(Keys.Up))
                {
                    pc.Position.Y -= pc.MoveSpeed;
                    //actions.Enqueue("MOVE");
                }

                if (currentKeyboardState.IsKeyDown(Keys.Down))
                {
                    pc.Position.Y += pc.MoveSpeed;
                    //actions.Enqueue("MOVE");
                }

                // clamp might need to be done server-side for clients
                pc.Position.X = MathHelper.Clamp(pc.Position.X, 0, GraphicsDevice.Viewport.Width - LoadTexture(pc).Width);
                pc.Position.Y = MathHelper.Clamp(pc.Position.Y, 0, GraphicsDevice.Viewport.Height - LoadTexture(pc).Height);
            }
            else
            {
                //if clientmode == true
                string str = "";
                bool n = false, s = false, e = false, w = false;

                if (currentKeyboardState.IsKeyDown(Keys.Left))
                {
                    //pc.Position.X -= pc.MoveSpeed;
                    w = true;
                }

                if (currentKeyboardState.IsKeyDown(Keys.Right))
                {
                    //pc.Position.X += pc.MoveSpeed;
                    e = true;
                }

                if (currentKeyboardState.IsKeyDown(Keys.Up))
                {
                    //pc.Position.Y -= pc.MoveSpeed;
                    n = true;
                }

                if (currentKeyboardState.IsKeyDown(Keys.Down))
                {
                    //pc.Position.Y += pc.MoveSpeed;
                    s = true;
                }

                if (n || s || e || w)
                {
                    str = "MOVE " + pc.MoveSpeed.ToString();
                }
                if (n)
                {
                    str += " N";
                }
                if (s)
                {
                    str += " S";
                }
                if (e)
                {
                    str += " E";
                }
                if (w)
                {
                    str += " W";
                }
                if (n || s || e || w)
                {
                    actions.Enqueue(str);
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

            // TODO: Add your drawing code here

            // start drawing
            spriteBatch.Begin();

            lock (room)
            {
                //draw player
                //pc.Draw(spriteBatch);
                int i;
                for (i = 0; i < room.num; i++)
                {
                    //((PC)room.members[i]).Draw(spriteBatch);
                    DrawSprite(room.members[i], spriteBatch);
                }
            }

            base.Draw(gameTime);

            //stop draw
            spriteBatch.End();
        }

        
        /* loads another PC in */
        public void LoadPC(PC c)
        {
            // TODO: use this.Content to load your game content here

            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,
                GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            c.Initialize("player", playerPosition);
        }


        /* fetches the relevant Texture2D for a Texture string */
        public Texture2D LoadTexture(Actor a)
        {
            //this works, probably
            return Content.Load<Texture2D>(root + "\\Content\\Graphics\\" + a.Texture);
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
            spriteBatch.Draw(LoadTexture(a), a.Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
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
