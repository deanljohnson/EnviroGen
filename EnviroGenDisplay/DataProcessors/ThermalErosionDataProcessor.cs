using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using EnviroGen.Erosion;
using EnviroGenDisplay.Annotations;

namespace EnviroGenDisplay.DataProcessors
{
    public sealed class ThermalErosionDataProcessor : INotifyPropertyChanged
    {
        public ThermalErosionData Data { get; private set; }

        public string TalusAngle
        {
            get { return Data.TalusAngle.ToString(CultureInfo.CurrentCulture); }
            set
            {
                if (Data.TalusAngle.ToString(CultureInfo.CurrentCulture) != value)
                {
                    Data.TalusAngle = float.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string Iterations
        {
            get { return Data.Iterations.ToString(); }
            set
            {
                if (Data.Iterations.ToString() != value)
                {
                    Data.Iterations = Int32.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public ThermalErosionDataProcessor()
        {
            Data = new ThermalErosionData
            {
                Iterations = 50, TalusAngle = .02f
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
