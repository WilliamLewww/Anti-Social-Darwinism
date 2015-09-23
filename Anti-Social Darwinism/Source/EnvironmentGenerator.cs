namespace Anti_Social_Darwinism.Source
{
    class EnvironmentGenerator
    {
        public int[,] generateTerrain(int width, int height)
        {
            int[,] tilemap = new int[height, width];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    tilemap[y, x] = 1;

            return tilemap;
        }
    }
}
