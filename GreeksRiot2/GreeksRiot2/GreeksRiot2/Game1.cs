



///############################################################################################
///                  ########### by:  cOLDsNAP  #####################
///                  Project Manager: Tsakiris Marios
///                  Programmer #1  : Ares-Raymond Slavov
///Programmer #2/Graphics Designer  : Neromiliotis Alexandros
/// ###########################################################################################
///                 --------------Greeks Riot--------------
///                 
///Summary: This is a Tower Defence game Demo made for presentation only. The player must save the
///Greek Parliament from the angry protesters that want to burn it down. The player must use the police force 
///in order to stop the protesters from coming near the Parliament.
///
/// In this demo the parts of the game that have been implemented are:
/// 
/*
 1)
 2)
 3)
 4)
 */


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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace GreeksRiot2
{
    
    /// 
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Riot : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Wave wave;
        Level level = new Level();
        Player player;
        public Riot()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = (level.Width * 32) + 100;
            graphics.PreferredBackBufferHeight = level.Height * 32;
            graphics.ApplyChanges();
            IsMouseVisible = true;
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

            // TODO: use this.Content to load your game content here
            Texture2D grass = Content.Load<Texture2D>("grass");
            Texture2D path = Content.Load<Texture2D>("path");
            level.AddTexture(grass);
            level.AddTexture(path);

            Texture2D towerTexture = Content.Load<Texture2D>("arrowtower");
            Texture2D bulletTexture = Content.Load<Texture2D>("bullet");
            Texture2D healthBarTexture = Content.Load<Texture2D>("HealthBar2");
            player = new Player(level, towerTexture, bulletTexture, healthBarTexture);
            Texture2D protesterTexture = Content.Load<Texture2D>("protester");
            wave = new Wave(0, 10, level, protesterTexture, player);
            wave.Start();
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            wave.Update(gameTime);
            player.Update(gameTime, wave.enemies);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            level.Draw(spriteBatch);
            wave.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
            
        }
    }
}
