using System;
using System.Windows.Media;
using EnviroGen.Coloring;

namespace EnviroGenDisplay.ViewModels
{
    class ColorRangeViewModel : ViewModelBase
    {
        public ColorRange ColorRange { get; }

        public Color LowColor
        {
            get { return ColorRange.LowColor; }
            set
            {
                ColorRange.LowColor = value;
                OnPropertyChanged();
            }
        }

        public Color HighColor
        {
            get { return ColorRange.HighColor; }
            set
            {
                ColorRange.HighColor = value;
                OnPropertyChanged();
            }
        }

        public float LowHeight
        {
            get { return ColorRange.LowHeight; }
            set
            {
                if (Math.Abs(ColorRange.LowHeight - value) > float.Epsilon)
                {
                    ColorRange.LowHeight = value;
                    OnPropertyChanged();
                }
            }
        }

        public float HighHeight
        {
            get { return ColorRange.HighHeight; }
            set
            {
                if (Math.Abs(ColorRange.HighHeight - value) > float.Epsilon)
                {
                    ColorRange.HighHeight = value;
                    OnPropertyChanged();
                }
            }
        }

        public ColorRangeViewModel()
        {
            ColorRange = new ColorRange(Color.FromRgb(0, 0, 0), Color.FromRgb(255, 255, 255), 0f, 1f);
        }

        public ColorRangeViewModel(ColorRange colorRange)
        {
            ColorRange = colorRange;
        }
    }
}
