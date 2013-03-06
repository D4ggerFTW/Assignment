using System;
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
    class Wave
    {
        private int numOfEnemies; // Number of enemies to spawn
        private int waveNumber; // What wave is this?
        private float spawnTimer = 0; // When should we spawn an enemy
        private int enemiesSpawned = 0; // How many enemies have spawned
        private Player player; // Reference of the player
        private bool enemyAtEnd; // Has an enemy reached the end of the path?
        private bool spawningEnemies; // Are we still spawing enemies?
        private Level level; // A reference of the level
        private Texture2D enemyTexture; // A texture for the enemies
        public List<Protester> enemies = new List<Protester>(); // List of enemies
		
		//This returns whether the round is over 
        public bool RoundOver
        {
            get
            {
                return enemies.Count == 0 && enemiesSpawned == numOfEnemies; //bool
            }
        }
		
		//Returns round number
        public int RoundNumber
        {
            get { return waveNumber; }
        }
		
		//Getter/setter for the enemy reaching the end of the pathway
        public bool EnemyAtEnd
        {
            get { return enemyAtEnd; }
            set { enemyAtEnd = value; }
        }
		
		//the list containing the enemies spawned
        public List<Protester> Enemies
        {
            get { return enemies; }
        }
		
		//Constructor
        public Wave(int waveNumber, int numOfEnemies, Level level, Texture2D enemyTexture, Player player)
        {
            this.waveNumber = waveNumber;
            this.numOfEnemies = numOfEnemies;
            this.player = player;
            this.level = level;
            this.enemyTexture = enemyTexture;
        }
		
		//Used when spawning a new protestor.
        private void AddEnemy()
        {
            Protester enemy = new Protester(enemyTexture,
            level.Waypoints.Peek(), 50, 2, 0.5f);
            enemy.SetWaypoints(level.Waypoints); //sets waypoints
            enemies.Add(enemy); //adds enemy to list
            spawnTimer = 0;

            enemiesSpawned++; //increase counter
        }
		
		//Bool for beggining of round
        public void Start()
        {
            spawningEnemies = true;
        }
		
		//Update logic
        public void Update(GameTime gameTime)
        {
            if (enemiesSpawned == numOfEnemies)
                spawningEnemies = false; // We have spawned enough enemies
            if (spawningEnemies)
            {
                spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (spawnTimer > 2)
                    AddEnemy(); // Time to add a new enemy
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                Protester enemy = enemies[i];
                enemy.Update(gameTime);
                if (enemy.IsDead)
                {
                    if (enemy.CurrentHealth > 0) // Enemy is at the end
                    {
                        enemyAtEnd = true;
                        player.DamageHealth(enemy.Damage); //Removes lives from player
                    }
                    player.GiveBounty(enemies[i].BountyGiven); //Gives cash to player.
                    enemies.Remove(enemy);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Protester enemy in enemies)
                enemy.Draw(spriteBatch); //Draw the protesters
        }
    }
}
