using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

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

            if (!creature.VelocityX.ContainsKey("selectedMovement")) creature.VelocityX.Add("selectedMovement", 0);
            if (!creature.VelocityY.ContainsKey("selectedMovement")) creature.VelocityY.Add("selectedMovement", 0);

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

        public Creature getCreatureByID(string creatureID, List<Creature> creatureList)
        {
            if (creatureID.Substring(0, 9).Equals("collision"))
                foreach (Creature creature in creatureList)
                    if (creature.CreatureID == Int32.Parse(creatureID.Substring(9))) return creature;

            return null;
        }

        public void creatureCollision(Creature creatureA, Creature creatureB)
        {
            bool collisionX = false, collisionY = false;

            if (creatureA.NetVelocityY != 0 && creatureB.NetVelocityY != 0 && creatureA.NetVelocityY != creatureB.NetVelocityY)
            {

            }
            else
            {
                //top
                if ((creatureA.Rectangle.Y + creatureA.Texture.Height >= creatureB.Rectangle.Y) &&
                    (creatureA.Rectangle.Y + creatureA.Texture.Height <= creatureB.Rectangle.Y + 25) &&
                    (creatureA.Rectangle.X + creatureA.Texture.Width >= creatureB.Rectangle.X + 4) &&
                    (creatureA.Rectangle.X <= creatureB.Rectangle.X + creatureB.Texture.Width - 4))
                {
                    if (creatureA.NetVelocityY > creatureB.NetVelocityY)
                    {
                        if (!creatureB.VelocityY.ContainsKey("collision" + creatureA.CreatureID))
                            creatureB.VelocityY.Add("collision" + creatureA.CreatureID, creatureA.NetVelocityY);

                        collisionY = true;
                    }

                    if (creatureA.NetVelocityY == creatureB.NetVelocityY)
                        collisionY = true;
                }

                //bottom
                if ((creatureA.Rectangle.Y <= creatureB.Rectangle.Y + creatureB.Texture.Height) &&
                    (creatureA.Rectangle.Y >= creatureB.Rectangle.Y + creatureB.Texture.Height - 25) &&
                    (creatureA.Rectangle.X + creatureA.Texture.Width >= creatureB.Rectangle.X + 4) &&
                    (creatureA.Rectangle.X <= creatureB.Rectangle.X + creatureB.Texture.Width - 4))
                {
                    if (creatureA.NetVelocityY < creatureB.NetVelocityY)
                    {
                        if (!creatureB.VelocityY.ContainsKey("collision" + creatureA.CreatureID))
                            creatureB.VelocityY.Add("collision" + creatureA.CreatureID, creatureA.NetVelocityY);

                        collisionY = true;
                    }

                    if (creatureA.NetVelocityY == creatureB.NetVelocityY)
                        collisionY = true;
                }
            }

            if (collisionY == false)
                if (creatureB.VelocityY.ContainsKey("collision" + creatureA.CreatureID))
                    creatureB.VelocityY.Remove("collision" + creatureA.CreatureID);

            if (creatureA.NetVelocityX != 0 && creatureB.NetVelocityX != 0 && creatureA.NetVelocityX != creatureB.NetVelocityX)
            {

            }
            else
            {
                //left
                if ((creatureA.Rectangle.X + creatureA.Texture.Width >= creatureB.Rectangle.X) &&
                    (creatureA.Rectangle.X + creatureA.Texture.Width <= creatureB.Rectangle.X + 25) &&
                    (creatureA.Rectangle.Y + creatureA.Texture.Height >= creatureB.Rectangle.Y + 4) &&
                    (creatureA.Rectangle.Y <= creatureB.Rectangle.Y + creatureB.Texture.Height - 4))
                {
                    if (creatureA.NetVelocityX > creatureB.NetVelocityX)
                    {
                        if (!creatureB.VelocityX.ContainsKey("collision" + creatureA.CreatureID))
                            creatureB.VelocityX.Add("collision" + creatureA.CreatureID, creatureA.NetVelocityX);

                        collisionX = true;
                    }

                    if (creatureA.NetVelocityX == creatureB.NetVelocityX)
                        collisionX = true;
                }

                //right
                if ((creatureA.Rectangle.X <= creatureB.Rectangle.X + creatureB.Texture.Width) &&
                    (creatureA.Rectangle.X >= creatureB.Rectangle.X + creatureB.Texture.Width - 25) &&
                    (creatureA.Rectangle.Y + creatureA.Texture.Height >= creatureB.Rectangle.Y + 4) &&
                    (creatureA.Rectangle.Y <= creatureB.Rectangle.Y + creatureB.Texture.Height - 4))
                {
                    if (creatureA.NetVelocityX < creatureB.NetVelocityX)
                    {
                        if (!creatureB.VelocityX.ContainsKey("collision" + creatureA.CreatureID))
                            creatureB.VelocityX.Add("collision" + creatureA.CreatureID, creatureA.NetVelocityX);

                        collisionX = true;
                    }

                    if (creatureA.NetVelocityX == creatureB.NetVelocityX)
                        collisionX = true;
                }
            }

            if (collisionX == false)
                if (creatureB.VelocityX.ContainsKey("collision" + creatureA.CreatureID))
                    creatureB.VelocityX.Remove("collision" + creatureA.CreatureID);
        }
    }
}
