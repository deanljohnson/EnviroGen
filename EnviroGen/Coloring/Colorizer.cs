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

        /// <summary>
        /// Returns an Image with coloring applied based on the given HeightMap
        /// </summary>
        public Image Colorize(HeightMap map)
        {
            var img = new Image(map.Size.X, map.Size.Y);

            for (uint y = 0; y < map.Size.Y; y++)
            {
                for (uint x = 0; x < map.Size.X; x++)
                {
                    img.SetPixel(x, y, GetColor(map[x, y]));
                }
            }

            return img;
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

        public void Clear()
        {
            m_colorRanges.Clear();
        }
    }
}
