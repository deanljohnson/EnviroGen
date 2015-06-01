using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;

namespace EnviroGen.Coloring
{
    public class Colorizer
    {
        private List<ColorRange> m_colorRanges { get; set; }

        public Colorizer()
        {
            m_colorRanges = new List<ColorRange>();
        }

        public void AddColorRange(Color lowColor, Color highColor, float lowHeight, float highHeight)
        {
            m_colorRanges.Add(new ColorRange(lowColor, highColor, lowHeight, highHeight));
        }

        public void AddColorRange(Color color, float lowHeight, float highHeight)
        {
            AddColorRange(color, color, lowHeight, highHeight);
        }

        public void AddColorRange(ColorRange range)
        {
            m_colorRanges.Add(range);
        }

        public Color GetColor(float height)
        {
            var color = m_colorRanges.First(cr => cr.InRange(height)).GetColor(height);
            return color;
        }
    }
}
