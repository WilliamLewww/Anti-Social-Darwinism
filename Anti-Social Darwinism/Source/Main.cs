using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Anti_Social_Darwinism.Source
{
    class Main
    {
        TileMap tileMap;
        EnvironmentGenerator environmentGenerator;

        CreatureList creatureList;

        DebugTools debugTools;

        public void LoadContent(ContentManager content, GraphicsDevice graphics)
        {
            Tile.Content = content;
            Tile.Graphics = graphics;
            Creature.Content = content;
            DebugTools.Graphics = graphics;

            tileMap = new TileMap();

            environmentGenerator = new EnvironmentGenerator();
            tileMap.Generate(environmentGenerator.generateTerrain(40, 25), 32);

            creatureList = new CreatureList();
            creatureList.LoadContent(content);

            debugTools = new DebugTools();
        }

        public void Update(GameTime gameTime)
        {
            foreach (Creature creature in CreatureList.creatureList)
                creature.Update(gameTime);

            creatureList.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            tileMap.Draw(spriteBatch);
            creatureList.Draw(spriteBatch);
            debugTools.Draw(spriteBatch);
        }
    }
}