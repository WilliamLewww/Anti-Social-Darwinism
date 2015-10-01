using Microsoft.Xna.Framework;

namespace Anti_Social_Darwinism.Source
{
    class Movement
    {
        public void resetMovement(Creature creature)
        {
            creature.Destination = new Vector2(0, 0);
            creature.VelocityX = 0f;
            creature.VelocityY = 0f;
            creature.IsMoving = false;
        }

        public void moveCreature(Creature creature, Vector2 position)
        {
            if (creature.Destination == null || creature.Destination == new Vector2(0, 0))
            {
                creature.Destination = position;
            }

            /*
            foreach (Creature creatureObject in CreatureList.creatureList)
            {
                if (creature != creatureObject)
                    creatureCollision(creature, creatureObject);
            }
            */

            if (creature.Position.X != (creature.Destination.X - (creature.Texture.Width / 2)) || creature.Position.Y != (creature.Destination.Y - (creature.Texture.Height / 2)))
            {
                Vector2 tempPosition = creature.Position;

                if (creature.Position.X < creature.Destination.X - (creature.Texture.Width / 2))
                {
                    creature.VelocityX = 3;

                    if (creature.Position.X + creature.VelocityX > creature.Destination.X - (creature.Texture.Width / 2))
                        creature.VelocityX = 1;

                    tempPosition.X += creature.VelocityX;
                }

                if (creature.Position.X > creature.Destination.X - (creature.Texture.Width / 2))
                {
                    creature.VelocityX = -3;

                    if (creature.Position.X + creature.VelocityX < creature.Destination.X - (creature.Texture.Width / 2))
                        creature.VelocityX = -1;

                    tempPosition.X += creature.VelocityX;
                }

                if (creature.Position.Y < creature.Destination.Y - (creature.Texture.Height / 2))
                {
                    creature.VelocityY = 3;

                    if (creature.Position.Y + creature.VelocityY > creature.Destination.Y - (creature.Texture.Height / 2))
                        creature.VelocityY = 1;

                    tempPosition.Y += creature.VelocityY;
                }

                if (creature.Position.Y > creature.Destination.Y - (creature.Texture.Height / 2))
                {
                    creature.VelocityY = -3;

                    if (creature.Position.Y + creature.VelocityY < creature.Destination.Y - (creature.Texture.Height / 2))
                        creature.VelocityY = -1;

                    tempPosition.Y += creature.VelocityY;
                }

                creature.Position = tempPosition;
            }
            else
            {
                creature.Destination = new Vector2(0, 0);
                creature.VelocityX = 0f;
                creature.VelocityY = 0f;
                creature.IsMoving = false;
            }
        }

        public void creatureCollision(Creature creatureA, Creature creatureB)
        {
            //top
            if ((creatureA.Rectangle.Y + creatureA.Texture.Height >= creatureB.Rectangle.Y) &&
                (creatureA.Rectangle.Y + creatureA.Texture.Height <= creatureB.Rectangle.Y + 25) &&
                (creatureA.Rectangle.X + creatureA.Texture.Width >= creatureB.Rectangle.X + 4) &&
                (creatureA.Rectangle.X <= creatureB.Rectangle.X + creatureB.Texture.Width - 4))
            {
                if (creatureA.VelocityY > creatureB.VelocityY)
                {
                    creatureB.VelocityY = creatureA.VelocityY;

                    Vector2 tempPosition = creatureB.Position;
                    float tempSpeed = creatureA.VelocityY;
                    tempPosition.Y += tempSpeed;
                    creatureB.Position = tempPosition;
                }

                if (creatureA.VelocityY == creatureB.VelocityY)
                {
                    Vector2 tempPosition = creatureB.Position;
                    float tempSpeed = creatureA.VelocityY;
                    tempPosition.Y += tempSpeed;
                    creatureB.Position = tempPosition;
                }
            }

            //bottom
            if ((creatureA.Rectangle.Y <= creatureB.Rectangle.Y + creatureB.Texture.Height) &&
                (creatureA.Rectangle.Y >= creatureB.Rectangle.Y + creatureB.Texture.Height - 25) &&
                (creatureA.Rectangle.X + creatureA.Texture.Width >= creatureB.Rectangle.X + 4) &&
                (creatureA.Rectangle.X <= creatureB.Rectangle.X + creatureB.Texture.Width - 4))
            {

            }

            //left
            if ((creatureA.Rectangle.X + creatureA.Texture.Width >= creatureB.Rectangle.X) &&
                (creatureA.Rectangle.X + creatureA.Texture.Width <= creatureB.Rectangle.X + 50) &&
                (creatureA.Rectangle.Y + creatureA.Texture.Height >= creatureB.Rectangle.Y + 3) &&
                (creatureA.Rectangle.Y <= creatureB.Rectangle.Y + creatureB.Texture.Height - 3))
            {

            }

            //right
            if ((creatureA.Rectangle.X <= creatureB.Rectangle.X + creatureB.Texture.Width) &&
                (creatureA.Rectangle.X >= creatureB.Rectangle.X + creatureB.Texture.Width - 50) &&
                (creatureA.Rectangle.Y + creatureA.Texture.Height >= creatureB.Rectangle.Y + 3) &&
                (creatureA.Rectangle.Y <= creatureB.Rectangle.Y + creatureB.Texture.Height - 3))
            {

            }
        }
    }
}
