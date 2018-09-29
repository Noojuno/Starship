#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Redbus;
using Redbus.Interfaces;
using Starship.Entities;
using Starship.Event;
using Starship.Event.Input;
using Starship.Input;
using Starship.World.Blocks;

#endregion

namespace Starship
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class StarshipEngine : Game
    {
        public static StarshipEngine INSTANCE;
        public static IEventBus EVENT_BUS = new EventBus();

        public virtual string Name { get; } = "Starship";

        public Rectangle Size = Rectangle.Empty;

        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        public StarshipEngine(CommandLineOptions args)
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 1280;  // set this value to the desired width of your window
            this.graphics.PreferredBackBufferHeight = 720;   // set this value to the desired height of your window

            this.graphics.IsFullScreen = args.Fullscreen;

            this.graphics.ApplyChanges();

            this.Window.AllowUserResizing = false;
            this.Window.ClientSizeChanged += this.OnResize;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            StarshipEngine.INSTANCE = this;

            StarshipEngine.EVENT_BUS.Publish(new PreInitializationEvent("pre"));

            StarshipEngine.EVENT_BUS.Publish(new RegistryRegisterEvent<BlockDefinition>());

            StarshipEngine.EVENT_BUS.Publish(new InitializationEvent("init"));
            StarshipEngine.EVENT_BUS.Publish(new PostInitializationEvent("post"));

            this.Size = this.Window.ClientBounds;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //TODO: use this.Content to load your game content here 
        }

        public void OnResize(Object sender, EventArgs e)
        {
            this.Window.ClientSizeChanged -= this.OnResize;
            this.graphics.PreferredBackBufferWidth = this.Window.ClientBounds.Width < 100 ? 100 : this.Window.ClientBounds.Width;
            this.graphics.PreferredBackBufferHeight = this.Window.ClientBounds.Height < 100 ? 100 : this.Window.ClientBounds.Height;

            this.graphics.ApplyChanges();

            StarshipEngine.EVENT_BUS.Publish(new ResizeEvent(this.Window.ClientBounds));

            this.Window.ClientSizeChanged += this.OnResize;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {	
            InputManager.Update(gameTime);
            EntityManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GraphicsDevice.Clear(Color.Black);

            this.Render(gameTime);
        }

        protected virtual void Render(GameTime gameTime)
        {
            EntityManager.Render(gameTime);
        }
    }
}
