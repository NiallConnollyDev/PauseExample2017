using Engine.Engines;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Screens;
using System;

namespace PauseEx
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputEngine input;
        SplashScreen opening;
        SplashScreen Pause;
        TimeSpan pauseTime;
       
        public ActiveScreen current;
        private Texture2D playTx;
        private SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            input = new InputEngine(this);
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
            Helper.graphicsDevice = GraphicsDevice;
            opening = new SplashScreen(
                            Vector2.Zero, Content.Load<Texture2D>("OpeningScreen"),
                            Content.Load<Song>("backing track"),
                            Keys.Enter);
            opening.Active = true;
            current = ActiveScreen.OPENING;

            Pause = new SplashScreen(
                            Vector2.Zero, Content.Load<Texture2D>("Pause"),
                            Content.Load<Song>("success"),
                            Keys.P);

            playTx = Content.Load<Texture2D>("PlayScreen");
            font = Content.Load<SpriteFont>("font");
            // TODO: use this.Content to load your game content here
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
            switch (current)
            {
                case ActiveScreen.OPENING:
                    opening.Update();
                    if (!opening.Active)
                        current = ActiveScreen.PLAY;
                    break;
                case ActiveScreen.PAUSE:
                    Pause.Update();
                    if (!Pause.Active)
                    {
                        current = ActiveScreen.PLAY;
                        gameTime.TotalGameTime = pauseTime;
                    }
                    break;
                case ActiveScreen.PLAY:
                    // Need to check for change of state caused by pressing p in Pause Update
                    Pause.Update();
                    if (Pause.Active)
                    {
                        pauseTime = gameTime.TotalGameTime;
                        current = ActiveScreen.PAUSE;
                    }

                    // Play Update calls here do as methods 
                    break;
            }
            
            
            // TODO: Add your update logic here
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            opening.Draw(spriteBatch);
            Pause.Draw(spriteBatch);
            if (current == ActiveScreen.PLAY)
            {
                spriteBatch.Draw(playTx, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                spriteBatch.DrawString(font, 
                    "Time Elapsed " + Math.Round(gameTime.TotalGameTime.TotalSeconds).ToString(), 
                    new Vector2(10,10),
                    Color.Black);
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
