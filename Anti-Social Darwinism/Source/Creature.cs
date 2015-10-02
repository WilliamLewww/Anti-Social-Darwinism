﻿using Microsoft.Xna.Framework;
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
                        DebugTools.createRectangle(new Rectangle(Rectangle.X - 5, Rectangle.Y - 5, Rectangle.Width + 10, Rectangle.Height + 10));

                    Selected = true;
                }
                else
                {
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

                if (creature.IsMoving == true)
                    movement.moveCreature(creature, Cursor.mousePosition);

                foreach (Creature creatureObject in creatureList)
                {
                    if (creature != creatureObject)
                        movement.creatureCollision(creature, creatureObject);
                }
            }


            int parentCounter = 0;

            foreach (Creature creature in selectedCreatureList)
            {
                if (creature.Selected == false)
                    tempSelectedCreatureList.Add(creature);

                DebugTools.followParent(parentCounter, creature.Rectangle, 5);
                parentCounter += 1;

                if (rightClickDown == true && Cursor.ReturnMouseStateRight == false)
                {
                    movement.resetMovement(creature);
                    creature.IsMoving = true;
                }
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
