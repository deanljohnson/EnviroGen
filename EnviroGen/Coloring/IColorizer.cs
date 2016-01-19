using System.Collections.Generic;
using System.Windows.Media;
using EnviroGen.HeightMaps;

namespace EnviroGen.Coloring
{
    /// <summary>
    /// Represents a simple colorizer that color's a HeightMap only based on height
    /// </summary>
    public interface IColorizer
    {
        /// <summary>
        /// The base color to be applied based on a certain height on a HeightMap
        /// </summary>
        List<ColorRange> BaseColorRanges { get; set; }

        /// <summary>
        /// Returns an array of Color's representing the color for each location in a HeightMap
        /// </summary>
        Color[,] Colorize(HeightMap map);

        /// <summary>
        /// Return the base Color for the given height. If overlap is allowed, 
        /// a random color will be selected from those that apply to the given height
        /// </summary>
        Color GetBaseColor(float height, bool allowOverlap = false);
    }
}