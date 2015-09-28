using Microsoft.Xna.Framework;

namespace Anti_Social_Darwinism.Source
{
    class Movement
    {
        public void resetMovement(Creature creature)
        {
            creature.Destination = new Vector2(0, 0);
            creature.Speed = 0f;
            creature.IsMoving = false;
        }

        public void moveCreature(Creature creature, float speed, Vector2 position)
        {
            if (creature.Destination == null || creature.Destination == new Vector2(0, 0))
            {
                creature.Destination = position;
                creature.Speed = .3f;
            }

            if (creature.Position.X != (creature.Destination.X - (creature.Texture.Width / 2)) || creature.Position.Y != (creature.Destination.Y - (creature.Texture.Height / 2)))
            {
                Vector2 tempPosition = creature.Position;

                if (creature.Position.X < creature.Destination.X - (creature.Texture.Width / 2))
                    tempPosition.X += speed;
                if (creature.Position.X > creature.Destination.X - (creature.Texture.Width / 2))
                    tempPosition.X -= speed;
                if (creature.Position.Y < creature.Destination.Y - (creature.Texture.Height / 2))
                    tempPosition.Y += speed;
                if (creature.Position.Y > creature.Destination.Y - (creature.Texture.Height / 2))
                    tempPosition.Y -= speed;

                creature.Position = tempPosition;
            }
            else
            {
                creature.Destination = new Vector2(0, 0);
                creature.Speed = 0f;
                creature.IsMoving = false;
            }
        }
    }
}
