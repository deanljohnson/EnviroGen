using System;
using EnviroGen.HeightMaps;

namespace EnviroGen.Noise.Modifiers
{
    public class ExponentModifier : IInvertableModifier
    {
        public float Exponent { get; set; }

        public ExponentModifier(float exp)
        {
            Exponent = exp;
        }

        public void Modify(HeightMap map)
        {
            for (var y = 0; y < map.Size.Y; y++)
            {
                for (var x = 0; x < map.Size.X; x++)
                {
                    map[x, y] = (float)Math.Pow(map[x, y], Exponent);
                }
            }
        }

        public void InvertModify(HeightMap map)
        {
            var exp = 1 / Exponent;

            for (var y = 0; y < map.Size.Y; y++)
            {
                for (var x = 0; x < map.Size.X; x++)
                {
                    map[x, y] = (float)Math.Pow(map[x, y], exp);
                }
            }
        }
    }
}
