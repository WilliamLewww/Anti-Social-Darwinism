using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Anti_Social_Darwinism.Source
{
    class Creature
    {
        public static int creatureCount;

        Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        int creatureID;
        public int CreatureID
        {
            get { return creatureID; }
            set { creatureID = value; }
        }

        Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        bool selected;
        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        bool isMoving;
        public bool IsMoving
        {
            get { return isMoving; }
            set { isMoving = value; }
        }

        Dictionary<string, float> velocityX = new Dictionary<string, float>();
        public Dictionary<string, float> VelocityX
        {
            get { return velocityX; }
            set { velocityX = value; }
        }

        public float NetVelocityX
        {
            get
            {
                float netVelocity = 0;

                foreach (KeyValuePair<string, float> dictionary in velocityX)
                    netVelocity += dictionary.Value;

                return netVelocity;
            }
        }

        Dictionary<string, float> velocityY = new Dictionary<string, float>();
        public Dictionary<string, float> VelocityY
        {
            get { return velocityY; }
            set { velocityY = value; }
        }

        public float NetVelocityY
        {
            get
            {
                float netVelocity = 0;

                foreach (KeyValuePair<string, float> dictionary in velocityY)
                    netVelocity += dictionary.Value;

                return netVelocity;
            }
        }

        Vector2 destination;
        public Vector2 Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        private static ContentManager content;
        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public Creature(int i, int x, int y)
        {
            texture = Content.Load<Texture2D>("Sprites/creature");
            position = new Vector2(x, y);

            rectangle = new Rectangle((int)(position.X), (int)(position.Y), texture.Width, texture.Height);

            creatureID = creatureCount;
            creatureCount += 1;
        }

        public void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<string, float> dictionary in velocityX)
                position.X += dictionary.Value;

            foreach (KeyValuePair<string, float> dictionary in velocityY)
                position.Y += dictionary.Value;

            rectangle = new Rectangle((int)(position.X), (int)(position.Y), texture.Width, texture.Height);

            if (Cursor.ReturnMouseState == true)
            {
                if (Cursor.ReturnRectangle.Intersects(Rectangle))
                {
                    if (Selected == false)
                        DebugTools.createRectangle(CreatureID.ToString(), new Rectangle(Rectangle.X - 5, Rectangle.Y - 5, Rectangle.Width + 10, Rectangle.Height + 10));

                    Selected = true;
                }
                else
                {
                    DebugTools.removeRectangle(CreatureID.ToString());
                    Selected = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }

    class CreatureList
    {
        bool rightClickDown;

        Movement movement = new Movement();

        public static List<Creature> creatureList = new List<Creature>();

        public static List<Creature> selectedCreatureList = new List<Creature>();
        private static List<Creature> tempSelectedCreatureList = new List<Creature>();

        public void LoadContent(ContentManager content)
        {
            Creature.Content = content;

            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                    creatureList.Add(new Creature(0, x * 50 + 25, y * 50 + 25));
        }

        public void Update(GameTime gameTime)
        {
            if (Cursor.ReturnMouseStateRight == true)
                rightClickDown = true;

            foreach (Creature creature in creatureList)
            {
                if (creature.Selected == true)
                    if (!selectedCreatureList.Contains(creature))
                        selectedCreatureList.Add(creature);

                foreach (Creature creatureObject in creatureList)
                {
                    if (creature != creatureObject) movement.creatureCollision(creature, creatureObject);
                }
            }

            int parentCounter = 0, childCounter = 0, yOffsetCounter = 0, yOffset = 0, numberPerRow = 9;
            foreach (Creature creature in selectedCreatureList)
            {
                if (creature.IsMoving == true)
                {
                    if (yOffsetCounter / numberPerRow >= 1)
                    {
                        yOffset += 1;
                        yOffsetCounter = 0;
                    }

                    movement.moveCreature(creature, Cursor.mousePosition - new Vector2((childCounter - numberPerRow / 2) - (yOffset * numberPerRow) * 64, yOffset * 64));
                }

                if (creature.Selected == false)
                    tempSelectedCreatureList.Add(creature);
                else
                {
                    DebugTools.followParent(creature.CreatureID.ToString(), creature.Rectangle, 5);
                    parentCounter += 1;

                    if (rightClickDown == true && Cursor.ReturnMouseStateRight == false)
                    {
                        movement.resetMovement(creature);
                        creature.IsMoving = true;
                    }
                }

                childCounter += 1;
                yOffsetCounter += 1;
            }

            foreach (Creature creature in tempSelectedCreatureList)
                selectedCreatureList.Remove(creature);
            tempSelectedCreatureList.Clear();

            if (Cursor.ReturnMouseStateRight == false)
                rightClickDown = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Creature creature in creatureList)
                creature.Draw(spriteBatch);
        }
    }
}
