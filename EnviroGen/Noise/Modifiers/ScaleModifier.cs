namespace EnviroGen.Noise.Modifiers
{
    public class ScaleModifier : IModifier
    {
        public float Scale { get; set; }

        public ScaleModifier(float scale)
        {
            Scale = scale;
        }

        public void Modify(ref float[,] map)
        {
            for (var y = 0; y < map.GetLength(1); y++)
            {
                for (var x = 0; x < map.GetLength(0); x++)
                {
                    map[x, y] *= Scale;
                }
            }
        }
    }
}
