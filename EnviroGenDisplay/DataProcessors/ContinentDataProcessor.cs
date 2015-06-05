using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using EnviroGen.Continents;
using EnviroGenDisplay.Annotations;

namespace EnviroGenDisplay.DataProcessors
{
    class ContinentDataProcessor : INotifyPropertyChanged
    {
        public ContinentGenerationData Data { get; private set; }

        public string NumberOfContinents
        {
            get { return Data.NumContinents.ToString(); }
            set
            {
                if (Data.NumContinents.ToString() != value)
                {
                    Data.NumContinents = Int32.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string MinimumContinentSize
        {
            get { return Data.MinimumContinentSize.ToString(); }
            set
            {
                if (Data.MinimumContinentSize.ToString() != value)
                {
                    Data.MinimumContinentSize = Int32.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string MaximumContinentSize
        {
            get { return Data.MaximumContinentSize.ToString(); }
            set
            {
                if (Data.MaximumContinentSize.ToString() != value)
                {
                    Data.MaximumContinentSize = Int32.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string ContinentScale
        {
            get { return Data.Scale.ToString(CultureInfo.CurrentCulture); }
            set
            {
                if (Data.Scale.ToString(CultureInfo.CurrentCulture) != value)
                {
                    Data.Scale = float.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public ContinentDataProcessor()
        {
            Data = new ContinentGenerationData
            {
                NumContinents = 1,
                MinimumContinentSize = 400,
                MaximumContinentSize = 450
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
