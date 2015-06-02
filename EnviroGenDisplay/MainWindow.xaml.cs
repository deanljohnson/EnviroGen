using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;

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
            GenerationThread = new Thread(EnvironmentDisplay.GenerateFromData);

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
                GenerationThread = new Thread(EnvironmentDisplay.GenerateFromData);
                GenerationThread.Start();
            }
        }

        private void OnRefreshClick(object sender, RoutedEventArgs e)
        {
            var ed = Grid.FindResource("EnvironmentData") as EnvironmentData;
            
            if (ed == null)
            {
                throw new ArgumentNullException();
            }

            EnvironmentDisplay.RefreshColorMapping(ed);
        }
    }
}
