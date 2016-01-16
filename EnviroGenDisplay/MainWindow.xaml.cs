using EnviroGenDisplay.ViewModels;

namespace EnviroGenDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        //private static BackgroundWorker DisplayWorker { get; set; }
        //private static BackgroundWorker GenerateAllWorker { get; set; }
        //private static BackgroundWorker GenerationWorker { get; set; }

        public MainWindow()
        {
            //DisplayWorker = new BackgroundWorker();
            //GenerationWorker = new BackgroundWorker();
            //GenerateAllWorker = new BackgroundWorker();

            InitializeComponent();

            SetupUI();

            ////AddMap();
            
            //DisplayWorker.DoWork += EnvironmentDisplay.Update;
            //DisplayWorker.RunWorkerAsync();
            //GenerationWorker.DoWork += EnvironmentDisplay.GenerateHeightMap;
            //GenerateAllWorker.DoWork += EnvironmentDisplay.GenerateHeightMap;
        }

        private void SetupUI()
        {
            HeightMapView.Content = new EnvironmentViewModel();
            HeightMapTab.Content = new EnvironmentDataViewModel
            {
                Map = (IEnvironment) HeightMapView.Content
            };

            HydraulicErosionTab.Content = new HydraulicErosionViewModel((IEnvironment)HeightMapView.Content);
            ThermalErosionTab.Content = new ThermalErosionViewModel((IEnvironment)HeightMapView.Content);
            ImprovedThermalErosionTab.Content = new ImprovedThermalErosionViewModel((IEnvironment)HeightMapView.Content);

            SquareContinentTab.Content = new SquareContinentViewModel((IEnvironment)HeightMapView.Content);

            ColoringTab.Content = new ColorizerViewModel
            {
                Map = (IEnvironment)HeightMapView.Content
            };

        }

        //private void OnGenerateAllClick(object sender, RoutedEventArgs e)
        //{
        //    var button = sender as FrameworkElement;

        //    if (button == null) return;

        //    var contentControls = button.Parent.FindLogicalChildren<ContentControl>().Where(cc => cc.Content is EnvironmentData);
        //    var objects = contentControls.Select(cc => cc.Content as EnvironmentData);

        //    if (!GenerateAllWorker.IsBusy)
        //    {
        //        GenerateAllWorker.RunWorkerAsync(objects);
        //    }
        //}
    }
}
