using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EnviroGen;
using EnviroGen.Noise.Modifiers;
using EnviroGenDisplay.Annotations;

namespace EnviroGenDisplay
{
    public class EnvironmentData : GenerationOptions, INotifyPropertyChanged
    {
        public new ObservableCollection<IModifier> Modifiers { get; set; }
        private bool m_combining;

        public bool Combining
        {
            get { return m_combining; }
            set
            {
                m_combining = value; 
                OnPropertyChanged();
            }
        }

        public EnvironmentData()
        {
            Modifiers = new ObservableCollection<IModifier>();
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
