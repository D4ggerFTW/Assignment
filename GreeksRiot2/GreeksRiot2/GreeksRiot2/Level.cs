using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GreeksRiot2
{
    public class Level
    {
        private List<Texture2D> tileTextures = new List<Texture2D>(); //List containing the tile textures.
        private Queue<Vector2> waypoints = new Queue<Vector2>(); //Waypoints for the protestors to go around the map

        int[,] map = new int[,]
        {
            {0,0,1,0,0,0,0,0,},
            {0,0,1,1,0,0,0,0,},
            {0,0,0,1,1,0,0,0,},
            {0,0,0,0,1,0,0,0,},
            {0,0,0,1,1,0,0,0,},
            {0,0,1,1,0,0,0,0,},
            {0,0,1,0,0,0,0,0,},
            {0,0,1,1,1,1,1,1,},
        };

        public Queue<Vector2> Waypoints 
        {
            get { return waypoints; }
        }

        public int Width // gets the width of the map in tiles
        {
            get { return map.GetLength(1); }
        }
        public int Height // gets the height of the map in tiles
        {
            get { return map.GetLength(0); }
        }

        public Level() //constructor
        {
            waypoints.Enqueue(new Vector2(2, 0) * 32); //waypoints, defined for protestors
            waypoints.Enqueue(new Vector2(2, 1) * 32);
            waypoints.Enqueue(new Vector2(3, 1) * 32);
            waypoints.Enqueue(new Vector2(3, 2) * 32);
            waypoints.Enqueue(new Vector2(4, 2) * 32);
            waypoints.Enqueue(new Vector2(4, 4) * 32);
            waypoints.Enqueue(new Vector2(3, 4) * 32);
            waypoints.Enqueue(new Vector2(3, 5) * 32);
            waypoints.Enqueue(new Vector2(2, 5) * 32);
            waypoints.Enqueue(new Vector2(2, 7) * 32);
            waypoints.Enqueue(new Vector2(7, 7) * 32);
        }

        public void AddTexture(Texture2D texture) //adds textures for map
        {
            tileTextures.Add(texture);
        }

        public void Draw(SpriteBatch batch) // draws the map
        {
            for (int x = 0; x < Width; x++) //for each row of tiles
            {
                for (int y = 0; y < Height; y++) // for each column of tiles
                {
                    int textureIndex = map[y, x]; 

                    if (textureIndex == -1) //check if it's out of bounds
                        continue;

                    Texture2D texture = tileTextures[textureIndex]; // pick the right texture from the list based on the current index

                    batch.Draw(texture, new Rectangle(x * 32, y * 32, 32, 32), Color.White); //draw it
                }
            }
        }

        public int GetIndex(int cellX, int cellY) //gets value of tile at X row and Y column
        {
            if (cellX < 0 || cellX > Width || cellY < 0 || cellY > Height)
                return 0;

            return map[cellY, cellX];
        }
    }
}
