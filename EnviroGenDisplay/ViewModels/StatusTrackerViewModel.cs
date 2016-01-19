using System.Collections.Generic;

namespace EnviroGenDisplay.ViewModels
{
    internal class StatusTrackerViewModel : ViewModelBase, IStatusTracker
    {
        private Stack<string> m_Messages { get; } = new Stack<string>();

        public string CurrentMessage => m_Messages.Peek();

        public void PushMessage(string msg)
        {
            lock (m_Messages)
            {
                m_Messages.Push(msg);
                OnPropertyChanged(nameof(CurrentMessage));
            }
        }

        public void PopMessage()
        {
            lock (m_Messages)
            {
                m_Messages.Pop();
                OnPropertyChanged(nameof(CurrentMessage));
            }
        }
    }
}
