using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EnviroGen.Continents;
using EnviroGenDisplay.ViewModels;
using Xceed.Wpf.AvalonDock.Controls;

namespace EnviroGenDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static BackgroundWorker DisplayWorker { get; set; }
        private static BackgroundWorker GenerateAllWorker { get; set; }
        private static BackgroundWorker GenerationWorker { get; set; }

        public MainWindow()
        {
            DisplayWorker = new BackgroundWorker();
            GenerationWorker = new BackgroundWorker();
            GenerateAllWorker = new BackgroundWorker();

            InitializeComponent();

            SetupUI();

            //AddMap();
            
            DisplayWorker.DoWork += EnvironmentDisplay.Update;
            DisplayWorker.RunWorkerAsync();
            GenerationWorker.DoWork += EnvironmentDisplay.GenerateHeightMap;
            GenerateAllWorker.DoWork += EnvironmentDisplay.GenerateHeightMap;
        }

        private void SetupUI()
        {
            HeightMapTab.Content = new EnvironmentDataViewModel();
            HydraulicErosionTab.Content = new HydraulicErosionViewModel();
            ThermalErosionTab.Content = new ThermalErosionViewModel();
            ImprovedThermalErosionTab.Content = new ImprovedThermalErosionViewModel();
            ColoringTab.Content = new ColorizerViewModel();
        }

        private void OnGenerateAllClick(object sender, RoutedEventArgs e)
        {
            var button = sender as FrameworkElement;

            if (button == null) return;

            var contentControls = button.Parent.FindLogicalChildren<ContentControl>().Where(cc => cc.Content is EnvironmentData);
            var objects = contentControls.Select(cc => cc.Content as EnvironmentData);

            if (!GenerateAllWorker.IsBusy)
            {
                GenerateAllWorker.RunWorkerAsync(objects);
            }
        }

        private void OnBuildContinentsClick(object sender, RoutedEventArgs e)
        {
            var data = ((FrameworkElement)sender).DataContext as ContinentGenerationData;

            if (data == null)
            {
                throw new ArgumentNullException();
            }

            EnvironmentDisplay.BuildContinents(data);
        }
    }
}
