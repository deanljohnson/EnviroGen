using System;
using System.Windows.Input;
using EnviroGen.Erosion;

namespace EnviroGenDisplay.ViewModels
{
    class HydraulicErosionViewModel : ViewModelBase
    {
        private readonly HydraulicEroder m_Eroder;

        public int Iterations
        {
            get { return m_Eroder.Iterations; }
            set
            {
                if (m_Eroder.Iterations != value)
                {
                    m_Eroder.Iterations = value;
                    OnPropertyChanged();
                }
            }
        }

        public float RainAmount
        {
            get { return m_Eroder.RainAmount; }
            set
            {
                if (Math.Abs(m_Eroder.RainAmount - value) > float.Epsilon)
                {
                    m_Eroder.RainAmount = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Solubility
        {
            get { return m_Eroder.Solubility; }
            set
            {
                if (Math.Abs(m_Eroder.Solubility - value) > float.Epsilon)
                {
                    m_Eroder.Solubility = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Evaporation
        {
            get { return m_Eroder.Evaporation; }
            set
            {
                if (Math.Abs(m_Eroder.Evaporation - value) > float.Epsilon)
                {
                    m_Eroder.Evaporation = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Capacity
        {
            get { return m_Eroder.Capacity; }
            set
            {
                if (Math.Abs(m_Eroder.Capacity - value) > float.Epsilon)
                {
                    m_Eroder.Capacity = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ErodeMapCommand { get; set; }

        public IEnvironment Environment { get; set; }

        public HydraulicErosionViewModel(IEnvironment environment)
        {
            m_Eroder = new HydraulicEroder();
            ErodeMapCommand = new RelayCommand(ErodeMap);
            Environment = environment;
        }

        private void ErodeMap(object n = null)
        {
            Environment?.ErodeTerrain(m_Eroder);
        }
    }
}
