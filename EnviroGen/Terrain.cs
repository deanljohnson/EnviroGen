using System.Collections.Generic;
using EnviroGen.Coloring;
using SFML.Graphics;
using SFML.Window;

namespace EnviroGen
{
    /// <summary>
    /// Includes land masses and oceans
    /// </summary>
    public class Terrain : Transformable, Drawable
    {
        private static Colorizer DefaultColorizer { get; set; }

        private HeightMap m_heightMap;
        private Sprite m_heightSprite { get; set; }

        /// <summary>
        /// Sets the Terrains height map. Setting this will cause a regeneration of the sprite.
        /// </summary>
        public HeightMap HeightMap
        {
            private get { return m_heightMap; }
            set
            {
                m_heightMap = value;
                Colorize(DefaultColorizer);
            }
        }

        public List<Vector2i> RiverTiles { get; set; }

        static Terrain()
        {
            DefaultColorizer = new Colorizer();
            DefaultColorizer.AddColorRange(Color.Black, Color.White, 0f, 1f);
        }

        public Terrain(HeightMap heightMap)
            : this(heightMap, new List<Vector2i>())
        {
        }

        public Terrain(HeightMap heightMap, List<Vector2i> riverTiles, Colorizer colorizer = null)
        {
            if (colorizer == null)
            {
                colorizer = DefaultColorizer;
            }

            m_heightMap = heightMap;
            RiverTiles = riverTiles;
            Colorize(colorizer);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform.Combine(Transform);
            target.Draw(m_heightSprite, states);
        }

        /// <summary>
        /// Uses the given colorizer to set the pixel colors of the Terrains sprite.
        /// </summary>
        public void Colorize(Colorizer colorizer)
        {
            m_heightSprite = new Sprite(new Texture(colorizer.Colorize(m_heightMap)));
        }
    }
}
