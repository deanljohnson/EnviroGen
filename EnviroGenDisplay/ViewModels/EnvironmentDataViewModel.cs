using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using EnviroGenDisplay.ViewModels.Modifiers;

namespace EnviroGenDisplay.ViewModels
{
    class EnvironmentDataViewModel : ViewModelBase
    {
        private readonly EnvironmentData m_Data;

        public ObservableCollection<ModifierViewModel> Modifiers
        {
            get { return m_Data.Modifiers; }
            set { m_Data.Modifiers = value; }
        }

        public bool Combining
        {
            get { return m_Data.Combining; }
            set
            {
                if (m_Data.Combining != value)
                {
                    m_Data.Combining = value;
                    OnPropertyChanged();
                }
            }
        }

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
        public ICommand AddModifierCommand { get; set; }
        public ICommand RemoveModifierCommand { get; set; }

        public EnvironmentDataViewModel(IEnvironment map)
        {
            Map = map;
            m_Data = new EnvironmentData();
            GenerateCommand = new RelayCommand(Generate);
            AddModifierCommand = new RelayCommand(AddModifier);
            RemoveModifierCommand = new RelayCommand(RemoveModifier);
        }

        private void Generate(object n = null)
        {
            Map.GenerateTerrain(m_Data);
        }

        private void AddModifier(object m = null)
        {
            var cbi = m as ComboBoxItem;

            if (cbi == null) return;

            switch (cbi.Content.ToString())
            {
                case "Ridge":
                    Modifiers.Add(new RidgedModifierViewModel());
                    break;
                case "Scale":
                    Modifiers.Add(new ScaleModifierViewModel());
                    break;
                case "Exponent":
                    Modifiers.Add(new ExponentModifierViewModel());
                    break;
                case "Normalize":
                    Modifiers.Add(new NormalizeModifierViewModel());
                    break;
                case "Clamp":
                    Modifiers.Add(new ClampModifierViewModel());
                    break;
                case "Addition":
                    Modifiers.Add(new AddModifierViewModel());
                    break;
                case "Invert":
                    Modifiers.Add(new InvertModifierViewModel());
                    break;
            }
        }

        private void RemoveModifier(object m = null)
        {
            var index = m as int? ?? -1;

            if (index >= 0)
            {
                Modifiers.RemoveAt(index);
            }
        }
    }
}
