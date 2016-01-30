using System;

namespace EnviroGen.Noise.Modifiers
{
    public class RidgedModifier : IModifier
    {
        public void Modify(Environment environment)
        {
            var map = environment.Terrain;
            for (var y = 0; y < map.Size.Y; y++)
            {
                for (var x = 0; x < map.Size.X; x++)
                {
                    map[x, y] = Math.Abs(map[x, y]);
                }
            }
        }
    }
}
