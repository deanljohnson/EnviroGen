using System;
using System.Windows.Input;
using EnviroGen.Erosion;

namespace EnviroGenDisplay.ViewModels
{
    class ImprovedThermalErosionViewModel : ViewModelBase
    {
        private readonly ImprovedThermalEroder m_Eroder;

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

        public float TalusAngle
        {
            get { return m_Eroder.TalusAngle; }
            set
            {
                if (Math.Abs(m_Eroder.TalusAngle - value) > float.Epsilon)
                {
                    m_Eroder.TalusAngle = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ErodeMapCommand { get; set; }

        public IEnvironment Environment { get; set; }

        public ImprovedThermalErosionViewModel(IEnvironment environment)
        {
            m_Eroder = new ImprovedThermalEroder();
            ErodeMapCommand = new RelayCommand(ErodeMap);
            Environment = environment;
        }

        private void ErodeMap(object n = null)
        {
            Environment?.ErodeTerrain(m_Eroder);
        }
    }
}
