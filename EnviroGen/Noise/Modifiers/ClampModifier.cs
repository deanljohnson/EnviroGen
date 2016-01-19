namespace EnviroGen.Noise.Modifiers
{
    public class ClampModifier : IModifier
    {
        public float Low { get; set; }
        public float High { get; set; }

        public ClampModifier(float low, float high)
        {
            Low = low;
            High = high;
        }

        public void InvertModify(ref float[,] map)
        {
            for (uint y = 0; y < map.GetLength(1); y++)
            {
                for (uint x = 0; x < map.GetLength(0); x++)
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
