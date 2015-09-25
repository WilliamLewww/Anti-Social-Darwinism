using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Anti_Social_Darwinism.Source
{
    class Creature
    {
        Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
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

        float speed;
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
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
        bool rightClickDown;

        Movement movement = new Movement();

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

            if (Cursor.ReturnMouseStateRight == true)
            {
                rightClickDown = true;
            }

            foreach (Creature creature in creatureList)
            {
                if (creature.Selected == true)
                {
                    DebugTools.followParent(parentCounter, creature.Rectangle, 5);
                    parentCounter += 1;

                    if (rightClickDown == true && Cursor.ReturnMouseStateRight == false)
                    {
                        movement.resetMovement(creature);
                        creature.IsMoving = true;
                    }
                        
                }

                if (creature.IsMoving == true)
                {
                    movement.moveCreature(creature, creature.Speed, Cursor.mousePosition);
                }
            }

            if (Cursor.ReturnMouseStateRight == false)
            {
                rightClickDown = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Creature creature in creatureList)
                creature.Draw(spriteBatch);
        }
    }
}
