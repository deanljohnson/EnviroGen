namespace EnviroGen.Noise.Modifiers
{
    public class AddModifier : IInvertableModifier
    {
        public float Value { get; set; }

        public AddModifier(float value)
        {
            Value = value;
        }

        public void Modify(Environment environment)
        {
            var map = environment.Terrain;
            for (var y = 0; y < map.Size.Y; y++)
            {
                for (var x = 0; x < map.Size.X; x++)
                {
                    map[x, y] += Value;
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
                    map[x, y] -= Value;
                }
            }
        }
    }
}
