namespace EnviroGen.Noise.Modifiers
{
    public class AddModifier : IInvertableModifier
    {
        public float Value { get; set; }

        public AddModifier(float value)
        {
            Value = value;
        }

        public void Modify(ref float[,] map)
        {
            for (var y = 0; y < map.GetLength(1); y++)
            {
                for (var x = 0; x < map.GetLength(0); x++)
                {
                    map[x, y] += Value;
                }
            }
        }

        public void InvertModify(ref float[,] map)
        {
            for (var y = 0; y < map.GetLength(1); y++)
            {
                for (var x = 0; x < map.GetLength(0); x++)
                {
                    map[x, y] -= Value;
                }
            }
        }
    }
}
