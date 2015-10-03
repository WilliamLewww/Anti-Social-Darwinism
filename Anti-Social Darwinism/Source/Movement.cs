using Microsoft.Xna.Framework;

namespace Anti_Social_Darwinism.Source
{
    class Movement
    {
        public void resetMovement(Creature creature)
        {
            if (creature.VelocityX.ContainsKey("selectedMovement")) creature.VelocityX.Remove("selectedMovement");
            if (creature.VelocityY.ContainsKey("selectedMovement")) creature.VelocityY.Remove("selectedMovement");

            creature.Destination = new Vector2(0, 0);
            creature.IsMoving = false;
        }

        public void moveCreature(Creature creature, Vector2 position)
        {
            if (creature.Destination == null || creature.Destination == new Vector2(0, 0))
                creature.Destination = position;

            if (!creature.VelocityX.ContainsKey("selectedMovement") || !creature.VelocityY.ContainsKey("selectedMovement"))
            {
                creature.VelocityX.Add("selectedMovement", 0);
                creature.VelocityY.Add("selectedMovement", 0);
            }

            if (creature.Position.X != (creature.Destination.X - (creature.Texture.Width / 2)) || creature.Position.Y != (creature.Destination.Y - (creature.Texture.Height / 2)))
            {
                if (creature.Position.X < creature.Destination.X - (creature.Texture.Width / 2))
                {
                    creature.VelocityX["selectedMovement"] = 3;

                    if (creature.Position.X + creature.VelocityX["selectedMovement"] > creature.Destination.X - (creature.Texture.Width / 2))
                        creature.VelocityX["selectedMovement"] = 1;
                }

                if (creature.Position.X > creature.Destination.X - (creature.Texture.Width / 2))
                {
                    creature.VelocityX["selectedMovement"] = -3;

                    if (creature.Position.X + creature.VelocityX["selectedMovement"] < creature.Destination.X - (creature.Texture.Width / 2))
                        creature.VelocityX["selectedMovement"] = -1;
                }

                if (creature.Position.X == creature.Destination.X - (creature.Texture.Width / 2))
                    creature.VelocityX["selectedMovement"] = 0;

                if (creature.Position.Y < creature.Destination.Y - (creature.Texture.Height / 2))
                {
                    creature.VelocityY["selectedMovement"] = 3;

                    if (creature.Position.Y + creature.VelocityY["selectedMovement"] > creature.Destination.Y - (creature.Texture.Height / 2))
                        creature.VelocityY["selectedMovement"] = 1;
                }

                if (creature.Position.Y > creature.Destination.Y - (creature.Texture.Height / 2))
                {
                    creature.VelocityY["selectedMovement"] = -3;

                    if (creature.Position.Y + creature.VelocityY["selectedMovement"] < creature.Destination.Y - (creature.Texture.Height / 2))
                        creature.VelocityY["selectedMovement"] = -1;
                }

                if (creature.Position.Y == creature.Destination.Y - (creature.Texture.Height / 2))
                    creature.VelocityY["selectedMovement"] = 0;
            }
            else
            {
                creature.Destination = new Vector2(0, 0);
                creature.VelocityX.Remove("selectedMovement");
                creature.VelocityY.Remove("selectedMovement");
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
                if (creatureA.NetVelocityY > creatureB.NetVelocityY)
                    if (!creatureB.VelocityY.ContainsKey("collision"))
                        creatureB.VelocityY.Add("collision", creatureA.NetVelocityY);
            }

            //bottom
            if ((creatureA.Rectangle.Y <= creatureB.Rectangle.Y + creatureB.Texture.Height) &&
                (creatureA.Rectangle.Y >= creatureB.Rectangle.Y + creatureB.Texture.Height - 25) &&
                (creatureA.Rectangle.X + creatureA.Texture.Width >= creatureB.Rectangle.X + 4) &&
                (creatureA.Rectangle.X <= creatureB.Rectangle.X + creatureB.Texture.Width - 4))
            {
                if (creatureA.NetVelocityY < creatureB.NetVelocityY)
                    if (!creatureB.VelocityY.ContainsKey("collision"))
                        creatureB.VelocityY.Add("collision", creatureA.NetVelocityY);
            }

            //left
            if ((creatureA.Rectangle.X + creatureA.Texture.Width >= creatureB.Rectangle.X) &&
                (creatureA.Rectangle.X + creatureA.Texture.Width <= creatureB.Rectangle.X + 25) &&
                (creatureA.Rectangle.Y + creatureA.Texture.Height >= creatureB.Rectangle.Y + 4) &&
                (creatureA.Rectangle.Y <= creatureB.Rectangle.Y + creatureB.Texture.Height - 4))
            {
                if (creatureA.NetVelocityX > creatureB.NetVelocityX)
                    if (!creatureB.VelocityX.ContainsKey("collision"))
                        creatureB.VelocityX.Add("collision", creatureA.NetVelocityX);
            }

            //right
            if ((creatureA.Rectangle.X <= creatureB.Rectangle.X + creatureB.Texture.Width) &&
                (creatureA.Rectangle.X >= creatureB.Rectangle.X + creatureB.Texture.Width - 25) &&
                (creatureA.Rectangle.Y + creatureA.Texture.Height >= creatureB.Rectangle.Y + 4) &&
                (creatureA.Rectangle.Y <= creatureB.Rectangle.Y + creatureB.Texture.Height - 4))
            {
                if (creatureA.NetVelocityX < creatureB.NetVelocityX)
                    if (!creatureB.VelocityX.ContainsKey("collision"))
                        creatureB.VelocityX.Add("collision", creatureA.NetVelocityX);
            }
        }
    }
}
