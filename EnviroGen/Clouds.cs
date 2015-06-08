using EnviroGen.Coloring;
using EnviroGen.HeightMaps;
using SFML.Graphics;

namespace EnviroGen
{
    public class Clouds : HeightMapDrawable
    {
        public Clouds(HeightMap heightMap)
            : this(heightMap, DefaultColorizer)
        {
        }

        public Clouds(HeightMap heightMap, Colorizer colorizer)
        {
            Colorizer = colorizer;

            m_heightMap = heightMap;
            Colorize(colorizer);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform.Combine(Transform);
            target.Draw(m_sprite, states);
        }
    }
}
