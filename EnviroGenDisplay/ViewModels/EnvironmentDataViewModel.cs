using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using EnviroGenDisplay.ViewModels.Modifiers;

namespace EnviroGenDisplay.ViewModels
{
    class EnvironmentDataViewModel : ViewModelBase
    {
        private readonly EnvironmentData m_data;

        public ObservableCollection<ModifierViewModel> Modifiers
        {
            get { return m_data.Modifiers; }
            set { m_data.Modifiers = value; }
        }

        public bool Combining
        {
            get { return m_data.Combining; }
            set
            {
                if (m_data.Combining != value)
                {
                    m_data.Combining = value;
                    OnPropertyChanged();
                }
            }
        }

        public int SizeX
        {
            get { return m_data.SizeX; }
            set
            {
                if (m_data.SizeX != value)
                {
                    m_data.SizeX = value;
                    OnPropertyChanged();
                }
            }
        }

        public int SizeY
        {
            get { return m_data.SizeY; }
            set
            {
                if (m_data.SizeY != value)
                {
                    m_data.SizeY = value;
                    OnPropertyChanged();
                }
            }
        }

        public int OctaveCount
        {
            get { return m_data.OctaveCount; }
            set
            {
                if (m_data.OctaveCount != value)
                {
                    m_data.OctaveCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Seed
        {
            get { return m_data.Seed; }
            set
            {
                if (m_data.Seed != value)
                {
                    m_data.Seed = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Gain
        {
            get { return m_data.Gain; }
            set
            {
                if (Math.Abs(m_data.Gain - value) > float.Epsilon)
                {
                    m_data.Gain = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Frequency
        {
            get { return m_data.Frequency; }
            set
            {
                if (Math.Abs(m_data.Frequency - value) > float.Epsilon)
                {
                    m_data.Frequency = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand GenerateCommand { get; set; }
        public ICommand AddModifierCommand { get; set; }
        public ICommand RemoveModifierCommand { get; set; }

        public EnvironmentDataViewModel()
        {
            m_data = new EnvironmentData();
            GenerateCommand = new RelayCommand(Generate);
            AddModifierCommand = new RelayCommand(AddModifier);
            RemoveModifierCommand = new RelayCommand(RemoveModifier);
        }

        private void Generate(object n = null)
        {
            EnvironmentDisplay.GenerateHeightMap(m_data);
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
            var index = m is int ? (int) m : -1;

            if (index >= 0)
            {
                Modifiers.RemoveAt(index);
            }
        }
    }
}
