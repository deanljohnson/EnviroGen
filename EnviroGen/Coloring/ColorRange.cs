using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace EnviroGen.Coloring
{
    public class ColorRange
    {
        public Color LowColor { get; set; }
        public Color HighColor { get; set; }
        public float LowHeight { get; set; }
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

        public bool InRange(float height)
        {
            return height > LowHeight && height < HighHeight;
        }

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
