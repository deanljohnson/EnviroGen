using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace EnviroGen
{
    public class Image : IEnumerable<Color>
    {
        private Color[,] m_Pixels { get; }

        public uint Width => (uint)m_Pixels.GetLength(0);
        public uint Height => (uint)m_Pixels.GetLength(1);

        public Color this[uint x, uint y]
        {
            get
            {
                if (x < Width && y < Height)
                {
                    return m_Pixels[x, y];
                }

                throw new IndexOutOfRangeException();
            }
            set
            {
                if (x < Width && y < Height)
                {
                    m_Pixels[x, y] = value;
                }

                throw new IndexOutOfRangeException();
            }
        }

        public Image(uint x, uint y)
        {
            m_Pixels = new Color[x, y];
        }

        public Image(Color[,] colors)
        {
            m_Pixels = colors;
        }

        IEnumerator<Color> IEnumerable<Color>.GetEnumerator()
        {
            return m_Pixels.Cast<Color>().GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return m_Pixels.GetEnumerator();
        }
    }
}
