namespace EnviroGen.Noise.Modifiers
{
    /// <summary>
    /// Clamps an Environment's HeightMap values between two numbers.
    /// Any value lower than the minimum is set to the minimum,
    /// and any value higher than the maximum is set to the maximum.
    /// </summary>
    public class ClampModifier : IModifier
    {
        public float Low { get; set; }
        public float High { get; set; }

        public ClampModifier(float low, float high)
        {
            Low = low;
            High = high;
        }

        public void Modify(Environment environment)
        {
            var map = environment.Terrain;
            for (var y = 0; y < map.Size.Y; y++)
            {
                for (var x = 0; x < map.Size.X; x++)
                {
                    if (map[x, y] < Low)
                    {
                        map[x, y] = Low;
                    }
                    else if (map[x, y] > High)
                    {
                        map[x, y] = High;
                    }
                }
            }
        }
    }
}
