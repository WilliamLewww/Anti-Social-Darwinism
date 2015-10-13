using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Anti_Social_Darwinism.Source
{
    class DebugTools
    {
        private static GraphicsDevice graphics;
        public static GraphicsDevice Graphics
        {
            protected get { return graphics; }
            set { graphics = value; }
        }

        public static Dictionary<string, Texture2D> textureList = new Dictionary<string, Texture2D>();
        public static Dictionary<string, Rectangle> rectangleList = new Dictionary<string, Rectangle>();

        public static void removeRectangle(string objectID)
        {
            textureList.Remove(objectID);
            rectangleList.Remove(objectID);
        }

        public static void createRectangle(string objectID, Rectangle rectangle)
        {
            rectangleList.Add(objectID, rectangle);
            textureList.Add(objectID, new Texture2D(graphics, rectangle.Width, rectangle.Height));

            Color[] dataWidth = new Color[rectangle.Width];
            for (int i = 0; i < dataWidth.Length; ++i) dataWidth[i] = new Color(0, 0, 100, 255);
            Color[] dataHeight = new Color[rectangle.Height];
            for (int i = 0; i < dataHeight.Length; ++i) dataHeight[i] = new Color(0, 0, 100, 255);

            textureList[objectID].SetData(0, new Rectangle(0, 0, rectangle.Width, 1), dataWidth, 0, rectangle.Width);
            textureList[objectID].SetData(0, new Rectangle(0, rectangle.Height - 1, rectangle.Width, 1), dataWidth, 0, rectangle.Width);
            textureList[objectID].SetData(0, new Rectangle(0, 0, 1, rectangle.Height), dataHeight, 0, rectangle.Height);
            textureList[objectID].SetData(0, new Rectangle(rectangle.Width - 1, 0, 1, rectangle.Height), dataHeight, 0, rectangle.Height);
        }

        public static void followParent(string objectID, Rectangle rectangleB, int offset)
        {
            Rectangle rectangle = rectangleList[objectID];
            rectangle.X = rectangleB.X - offset;
            rectangle.Y = rectangleB.Y - offset;
            rectangleList[objectID] = rectangle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (KeyValuePair<string, Texture2D> dictionary in textureList)
            {
                spriteBatch.Draw(dictionary.Value, rectangleList[dictionary.Key], Color.White);
            }
        }
    }
}
