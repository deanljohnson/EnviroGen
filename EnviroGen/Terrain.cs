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
    }
}
