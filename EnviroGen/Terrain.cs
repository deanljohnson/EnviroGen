using EnviroGen.Coloring;
using EnviroGen.HeightMaps;
using SFML.Graphics;

namespace EnviroGen
{
    public class Terrain : HeightMapDrawable
    {
        public Terrain(HeightMap heightMap)
            : this(heightMap, DefaultColorizer)
        {
        }

        public Terrain(HeightMap heightMap, Colorizer colorizer)
        {
            Colorizer = colorizer;

            m_heightMap = heightMap;
            Colorize(colorizer);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform.Combine(Transform);
            target.Draw(Sprite, states);
        }
    }
}
