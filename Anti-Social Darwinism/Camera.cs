using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Anti_Social_Darwinism
{
    class Camera
    {
        public Matrix transform;
        public static Vector2 cameraPosition = new Vector2(Game1.screenWidth / 2, Game1.screenHeight / 2);
        public static float scale = 1.0f;

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                cameraPosition.X -= 7 * (1 / scale);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                cameraPosition.X += 7 * (1 / scale);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                cameraPosition.Y -= 7 * (1 / scale);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                cameraPosition.Y += 7 * (1 / scale);
            }

            transform = Matrix.CreateTranslation(new Vector3(-cameraPosition, 0)) * Matrix.CreateScale(new Vector3(scale)) * Matrix.CreateTranslation(new Vector3(Game1.screenWidth / 2, Game1.screenHeight / 2, 0));
        }
    }
}
