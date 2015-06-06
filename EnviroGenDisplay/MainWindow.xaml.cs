using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using EnviroGen.Continents;
using EnviroGen.Erosion;

namespace EnviroGenDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static BackgroundWorker DisplayWorker { get; set; }
        private static Thread GenerationThread { get; set; }

        public MainWindow()
        {
            DisplayWorker = new BackgroundWorker();
            GenerationThread = new Thread(EnvironmentDisplay.GenerateHeightMap);

            InitializeComponent();

            DisplayWorker.DoWork += EnvironmentDisplay.Update;
            DisplayWorker.RunWorkerAsync();
        }

        private void OnGenerateClick(object sender, RoutedEventArgs e)
        {
            var data = Grid.FindResource("EnvironmentData") as EnvironmentData;

            if (data == null)
            {
                throw new ArgumentNullException();
            }

            EnvironmentDisplay.EnvironmentData = data;

            if (!GenerationThread.IsAlive)
            {
                GenerationThread = new Thread(EnvironmentDisplay.GenerateHeightMap);
                GenerationThread.Start(CombineCheckBox.IsChecked);
            }
        }

        private void OnSetColoringClick(object sender, RoutedEventArgs e)
        {
            var data = Grid.FindResource("EnvironmentData") as EnvironmentData;
            
            if (data == null)
            {
                throw new ArgumentNullException();
            }

            EnvironmentDisplay.SetColorMapping(data);
        }

        private void OnBuildContinentsClick(object sender, RoutedEventArgs e)
        {
            var data = ContinentsGrid.FindResource("ContinentData") as ContinentGenerationData;

            if (data == null)
            {
                throw new ArgumentNullException();
            }

            EnvironmentDisplay.BuildContinents(data);
        }

        private void OnImprovedThermalErodeClick(object sender, RoutedEventArgs e)
        {
            var data = ImprovedThermalErosionGrid.FindResource("ThermalErosionData") as ThermalErosionData;

            if (data == null)
            {
                throw new ArgumentNullException();
            }

            EnvironmentDisplay.ErodeHeightMap(data, true);
        }

        private void OnThermalErodeClick(object sender, RoutedEventArgs e)
        {
            var data = ThermalErosionGrid.FindResource("ThermalErosionData") as ThermalErosionData;

            if (data == null)
            {
                throw new ArgumentNullException();
            }

            EnvironmentDisplay.ErodeHeightMap(data);
        }

        private void OnHydraulicErodeClick(object sender, RoutedEventArgs e)
        {
            var data = HydraulicErosionGrid.FindResource("HydraulicErosionData") as HydraulicErosionData;

            if (data == null)
            {
                throw new ArgumentNullException();
            }

            EnvironmentDisplay.ErodeHeightMap(data);
        }
    }
}
