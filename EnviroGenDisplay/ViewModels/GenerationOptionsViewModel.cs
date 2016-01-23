using System;
using System.Windows.Input;
using EnviroGen;

namespace EnviroGenDisplay.ViewModels
{
    class GenerationOptionsViewModel : ViewModelBase
    {
        private readonly GenerationOptions m_Data;

        public int SizeX
        {
            get { return m_Data.SizeX; }
            set
            {
                if (m_Data.SizeX != value)
                {
                    m_Data.SizeX = value;
                    OnPropertyChanged();
                }
            }
        }

        public int SizeY
        {
            get { return m_Data.SizeY; }
            set
            {
                if (m_Data.SizeY != value)
                {
                    m_Data.SizeY = value;
                    OnPropertyChanged();
                }
            }
        }

        public int OctaveCount
        {
            get { return m_Data.OctaveCount; }
            set
            {
                if (m_Data.OctaveCount != value)
                {
                    m_Data.OctaveCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Seed
        {
            get { return m_Data.Seed; }
            set
            {
                if (m_Data.Seed != value)
                {
                    m_Data.Seed = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Gain
        {
            get { return m_Data.Gain; }
            set
            {
                if (Math.Abs(m_Data.Gain - value) > float.Epsilon)
                {
                    m_Data.Gain = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Frequency
        {
            get { return m_Data.Frequency; }
            set
            {
                if (Math.Abs(m_Data.Frequency - value) > float.Epsilon)
                {
                    m_Data.Frequency = value;
                    OnPropertyChanged();
                }
            }
        }

        public IEnvironment Map { get; set; }

        public ICommand GenerateCommand { get; set; }

        public GenerationOptionsViewModel(IEnvironment map)
        {
            Map = map;
            m_Data = new GenerationOptions();
            GenerateCommand = new RelayCommand(Generate);
        }

        private void Generate(object n = null)
        {
            Map.GenerateTerrain(m_Data);
        }
    }
}
