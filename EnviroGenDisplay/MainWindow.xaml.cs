using System;
using System.ComponentModel;
using System.Windows;

namespace EnviroGenDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static BackgroundWorker DisplayWorker { get; set; }

        public MainWindow()
        {
            DisplayWorker = new BackgroundWorker();
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
            
            EnvironmentDisplay.GenerateFromData(ed);
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
