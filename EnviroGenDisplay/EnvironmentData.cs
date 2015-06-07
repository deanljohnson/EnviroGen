using System.ComponentModel;
using System.Runtime.CompilerServices;
using EnviroGen;
using EnviroGenDisplay.Annotations;

namespace EnviroGenDisplay
{
    public class EnvironmentData : INotifyPropertyChanged
    {
        public GenerationOptions GenOptions { get; private set; }
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
            GenOptions = new GenerationOptions();
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
