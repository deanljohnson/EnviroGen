using EnviroGen.Coloring;
using EnviroGen.HeightMaps;

namespace EnviroGen
{
    public class Clouds
    {
        public static Colorizer DefaultColorizer { get; }

        private Colorizer m_Colorizer;

        public HeightMap HeightMap { get; }
        public Image Image { get; private set; }

        static Clouds()
        {
            var colorRange = new ColorRange(System.Windows.Media.Color.FromRgb(0, 0, 0),
                System.Windows.Media.Color.FromRgb(255, 255, 255), 0f, 1f);

            DefaultColorizer = new Colorizer(colorRange);
        }

        public Colorizer Colorizer
        {
            get { return m_Colorizer; }
            set
            {
                m_Colorizer = value;
                Colorize(m_Colorizer);
            }
        }

        public Clouds(HeightMap heightMap)
            : this(heightMap, DefaultColorizer)
        {
        }

        public Clouds(HeightMap heightMap, Colorizer colorizer)
        {
            HeightMap = heightMap;
            Colorizer = colorizer;
            Colorize(colorizer);
        }

        /// <summary>
        /// Uses the given colorizer to set the pixel colors of the Terrains sprite.
        /// </summary>
        public void Colorize(Colorizer colorizer)
        {
            Image = new Image(colorizer.Colorize(HeightMap));
        }

        /// <summary>
        /// Uses Colorizer property to set the Terrain's Colors.
        /// </summary>
        public void Colorize()
        {
            Image = new Image(Colorizer.Colorize(HeightMap));
        }
    }
}
