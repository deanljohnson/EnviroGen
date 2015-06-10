using System;
using System.Windows.Input;
using EnviroGen.Erosion;

namespace EnviroGenDisplay.ViewModels
{
    class ImprovedThermalErosionViewModel : ViewModelBase
    {
        private readonly ThermalErosionData m_data;

        public int Iterations
        {
            get { return m_data.Iterations; }
            set
            {
                if (m_data.Iterations != value)
                {
                    m_data.Iterations = value;
                    OnPropertyChanged();
                }
            }
        }

        public float TalusAngle
        {
            get { return m_data.TalusAngle; }
            set
            {
                if (Math.Abs(m_data.TalusAngle - value) > float.Epsilon)
                {
                    m_data.TalusAngle = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ErodeMapCommand { get; set; }

        public ImprovedThermalErosionViewModel()
        {
            m_data = new ThermalErosionData();
            ErodeMapCommand = new RelayCommand(ErodeMap);
        }

        private void ErodeMap(object n = null)
        {
            EnvironmentDisplay.ErodeHeightMap(m_data, true);
        }
    }
}
