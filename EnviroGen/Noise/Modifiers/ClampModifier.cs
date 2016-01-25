using EnviroGen.HeightMaps;

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

        public void Modify(HeightMap map)
        {
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
