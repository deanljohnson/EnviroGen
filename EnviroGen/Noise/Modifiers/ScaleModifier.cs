namespace EnviroGen.Noise.Modifiers
{
    public class ScaleModifier : IInvertableModifier
    {
        public float Scale { get; set; }

        public ScaleModifier(float scale)
        {
            Scale = scale;
        }

        public void Modify(Environment environment)
        {
            var map = environment.Terrain;
            for (var y = 0; y < map.Size.Y; y++)
            {
                for (var x = 0; x < map.Size.X; x++)
                {
                    map[x, y] *= Scale;
                }
            }
        }

        public void InvertModify(Environment environment)
        {
            var map = environment.Terrain;
            for (var y = 0; y < map.Size.Y; y++)
            {
                for (var x = 0; x < map.Size.X; x++)
                {
                    map[x, y] /= Scale;
                }
            }
        }
    }
}
