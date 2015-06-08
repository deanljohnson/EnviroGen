namespace EnviroGen.Noise.Modifiers
{
    public class InvertModifier : IModifier
    {
        public void Modify(ref float[,] map)
        {
            var maxValue = map[0, 0];

            for (var y = 0; y < map.GetLength(1); y++)
            {
                for (var x = 0; x < map.GetLength(0); x++)
                {
                    if (map[x, y] > maxValue)
                    {
                        maxValue = map[x, y];
                    }
                }
            }

            for (var y = 0; y < map.GetLength(1); y++)
            {
                for (var x = 0; x < map.GetLength(0); x++)
                {
                    map[x, y] = maxValue - map[x, y];
                }
            }
        }
    }
}
