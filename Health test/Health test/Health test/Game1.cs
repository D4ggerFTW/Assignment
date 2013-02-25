using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
///################################################
///testing of the health bar, this is only regarding the graphical part
///in order to make it work we use UP and DOWN keys in order to make changes on the health bar
///this functionality is going to be replaced by the hits of the enemies
///##################################################
///

namespace Health_test
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D mHealthBar;
        int HealthNow = 100;
        
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

            //Load the HealthBar image from the disk into the Texture2D object
            mHealthBar = Content.Load<Texture2D>("HealthBar2");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the default game to exit on Xbox 360 and Windows

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)

                this.Exit();



            // TODO: Add your update logic here

            //Get the current keyboard state (which keys are currently pressed and released)

            KeyboardState KeyCondition = Keyboard.GetState();



            // increase the Health bar

            if (KeyCondition.IsKeyDown(Keys.Up) == true)
            {

                HealthNow += 1;

            }



            //decrease the Health bar

            if (KeyCondition.IsKeyDown(Keys.Down) == true)
            {

                HealthNow -= 1;

            }



            //Clamp method helps us restrict the value between the two values (float, min, max)

            HealthNow = (int)MathHelper.Clamp(HealthNow, 0, 100);

            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            //TODO: Add your drawing code here
            spriteBatch.Begin();

            ///Posisioning
            ///we draw the image at the center, possibly it should be resized later..
            
            spriteBatch.Draw(mHealthBar, new Rectangle(this.Window.ClientBounds.Width / 2 - mHealthBar.Width / 2, 30, mHealthBar.Width, 44), new Rectangle(0, 45, mHealthBar.Width, 44), Color.Gray);

            //Draw the health as red 
            // update: HealthNow helps us draw the part of the box we need to represent the HPs.
            spriteBatch.Draw(mHealthBar, new Rectangle(this.Window.ClientBounds.Width / 2 - mHealthBar.Width / 2, 30, (int)(mHealthBar.Width * ((double)HealthNow / 100)), 44), new Rectangle(0, 45, mHealthBar.Width, 44), Color.Red);

            //finally a box is drawn in order to make it more beautiful.
            spriteBatch.Draw(mHealthBar, new Rectangle(this.Window.ClientBounds.Width / 2 - mHealthBar.Width / 2, 30, mHealthBar.Width, 44), new Rectangle(0, 0, mHealthBar.Width, 44), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
