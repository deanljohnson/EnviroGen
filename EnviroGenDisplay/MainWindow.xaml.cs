using System.Windows.Controls;
using EnviroGenDisplay.ViewModels;
using EnviroGenDisplay.ViewModels.Erosion;
using EnviroGenDisplay.ViewModels.Modifiers;
using EnviroGenDisplay.Views.Modifiers;

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

            HeightMapTab.Content = new GenerationOptionsViewModel(environment);

            HydraulicErosionTab.Content = new HydraulicErosionViewModel(environment);
            ThermalErosionTab.Content = new ThermalErosionViewModel(environment);
            ImprovedThermalErosionTab.Content = new ImprovedThermalErosionViewModel(environment);

            SquareContinentTab.Content = new SquareContinentViewModel(environment);

            ColoringTab.Content = new ColorizerViewModel(environment);

            ModifiersTab.Content = new ModifierView(environment);
        }
    }
}
