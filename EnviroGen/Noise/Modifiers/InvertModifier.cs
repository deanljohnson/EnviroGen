
namespace EnviroGen.Noise.Modifiers
{
    public class InvertModifier : IModifier
    {
        public float MaxValue { get; set; }

        public InvertModifier(float max)
        {
            MaxValue = max;
        }

        public void Modify(Environment environment)
        {
            var map = environment.Terrain;
            for (var y = 0; y < map.Size.Y; y++)
            {
                for (var x = 0; x < map.Size.X; x++)
                {
                    map[x, y] = MaxValue - map[x, y];
                }
            }
        }
    }
}
