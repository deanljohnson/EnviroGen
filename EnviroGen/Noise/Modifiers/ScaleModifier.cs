namespace EnviroGen.Noise.Modifiers
{
    public static class ScaleModifier
    {
        public static void Modify(ref float[,] map, float scale)
        {
            for (var y = 0; y < map.GetLength(1); y++)
            {
                for (var x = 0; x < map.GetLength(0); x++)
                {
                    map[x, y] *= scale;
                }
            }
        }
    }
}
