using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Anti_Social_Darwinism
{
    class Cursor
    {
        static Rectangle mouseRectangle;
        public static Vector2 mousePosition;
        public static bool drawRectangle = false;

        List<Color> dataWidth = new List<Color>();
        List<Color> dataHeight = new List<Color>();
        List<Color> dataFill = new List<Color>();

        int differenceX, differenceY;

        Texture2D texture;
        Vector2 rectanglePointInitial, rectanglePointFinal;
        bool initialClick = false;
        bool reflectX = false, reflectY = false;

        Vector2 tempMousePosition;
        bool changedMousePosition = false, initializeMousePosition = true;

        int previousScrollValue = Mouse.GetState().ScrollWheelValue;

        private static GraphicsDevice graphics;
        public static GraphicsDevice Graphics
        {
            protected get { return graphics; }
            set { graphics = value; }
        }

        public static Rectangle ReturnRectangle
        {
            get { return mouseRectangle; }
        }

        public static bool ReturnMouseState
        {
            get {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    return true;

                return false;
            }
        }

        public static bool ReturnMouseStateRight
        {
            get {
                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                    return true;

                return false;
            }
        }

        public void setRectangle()
        {
            mouseRectangle = new Rectangle((int)(rectanglePointInitial.X), (int)(rectanglePointInitial.Y), (int)(Math.Abs(rectanglePointFinal.X - rectanglePointInitial.X)), (int)(Math.Abs(rectanglePointFinal.Y - rectanglePointInitial.Y)));

            if (changedMousePosition == true)
            {
                differenceX = (int)(mousePosition.X - tempMousePosition.X);
                differenceY = (int)(mousePosition.Y - tempMousePosition.Y);

                if (mouseRectangle.Width > 0 && mouseRectangle.Height > 0)
                {
                    if (rectanglePointFinal.X - rectanglePointInitial.X < 0)
                        reflectX = true;
                    else
                        reflectX = false;

                    if (rectanglePointFinal.Y - rectanglePointInitial.Y < 0)
                        reflectY = true;
                    else
                        reflectY = false;

                    generateRectangleColor();
                    initializeMousePosition = true;
                    changedMousePosition = false;
                }
            }

            if (reflectX == true)
                reflectRectangle(true);
            if (reflectY == true)
                reflectRectangle(false);
        }

        public void generateRectangleColor()
        {
            texture = new Texture2D(graphics, mouseRectangle.Width, mouseRectangle.Height);

            generateLists();

            //if (mouseRectangle.Width > 4 && mouseRectangle.Height > 4)
                //texture.SetData(0, new Rectangle(0, 0, mouseRectangle.Width, mouseRectangle.Height), dataFill.ToArray(), 0, mouseRectangle.Width * mouseRectangle.Height);

            texture.SetData(0, new Rectangle(0, 0, mouseRectangle.Width, 2), dataWidth.ToArray(), 0, mouseRectangle.Width);
            texture.SetData(0, new Rectangle(0, mouseRectangle.Height - 2, mouseRectangle.Width, 2), dataWidth.ToArray(), 0, mouseRectangle.Width);
            texture.SetData(0, new Rectangle(0, 0, 2, mouseRectangle.Height), dataHeight.ToArray(), 0, mouseRectangle.Height);
            texture.SetData(0, new Rectangle(mouseRectangle.Width - 2, 0, 2, mouseRectangle.Height), dataHeight.ToArray(), 0, mouseRectangle.Height);

            drawRectangle = true;
        }

        public void clearRectangleColor()
        {
            dataWidth.Clear();
            dataHeight.Clear();
            dataFill.Clear();
        }

        public void generateLists()
        {
            if (rectanglePointFinal.X - rectanglePointInitial.X > 0)
            {
                for (int x = 0; x < Math.Abs(differenceX) * 2; x++)
                {
                    if (differenceX > 0)
                        dataWidth.Add(new Color(0, 0, 0, 255));
                    if (differenceX < 0)
                        if ((dataWidth.Count - 1) - x > 0)
                            dataWidth.Remove(dataWidth[(dataWidth.Count - 1) - x]);
                }
            }
            else
            {
                if (rectanglePointFinal.X - rectanglePointInitial.X < 0)
                {
                    for (int x = 0; x < Math.Abs(differenceX) * 2; x++)
                    {
                        if (differenceX < 0)
                            dataWidth.Add(new Color(0, 0, 0, 255));
                        if (differenceX > 0)
                            if ((dataWidth.Count - 1) - x > 0)
                                dataWidth.Remove(dataWidth[(dataWidth.Count - 1) - x]);
                    }
                }
            }

            if (rectanglePointFinal.Y - rectanglePointInitial.Y > 0)
            {
                for (int x = 0; x < Math.Abs(differenceY) * 2; x++)
                {
                    if (differenceY > 0)
                        dataHeight.Add(new Color(0, 0, 0, 255));
                    if (differenceY < 0)
                        if ((dataHeight.Count - 1) - x > 0)
                            dataHeight.Remove(dataHeight[(dataHeight.Count - 1) - x]);
                }
            }
            else
            {
                if (rectanglePointFinal.Y - rectanglePointInitial.Y < 0)
                {
                    for (int x = 0; x < Math.Abs(differenceY) * 2; x++)
                    {
                        if (differenceY < 0)
                            dataHeight.Add(new Color(0, 0, 0, 255));
                        if (differenceY > 0)
                            if ((dataHeight.Count - 1) - x > 0)
                                dataHeight.Remove(dataHeight[(dataHeight.Count - 1) - x]);
                    }
                }
            }

            //for (int x = 0; x < mouseRectangle.Width * mouseRectangle.Height; x++)
                //dataFill.Add(new Color(0, 0, 255, 75));
        }

        public void reflectRectangle(bool x)
        {
            if (x == true)
                mouseRectangle.X = (int)mousePosition.X;
            if (x == false)
                mouseRectangle.Y = (int)mousePosition.Y;
        }

        public void Update(GameTime gameTime)
        {
            if (initializeMousePosition == true)
            {
                tempMousePosition = mousePosition;
                initializeMousePosition = false;
            }

            mousePosition = new Vector2(((Mouse.GetState().X - Game1.screenWidth / 2) * (1 / Camera.scale)) + Camera.cameraPosition.X, ((Mouse.GetState().Y - Game1.screenHeight / 2) * (1 / Camera.scale)) + Camera.cameraPosition.Y);

            if (mousePosition != tempMousePosition)
                changedMousePosition = true;

            if (Mouse.GetState().ScrollWheelValue < previousScrollValue)
            {
                if (Camera.scale > .3f)
                {
                    Camera.scale -= .1f;
                }
            }
            else if (Mouse.GetState().ScrollWheelValue > previousScrollValue)
            {
                if (Camera.scale < 2f)
                {   
                    Camera.scale += .1f;
                }
            }
            previousScrollValue = Mouse.GetState().ScrollWheelValue;

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (initialClick == false)
                {
                    rectanglePointInitial = mousePosition;
                    initialClick = true;
                }
                else
                    rectanglePointFinal = mousePosition;

                setRectangle();
            }
            else
            {
                if (changedMousePosition == true)
                {
                    initializeMousePosition = true;
                    changedMousePosition = false;
                }
            }

            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                drawRectangle = false;
                initialClick = false;
                clearRectangleColor();
                rectanglePointInitial = mousePosition;
                rectanglePointFinal = mousePosition;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (drawRectangle == true)
                spriteBatch.Draw(texture, mouseRectangle, Color.White);
        }
    }
}
