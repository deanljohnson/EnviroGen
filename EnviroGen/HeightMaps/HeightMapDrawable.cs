using EnviroGen.Coloring;
using SFML.Graphics;

namespace EnviroGen.HeightMaps
{
    public abstract class HeightMapDrawable : Transformable, Drawable
    {
        protected static Colorizer DefaultColorizer { get; private set; }

        protected HeightMap m_heightMap;
        protected Sprite m_sprite;

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

        static HeightMapDrawable()
        {
            DefaultColorizer = new Colorizer();
            DefaultColorizer.AddColorRange(Color.Black, Color.White, 0f, 1f);
        }

        /// <summary>
        /// Uses the given colorizer to set the pixel colors of the Terrains sprite.
        /// </summary>
        public void Colorize(Colorizer colorizer)
        {
            m_sprite = new Sprite(new Texture(colorizer.Colorize(m_heightMap)));
        }

        /// <summary>
        /// Uses Colorizer property to set the Terrain's Colors.
        /// </summary>
        public void Colorize()
        {
            m_sprite = new Sprite(new Texture(Colorizer.Colorize(m_heightMap)));
        }

        public abstract void Draw(RenderTarget target, RenderStates states);
    }
}
