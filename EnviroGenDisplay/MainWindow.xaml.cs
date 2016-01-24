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

            EnvironmentTab.Content = new EnvironmentViewModel
            {
                StatusTracker = statusTracker
            };
            var environment = (IEnvironment)EnvironmentTab.Content;

            /*HeightMapTab.Content = new TerrainGeneratorNodeViewModel(environment);

            HydraulicErosionTab.Content = new HydraulicErosionNodeViewModel(environment);
            ThermalErosionTab.Content = new ThermalErosionNodeViewModel(environment);
            ImprovedThermalErosionTab.Content = new ImprovedThermalErosionNodeViewModel(environment);

            SquareContinentTab.Content = new SquareContinentViewModel(environment);

            ColoringTab.Content = new ColorizerViewModel(environment);

            ModifiersTab.Content = new ModifierView(environment);*/

            NodeEditorTab.Content = new NodeEditorViewModel(environment);
        }
    }
}
