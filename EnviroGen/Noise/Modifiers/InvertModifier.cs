namespace EnviroGen.Noise.Modifiers
{
    public class InvertModifier : IModifier
    {
        public float MaxValue { get; set; }

        public InvertModifier(float max)
        {
            MaxValue = max;
        }

        public void Modify(ref float[,] map)
        {

            for (var y = 0; y < map.GetLength(1); y++)
            {
                for (var x = 0; x < map.GetLength(0); x++)
                {
                    map[x, y] = MaxValue - map[x, y];
                }
            }
        }
    }
}
