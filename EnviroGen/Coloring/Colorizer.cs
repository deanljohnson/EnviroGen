using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using EnviroGen.HeightMaps;

namespace EnviroGen.Coloring
{
    /// <summary>
    /// Represents a class that can color a HeightMap object based on provided ColorRanges
    /// </summary>
    public class Colorizer : IColorizer
    {
        private static readonly Random Random = new Random();
        public List<ColorRange> BaseColorRanges { get; set; }

        public Colorizer()
        {
            BaseColorRanges = new List<ColorRange>();
        }

        public Colorizer(params ColorRange[] colorRanges)
        {
            BaseColorRanges = colorRanges.ToList();
        }

        public Colorizer(List<ColorRange> baseColorRanges)
        {
            BaseColorRanges = baseColorRanges;
        }

        public Colorizer(IEnumerable<ColorRange> colorRanges)
        {
            BaseColorRanges = colorRanges.ToList();
        }

        /// <summary>
        /// Returns an 2d Color array with coloring applied based on the given HeightMap
        /// </summary>
        public Color[,] Colorize(HeightMap map)
        {
            var img = new Color[map.Size.X, map.Size.Y];

            for (var y = 0; y < map.Size.Y; y++)
            {
                for (var x = 0; x < map.Size.X; x++)
                {
                    img[x, y] = GetBaseColor(map[x, y], true);
                }
            }

            return img;
        }

        /// <summary>
        /// Creates a new ColorRange object based on the given values and adds it to this Colorizer.
        /// </summary>
        public void AddColorRange(Color lowColor, Color highColor, float lowHeight, float highHeight)
        {
            BaseColorRanges.Add(new ColorRange(lowColor, highColor, lowHeight, highHeight));
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
            BaseColorRanges.Add(range);
        }

        /// <summary>
        /// Removes the given ColorRange to this Colorizer's ColorRange list.
        /// </summary>
        public void RemoveColorRange(ColorRange range)
        {
            BaseColorRanges.Remove(range);
        }

        /// <summary>
        /// Returns the Color provided by the first ColorRange found that handles the given height value.
        /// Will return Color.Black if this Colorizer does not have a ColorRange for the provided height.
        /// </summary>
        public Color GetBaseColor(float height, bool allowOverlap = false)
        {
            var colorRanges = BaseColorRanges.Where(cr => cr.InRange(height)).ToList();

            Color color;

            if (allowOverlap && colorRanges.Count > 1)
            {
                var i = Random.Next(colorRanges.Count);
                color = colorRanges[i].GetColor(height);
            }
            else if (colorRanges.Count > 0)
            {
                color = colorRanges[0].GetColor(height);
            }
            else
            {
                color = Color.FromRgb(0, 0, 0);
            }

            return color;
        }
    }
}
