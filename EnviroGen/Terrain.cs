using System.Collections.Generic;
using EnviroGen.Coloring;
using EnviroGen.HeightMaps;
using SFML.Graphics;
using SFML.Window;

namespace EnviroGen
{
    /// <summary>
    /// Includes land masses and oceans
    /// </summary>
    public class Terrain : Transformable, Drawable
    {
        /// <summary>
        /// The default colorizer that Terrain objects start with, unless specified in a constructor.
        /// Blends values from Black to White.
        /// </summary>
        private static Colorizer DefaultColorizer { get; set; }

        private HeightMap m_heightMap;
        private Sprite m_heightSprite { get; set; }

        /// <summary>
        /// Sets the Terrains height map. Setting this will cause a regeneration of the sprite.
        /// </summary>
        public HeightMap HeightMap
        {
            get { return m_heightMap; }
            set
            {
                m_heightMap = value;
                Colorize(Colorizer);
            }
        }

        public Colorizer Colorizer { get; set; }

        public List<Vector2i> RiverTiles { get; set; }

        static Terrain()
        {
            DefaultColorizer = new Colorizer();
            DefaultColorizer.AddColorRange(Color.Black, Color.White, 0f, 1f);
        }

        public Terrain(HeightMap heightMap)
            : this(heightMap, new List<Vector2i>(), DefaultColorizer)
        {
        }

        public Terrain(HeightMap heightMap, List<Vector2i> riverTiles, Colorizer colorizer)
        {
            Colorizer = colorizer;

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

        /// <summary>
        /// Uses Colorizer property to set the Terrain's Colors.
        /// </summary>
        public void Colorize()
        {
            m_heightSprite = new Sprite(new Texture(Colorizer.Colorize(m_heightMap)));
        }
    }
}
