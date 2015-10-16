using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Anti_Social_Darwinism.Source
{
    class TileMap
    {
        public List<CollisionTiles> collisionTiles = new List<CollisionTiles>();

        public void Generate(int[,] map, int size)
        {
            for (int x = 0; x < map.GetLength(1); x++)
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];

                    if (number > 0)
                    {
                        collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));
                    }
                }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (CollisionTiles tile in collisionTiles)
                tile.Draw(spriteBatch);
        }
    }

    class Tile
    {
        protected static Texture2D texture;

        private Texture2D tileTexture;
        public Texture2D TileTexture
        {
            get { return tileTexture; }
            protected set { tileTexture = value; }
        }

        private Color[] textureData;
        public Color[] TextureData
        {
            get { return textureData; }
            protected set { textureData = value; }
        }

        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            protected set { rectangle = value; }
        }

        private static ContentManager content;
        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        private static GraphicsDevice graphics;
        public static GraphicsDevice Graphics
        {
            protected get { return graphics; }
            set { graphics = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tileTexture, rectangle, Color.White);
        }
    }

    class CollisionTiles : Tile
    {
        public CollisionTiles(int i, Rectangle newRectangle)
        {
            texture = Content.Load<Texture2D>("Sprites/tile");
            TextureData = new Color[32 * 32];
            Rectangle = newRectangle;

            texture.GetData(0, new Rectangle(0, 0, 32, 32), TextureData, 0, 1024);
            TileTexture = new Texture2D(Graphics, 32, 32);
            TileTexture.SetData(TextureData);
        }

        public void outlineTile(int side)
        {
            Color[] newTextureData = new Color[32];
            for (int x = 0; x < 32; x++)
                newTextureData[x] = new Color(0, 0, 0, 255);

            if (side == 0)
                TileTexture.SetData(0, new Rectangle(0, 0, 32, 1), newTextureData, 0, 32);
            if (side == 1)
                TileTexture.SetData(0, new Rectangle(0, 31, 32, 1), newTextureData, 0, 32);
            if (side == 2)
                TileTexture.SetData(0, new Rectangle(0, 0, 1, 32), newTextureData, 0, 32);
            if (side == 3)
                TileTexture.SetData(0, new Rectangle(31, 0, 1, 32), newTextureData, 0, 32);
        }
    }
}
