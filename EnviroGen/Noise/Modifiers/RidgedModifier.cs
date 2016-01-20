using System;
using EnviroGen.HeightMaps;

namespace EnviroGen.Noise.Modifiers
{
    public class RidgedModifier : IModifier
    {
        public void Modify(HeightMap map)
        {
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
