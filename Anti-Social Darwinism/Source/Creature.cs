using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Anti_Social_Darwinism.Source
{
    class Creature
    {
        Texture2D texture;

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
        }

        public void Update(GameTime gameTime)
        {
            rectangle = new Rectangle((int)(position.X), (int)(position.Y), texture.Width, texture.Height);

            if (Cursor.ReturnMouseState == true)
                if (Cursor.ReturnRectangle.Intersects(Rectangle))
                {
                    if (Selected == false)
                        DebugTools.createRectangle(new Rectangle(Rectangle.X - 5, Rectangle.Y - 5, Rectangle.Width + 10, Rectangle.Height + 10));

                    Selected = true;
                }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }

    class CreatureList
    {
        CreatureMovement creatureMovement = new CreatureMovement();

        public static List<Creature> creatureList = new List<Creature>();

        public void LoadContent(ContentManager content)
        {
            Creature.Content = content;

            for (int x = 0; x < 10; x++)
                creatureList.Add(new Creature(0, x * 50 + 25, 75));
        }

        public void Update(GameTime gameTime)
        {
            int parentCounter = 0;
            foreach (Creature creature in creatureList)
            {
                if (creature.Selected == true)
                {
                    DebugTools.followParent(parentCounter, creature.Rectangle, 5);
                    parentCounter += 1;

                    if (Cursor.ReturnMouseStateRight == true)
                        creature.IsMoving = true;
                        
                }

                if (creature.IsMoving == true)
                {
                    creatureMovement.moveCreature(creature, Cursor.mousePosition);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Creature creature in creatureList)
                creature.Draw(spriteBatch);
        }
    }

    class CreatureMovement
    {
        public void moveCreature(Creature creature, Vector2 position)
        {
            Vector2 tempPosition = creature.Position;
            tempPosition.X = position.X;
            tempPosition.Y = position.Y;
            creature.Position = tempPosition;
        }
    }
}
