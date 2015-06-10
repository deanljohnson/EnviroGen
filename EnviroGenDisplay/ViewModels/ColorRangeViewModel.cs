using System;
using EnviroGen.Coloring;
using SFML.Graphics;

namespace EnviroGenDisplay.ViewModels
{
    class ColorRangeViewModel : ViewModelBase
    {
        private readonly ColorRange m_range;

        public Color LowColor
        {
            get { return m_range.LowColor; }
            set
            {
                m_range.LowColor = value;
                OnPropertyChanged();
            }
        }

        public Color HighColor
        {
            get { return m_range.HighColor; }
            set
            {
                m_range.HighColor = value;
                OnPropertyChanged();
            }
        }

        public float LowHeight
        {
            get { return m_range.LowHeight; }
            set
            {
                if (Math.Abs(m_range.LowHeight - value) > float.Epsilon)
                {
                    m_range.LowHeight = value;
                    OnPropertyChanged();
                }
            }
        }

        public float HighHeight
        {
            get { return m_range.HighHeight; }
            set
            {
                if (Math.Abs(m_range.HighHeight - value) > float.Epsilon)
                {
                    m_range.HighHeight = value;
                    OnPropertyChanged();
                }
            }
        }

        public ColorRangeViewModel()
        {
            m_range = new ColorRange(Color.White, Color.Black, 0f, 1f);
        }

        public ColorRange GetColorRange()
        {
            return m_range;
        }
    }
}
