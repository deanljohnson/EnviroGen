using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using EnviroGenDisplay.ViewModels;
using EnviroGenNodeEditor;

namespace EnviroGenDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static MainWindow Instance { get; private set; }

        private List<string> m_StatusMessages { get; } = new List<string>();
        private List<string> m_ContextMessages { get; } = new List<string>();

        public string CurrentContextInfoMessage => m_ContextMessages.Count > 0 ? m_ContextMessages.Last() : "";
        public string CurrentStatusMessage => m_StatusMessages.Count > 0 ? m_StatusMessages.Last() : "Ready";

        public MainWindow()
        {
            InitializeComponent();

            SetupUI();

            Instance = this;
            StatusTextBlock.DataContext = this;
        }

        public void SetStatusTextSafe(string text)
        {
            m_StatusMessages.Add(text);

            StatusTextBlock.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                    new Action(delegate{ StatusTextBlock.Text = CurrentStatusMessage; }
            ));
        }

        public void RemoveStatusTextSafe(string text)
        {
            m_StatusMessages.Remove(text);

            StatusTextBlock.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                    new Action(delegate { StatusTextBlock.Text = CurrentStatusMessage; }
            ));
        }

        public void SetContextInfoTextSafe(string text)
        {
            m_ContextMessages.Add(text);

            ContextInfoTextBlock.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                    new Action(delegate { ContextInfoTextBlock.Text = CurrentContextInfoMessage; }
            ));
        }

        public void RemoveContextInfoTextSafe(string text)
        {
            m_ContextMessages.Remove(text);

            ContextInfoTextBlock.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                    new Action(delegate { ContextInfoTextBlock.Text = CurrentContextInfoMessage; }
            ));
        }

        private void SetupUI()
        {
            var enviroVm = new EnvironmentViewModel();
            EnvironmentTab.Content = enviroVm;
            App.WorkingEnvironment = enviroVm;

            NodeEditorTab.Content = 
                new NodeEditorViewModel(
                    new NodeEditor<NodeViewModel,
                                    ObservableCollection<NodeViewModel>,
                                    NodeConnectionViewModel,
                                    ObservableCollection<NodeConnectionViewModel>>());
        }
    }
}
