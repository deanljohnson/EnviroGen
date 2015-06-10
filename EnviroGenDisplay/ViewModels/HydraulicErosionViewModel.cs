using System;
using System.Windows.Input;
using EnviroGen.Erosion;

namespace EnviroGenDisplay.ViewModels
{
    class HydraulicErosionViewModel : ViewModelBase
    {
        private readonly HydraulicErosionData m_data;

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

        public float RainAmount
        {
            get { return m_data.RainAmount; }
            set
            {
                if (Math.Abs(m_data.RainAmount - value) > float.Epsilon)
                {
                    m_data.RainAmount = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Solubility
        {
            get { return m_data.Solubility; }
            set
            {
                if (Math.Abs(m_data.Solubility - value) > float.Epsilon)
                {
                    m_data.Solubility = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Evaporation
        {
            get { return m_data.Evaporation; }
            set
            {
                if (Math.Abs(m_data.Evaporation - value) > float.Epsilon)
                {
                    m_data.Evaporation = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Capacity
        {
            get { return m_data.Capacity; }
            set
            {
                if (Math.Abs(m_data.Capacity - value) > float.Epsilon)
                {
                    m_data.Capacity = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ErodeMapCommand { get; set; }

        public HydraulicErosionViewModel()
        {
            m_data = new HydraulicErosionData();
            ErodeMapCommand = new RelayCommand(ErodeMap);
        }

        private void ErodeMap(object n = null)
        {
            EnvironmentDisplay.ErodeHeightMap(m_data);
        }
    }
}
