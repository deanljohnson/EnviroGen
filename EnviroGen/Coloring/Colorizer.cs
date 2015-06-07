using System.Collections.Generic;
using System.Linq;
using EnviroGen.HeightMaps;
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

        public Colorizer(List<ColorRange> colorRanges)
        {
            m_colorRanges = colorRanges;
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

        /// <summary>
        /// Creates a new ColorRange object based on the given values and adds it to this Colorizer.
        /// </summary>
        public void AddColorRange(Color lowColor, Color highColor, float lowHeight, float highHeight)
        {
            m_colorRanges.Add(new ColorRange(lowColor, highColor, lowHeight, highHeight));
        }

        /// <summary>
        /// Creates a new ColorRange object based on the given values and adds it to this Colorizer.
        /// </summary>
        public void AddColorRange(Color color, float lowHeight, float highHeight)
        {
            AddColorRange(color, color, lowHeight, highHeight);
        }

        /// <summary>
        /// Adds the given ColorRange to this Colorizer's ColorRange list.
        /// </summary>
        public void AddColorRange(ColorRange range)
        {
            m_colorRanges.Add(range);
        }

        /// <summary>
        /// Returns the Color provided by the first ColorRange found that handles the given height value.
        /// Will return Color.Black if this Colorizer does not have a ColorRange for the provided height.
        /// </summary>
        public Color GetColor(float height)
        {
            var colorRange = m_colorRanges.FirstOrDefault(cr => cr.InRange(height));

            if (colorRange != null)
            {
                var color = colorRange.GetColor(height);
                return color;
            }

            return Color.Black;
        }

        /// <summary>
        /// Removes all ColorRanges from this Colorizer. Equivalent to creating a new Colorizer.
        /// </summary>
        public void Clear()
        {
            m_colorRanges.Clear();
        }
    }
}
