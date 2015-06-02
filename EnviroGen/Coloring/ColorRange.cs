using SFML.Graphics;

namespace EnviroGen.Coloring
{
    public class ColorRange
    {
        /// <summary>
        /// The Color a pixel should be at the lowest height in this ColorRange.
        /// </summary>
        public Color LowColor { get; set; }
        /// <summary>
        /// The Color a pixel should be at the highest height in this ColorRange.
        /// </summary>
        public Color HighColor { get; set; }
        /// <summary>
        /// The lowest height that this ColorRange handles.
        /// </summary>
        public float LowHeight { get; set; }
        /// <summary>
        /// The highest height that this ColorRange handles.
        /// </summary>
        public float HighHeight { get; set; }

        public ColorRange(Color lowColor, Color highColor, float lowHeight, float highHeight)
        {
            LowColor = lowColor;
            HighColor = highColor;
            LowHeight = lowHeight;
            HighHeight = highHeight;
        }

        public ColorRange(Color color, float lowHeight, float highHeight)
            : this(color, color, lowHeight, highHeight)
        {
        }

        /// <summary>
        /// Returns whether or not the given height is within this ColorRange's range.
        /// </summary>
        public bool InRange(float height)
        {
            return height >= LowHeight && height <= HighHeight;
        }

        /// <summary>
        /// Returns a Color based on a given height value. The given height should be within this ColorRange's range.
        /// </summary>
        public Color GetColor(float height)
        {
            var heightRange = HighHeight - LowHeight;

            height -= LowHeight;

            var heightRatio = height / heightRange;

            var r = (byte)(((HighColor.R - LowColor.R) * heightRatio) + (LowColor.R));
            var g = (byte)(((HighColor.G - LowColor.G) * heightRatio) + (LowColor.G));
            var b = (byte)(((HighColor.B - LowColor.B) * heightRatio) + (LowColor.B));
            var a = (byte)(((HighColor.A - LowColor.A) * heightRatio) + (LowColor.A));

            return new Color(r, g, b, a);
        }
    }
}
