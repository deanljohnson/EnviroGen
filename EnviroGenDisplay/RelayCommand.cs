using System;
using System.Windows.Input;

namespace EnviroGenDisplay
{
    class RelayCommand : ICommand
    {
        private readonly Action<object> m_Action;

        public RelayCommand(Action<object> action)
        {
            m_Action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            m_Action(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
