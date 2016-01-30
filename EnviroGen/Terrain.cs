using EnviroGen.Coloring;
using EnviroGen.HeightMaps;
using System.Windows.Media;

namespace EnviroGen
{
    public class Terrain : HeightMap
    {
        public static IColorizer DefaultColorizer { get; }

        private IColorizer m_Colorizer;

        public Image Image { get; private set; }

        public IColorizer Colorizer
        {
            get { return m_Colorizer; }
            set
            {
                m_Colorizer = value;
                Image = new Image(m_Colorizer.Colorize(this));
            }
        }

        public override float this[int x, int y]
        {
            get { return base[x, y]; }
            set
            {
                base[x, y] = value;
                Image[(uint) x, (uint) y] = m_Colorizer.GetBaseColor(value, true);
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

        public Terrain(HeightMap map, IColorizer colorizer)
            : base(map)
        {
            Colorizer = colorizer;
            Image = new Image(colorizer.Colorize(this));
        }

        public Terrain(HeightMap map)
            : this(map, new Colorizer(DefaultColorizer.BaseColorRanges))
        {
        }

        /// <summary>
        /// Returns a new Terrain instance with dimensions equal to the greatest multiple 
        /// of m less than or equal to this Terrain's current dimensions.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public Terrain SizeTruncatedToMultiple(int m)
        {
            var width = Size.X - (Size.X % m);
            var height = Size.Y - (Size.Y % m);

            var heights = new float[width, height];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    heights[x, y] = this[x, y];
                }
            }

            var map = new HeightMap(heights);

            return new Terrain(map, new Colorizer(Colorizer.BaseColorRanges));
        }
    }
}
