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

        public static int listCounter = 0;
        static List<Texture2D> textureList = new List<Texture2D>();
        public static List<Rectangle> rectangleList = new List<Rectangle>();

        public static void createRectangle(Rectangle rectangle)
        {
            rectangleList.Add(rectangle);
            textureList.Add(new Texture2D(graphics, rectangle.Width, rectangle.Height));

            Color[] dataWidth = new Color[rectangle.Width];
            for (int i = 0; i < dataWidth.Length; ++i) dataWidth[i] = new Color(0, 0, 100, 255);
            Color[] dataHeight = new Color[rectangle.Height];
            for (int i = 0; i < dataHeight.Length; ++i) dataHeight[i] = new Color(0, 0, 100, 255);

            textureList[listCounter].SetData(0, new Rectangle(0, 0, rectangle.Width, 1), dataWidth, 0, rectangle.Width);
            textureList[listCounter].SetData(0, new Rectangle(0, rectangle.Height - 1, rectangle.Width, 1), dataWidth, 0, rectangle.Width);
            textureList[listCounter].SetData(0, new Rectangle(0, 0, 1, rectangle.Height), dataHeight, 0, rectangle.Height);
            textureList[listCounter].SetData(0, new Rectangle(rectangle.Width - 1, 0, 1, rectangle.Height), dataHeight, 0, rectangle.Height);

            listCounter += 1;
        }

        public static void followParent(int x, Rectangle rectangleB, int offset)
        {
            Rectangle rectangle = rectangleList[x];
            rectangle.X = rectangleB.X - offset;
            rectangle.Y = rectangleB.Y - offset;
            rectangleList[x] = rectangle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < listCounter; x++)
            {
                spriteBatch.Draw(textureList[x], rectangleList[x], Color.White);
            }
        }
    }
}
