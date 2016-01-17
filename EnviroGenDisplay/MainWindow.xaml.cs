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
            var environment = (IEnvironment) HeightMapView.Content;


            HeightMapTab.Content = new EnvironmentDataViewModel
            {
                Map = environment
            };

            HydraulicErosionTab.Content = new HydraulicErosionViewModel(environment);
            ThermalErosionTab.Content = new ThermalErosionViewModel(environment);
            ImprovedThermalErosionTab.Content = new ImprovedThermalErosionViewModel(environment);

            SquareContinentTab.Content = new SquareContinentViewModel(environment);

            ColoringTab.Content = new ColorizerViewModel(environment);

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
