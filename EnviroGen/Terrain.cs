using EnviroGen.Coloring;
using EnviroGen.HeightMaps;
using System.Windows.Media;

namespace EnviroGen
{
    public class Terrain : HeightMap
    {
        public static Colorizer DefaultColorizer { get; }

        private Colorizer m_Colorizer;

        public Image Image { get; private set; }

        public Colorizer Colorizer
        {
            get { return m_Colorizer; }
            set
            {
                m_Colorizer = value;
                Colorize(m_Colorizer);
            }
        }

        static Terrain()
        {
            DefaultColorizer = new Colorizer(
                new ColorRange(Color.FromRgb(0, 0, 255), Color.FromRgb(255, 255, 0), -1f, -.5f),
                new ColorRange(Color.FromRgb(255, 255, 0), Color.FromRgb(0, 255, 0), -.5f, 0f),
                new ColorRange(Color.FromRgb(0, 255, 0), Color.FromRgb(255, 0, 0), 0f, .5f),
                new ColorRange(Color.FromRgb(255, 0, 0), Color.FromRgb(255, 255, 255), .5f, 1f));
        }

        public Terrain(HeightMap map, Colorizer colorizer)
            : this(map.Map, colorizer)
        {
        }

        public Terrain(HeightMap map)
            : this(map.Map, DefaultColorizer)
        {
        }

        public Terrain(float[,] heights)
            : this(heights, DefaultColorizer)
        {
        }

        public Terrain(float[,] heights, Colorizer colorizer)
            : base(heights)
        {
            Colorizer = colorizer;
            Colorize(colorizer);
        }

        public void UpdateImage()
        {
            Image = new Image(Colorizer.Colorize(this));
        }

        /// <summary>
        /// Uses the given colorizer to set the pixel colors of the Terrains Image property.
        /// </summary>
        public void Colorize(IColorizer colorizer)
        {
            Image = new Image(colorizer.Colorize(this));
        }

        /// <summary>
        /// Uses Colorizer property to set the Terrain's Colors.
        /// </summary>
        public void Colorize()
        {
            Image = new Image(Colorizer.Colorize(this));
        }
    }
}
