﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class Player
    {
        private int money = 50; // Money to be spent on towers
        private int lives; // Current life
        private int initlives; // Starting life
        private List<Tower> towers = new List<Tower>(); // Towers the player has

        private MouseState mouseState; // Mouse state for the current frame
        private MouseState oldState; // Mouse state for the previous frame
        private Level level; //A reference of the level

        private int cellX;
        private int cellY;

        private int tileX;
        private int tileY;

        private Texture2D bulletTexture; // textures
        private Texture2D towerTexture;
        private Texture2D healthBarTexture;
        public void Update(GameTime gameTime, List<Protester> enemies)
        {
            mouseState = Mouse.GetState();

            cellX = (int)(mouseState.X / 32); // Convert the position of the mouse
            cellY = (int)(mouseState.Y / 32); // from array space to level space

            tileX = cellX * 32; // Convert from array space to level space
            tileY = cellY * 32; // Convert from array space to level space

            if (mouseState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed)
            {
                if (IsCellClear())
                {
                    ArrowTower tower = new ArrowTower(towerTexture, bulletTexture, new Vector2(tileX, tileY));
                    if (tower.Cost >= this.money)
                        tower = null;
                    else
                    {
                        this.money -= tower.Cost;
                        towers.Add(tower);
                    }
                }
            }

            foreach (Tower tower in towers)
            {
                if (tower.Target == null)
                {
                    tower.GetClosestEnemy(enemies);
                }

                tower.Update(gameTime);
            }

            oldState = mouseState; // Set the oldState so it becomes the state of the previous frame.
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tower tower in towers)
            {
                tower.Draw(spriteBatch);
            }
            spriteBatch.Draw(healthBarTexture, new Rectangle(256, 0, 256, 44), new Rectangle(0, 45, 256, 44), Color.Gray, MathHelper.ToRadians(270), Vector2.Zero, SpriteEffects.None, 1);

            //Draw the health as red 
            // update: HealthNow helps us draw the part of the box we need to represent the HPs.
            spriteBatch.Draw(healthBarTexture, new Rectangle(256, 0, 256, 44), new Rectangle(0, 45, 256 * (lives / initlives), 44), Color.Red, MathHelper.ToRadians(270), Vector2.Zero, SpriteEffects.None, 1);

            //finally a box is drawn in order to make it more beautiful.
            spriteBatch.Draw(healthBarTexture, new Rectangle(256, 0, 256, 44), new Rectangle(0, 45, 256, 44), Color.White, MathHelper.ToRadians(270), Vector2.Zero, SpriteEffects.None, 1);
        }

        private bool IsCellClear()
        {
            bool inBounds = cellX >= 0 && cellY >= 0 && // Make sure tower is within limits
                cellX < level.Width && cellY < level.Height;

            bool spaceClear = true;

            foreach (Tower tower in towers) // Check that there is no tower here
            {
                spaceClear = (tower.Position != new Vector2(tileX, tileY));

                if (!spaceClear)
                    break;
            }

            bool onPath = (level.GetIndex(cellX, cellY) != 1);

            return inBounds && spaceClear && onPath; // If both checks are true return true
        }

        public Player(Level level, Texture2D towerTexture, Texture2D bulletTexture,Texture2D hpbarText)
        {
            this.level = level;
            this.healthBarTexture = hpbarText;
            this.towerTexture = towerTexture;
            this.bulletTexture = bulletTexture;
            this.initlives = 30;
            this.lives = this.initlives;
        }

        public int Money
        {
            get { return money; }
        }
        public int Lives
        {
            get { return lives; }
        }

        public void GiveBounty(int bounty)
        {
            money += bounty;
        }

        public void DamageHealth(int amount)
        {
            lives -= amount;
        }
    }
}
