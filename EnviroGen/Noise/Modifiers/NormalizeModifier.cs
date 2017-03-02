namespace EnviroGen.Noise.Modifiers
{
    /// <summary>
    /// Normalizes an Environment's HeightMap to be within a certain range
    /// </summary>
    public class NormalizeModifier : IModifier
    {
        public float Low { get; set; }
        public float High { get; set; }

        public NormalizeModifier(float low, float high)
        {
            Low = low;
            High = high;
        }


        public void Modify(Environment environment)
        {
            var map = environment.Terrain;
            var maxValue = map[0, 0];
            var minValue = map[0, 0];

            foreach (float h in map)
            {
                maxValue = h > maxValue ? h : maxValue;
                minValue = h < minValue ? h : minValue;
            }

            var valueDif = maxValue - minValue;
            var scaleDif = High - Low;

            for (var y = 0; y < map.Size.Y; y++)
            {
                for (var x = 0; x < map.Size.X; x++)
                {
                    map[x, y] = (scaleDif * (map[x, y] - minValue)) / (valueDif) + Low;
                }
            }
        }
    }
}
