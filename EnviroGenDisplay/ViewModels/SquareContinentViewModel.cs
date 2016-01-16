using System;
using System.Windows.Input;
using EnviroGen.Continents;

namespace EnviroGenDisplay.ViewModels
{
    class SquareContinentViewModel : ViewModelBase
    {
        private readonly SquareContinentGenerator m_Generator;

        public int MaximumContinentSize {
            get { return m_Generator.MaximumContinentSize; }
            set
            {
                if (m_Generator.MaximumContinentSize != value)
                {
                    m_Generator.MaximumContinentSize = value;
                    OnPropertyChanged();
                }
            }
        }

        public int MinimumContinentSize
        {
            get { return m_Generator.MinimumContinentSize; }
            set
            {
                if (m_Generator.MinimumContinentSize != value)
                {
                    m_Generator.MinimumContinentSize = value;
                    OnPropertyChanged();
                }
            }
        }

        public float ScaleAmount {
            get { return m_Generator.ScaleAmount; }
            set
            {
                if (Math.Abs(m_Generator.ScaleAmount - value) > float.Epsilon)
                {
                    m_Generator.ScaleAmount = value;
                    OnPropertyChanged();
                }
            }
        }


        public ICommand BuildContinentsCommand { get; set; }

        public IEnvironment Environment { get; set; }

        public SquareContinentViewModel(IEnvironment environment)
        {
            Environment = environment;
            m_Generator = new SquareContinentGenerator();
            BuildContinentsCommand = new RelayCommand(GenerateContinents);
        }

        private void GenerateContinents(object n = null)
        {
            Environment?.GenerateContinents(m_Generator);
        }
    }
}
