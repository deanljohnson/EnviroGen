using System;
using System.Windows.Media;
using EnviroGen.Coloring;

namespace EnviroGenDisplay.ViewModels
{
    class ColorRangeViewModel : ViewModelBase
    {
        private readonly ColorRange m_Range;

        public Color LowColor
        {
            get { return m_Range.LowColor; }
            set
            {
                m_Range.LowColor = value;
                OnPropertyChanged();
            }
        }

        public Color HighColor
        {
            get { return m_Range.HighColor; }
            set
            {
                m_Range.HighColor = value;
                OnPropertyChanged();
            }
        }

        public float LowHeight
        {
            get { return m_Range.LowHeight; }
            set
            {
                if (Math.Abs(m_Range.LowHeight - value) > float.Epsilon)
                {
                    m_Range.LowHeight = value;
                    OnPropertyChanged();
                }
            }
        }

        public float HighHeight
        {
            get { return m_Range.HighHeight; }
            set
            {
                if (Math.Abs(m_Range.HighHeight - value) > float.Epsilon)
                {
                    m_Range.HighHeight = value;
                    OnPropertyChanged();
                }
            }
        }

        public ColorRangeViewModel()
        {
            m_Range = new ColorRange(Color.FromRgb(255, 255, 255), Color.FromRgb(0, 0, 0), 0f, 1f);
        }

        public ColorRange GetColorRange()
        {
            return m_Range;
        }
    }
}
