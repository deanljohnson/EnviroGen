using EnviroGenDisplay.ViewModels;
using EnviroGenDisplay.ViewModels.Erosion;

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

            MapView.Content = new EnvironmentViewModel
            {
                StatusTracker = statusTracker
            };
            var environment = (IEnvironment) MapView.Content;

            HeightMapTab.Content = new EnvironmentDataViewModel(environment);

            HydraulicErosionTab.Content = new HydraulicErosionViewModel(environment);
            ThermalErosionTab.Content = new ThermalErosionViewModel(environment);
            ImprovedThermalErosionTab.Content = new ImprovedThermalErosionViewModel(environment);

            SquareContinentTab.Content = new SquareContinentViewModel(environment);

            ColoringTab.Content = new ColorizerViewModel(environment);
        }
    }
}
