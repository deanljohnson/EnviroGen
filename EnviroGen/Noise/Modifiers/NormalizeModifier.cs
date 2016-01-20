using EnviroGen.HeightMaps;

namespace EnviroGen.Noise.Modifiers
{
    public class NormalizeModifier : IModifier
    {
        public float Low { get; set; }
        public float High { get; set; }

        public NormalizeModifier(float low, float high)
        {
            Low = low;
            High = high;
        }


        public void Modify(HeightMap map)
        {
            var maxValue = map[0, 0];
            var minValue = map[0, 0];

            foreach (var h in map.Map)
            {
                maxValue = h > maxValue ? h : maxValue;
                minValue = h < minValue ? h : minValue;
            }

            var valueDif = maxValue - minValue;
            var scaleDif = High - Low;

            for (uint y = 0; y < map.Size.Y; y++)
            {
                for (uint x = 0; x < map.Size.X; x++)
                {
                    map[x, y] = (scaleDif * (map[x, y] - minValue)) / (valueDif) + Low;
                }
            }
        }
    }
}
