using EnviroGenDisplay.ViewModels;

namespace EnviroGenDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            SetupUI();
        }

        private void SetupUI()
        {
            var statusTracker = new StatusTrackerViewModel();
            StatusTracker.DataContext = statusTracker;

            EnvironmentTab.Content = new EnvironmentViewModel
            {
                StatusTracker = statusTracker
            };
            var environment = (IEnvironment)EnvironmentTab.Content;

            NodeEditorTab.Content = new NodeEditorViewModel(environment);
        }
    }
}
