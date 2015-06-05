using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using EnviroGenDisplay.DataProcessors;

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
            var ed = Grid.FindResource("EnvironmentData") as EnvironmentData;

            if (ed == null)
            {
                throw new ArgumentNullException();
            }

            EnvironmentDisplay.EnvironmentData = ed;

            if (!GenerationThread.IsAlive)
            {
                GenerationThread = new Thread(EnvironmentDisplay.GenerateHeightMap);
                GenerationThread.Start(CombineCheckBox.IsChecked);
            }
        }

        private void OnSetColoringClick(object sender, RoutedEventArgs e)
        {
            var ed = Grid.FindResource("EnvironmentData") as EnvironmentData;
            
            if (ed == null)
            {
                throw new ArgumentNullException();
            }

            EnvironmentDisplay.SetColorMapping(ed);
        }

        private void OnBuildContinentsClick(object sender, RoutedEventArgs e)
        {
            var ed = Grid.FindResource("ContinentData") as ContinentDataProcessor;

            if (ed == null)
            {
                throw new ArgumentNullException();
            }

            EnvironmentDisplay.BuildContinents(ed.Data);
        }

        private void OnErodeClick(object sender, RoutedEventArgs e)
        {
            var ed = Grid.FindResource("ThermalErosionData") as ThermalErosionDataProcessor;

            if (ed == null)
            {
                throw new ArgumentNullException();
            }

            EnvironmentDisplay.ErodeHeightMap(ed.Data);
        }
    }
}
