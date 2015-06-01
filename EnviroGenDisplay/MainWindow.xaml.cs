using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

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

            EnvironmentDisplay.RefreshColorMappingHeights(ed);
        }

        private void SetSeaColor(object sender, RoutedPropertyChangedEventArgs<Color> routedPropertyChangedEventArgs)
        {
            var ed = Grid.FindResource("EnvironmentData") as EnvironmentData;

            if (ed == null)
            {
                throw new ArgumentNullException();
            }

            ed.SeaColor = routedPropertyChangedEventArgs.NewValue;
        }

        private void SetSandColor(object sender, RoutedPropertyChangedEventArgs<Color> routedPropertyChangedEventArgs)
        {
            var ed = Grid.FindResource("EnvironmentData") as EnvironmentData;

            if (ed == null)
            {
                throw new ArgumentNullException();
            }

            ed.SandColor = routedPropertyChangedEventArgs.NewValue;
        }

        private void SetForestColor(object sender, RoutedPropertyChangedEventArgs<Color> routedPropertyChangedEventArgs)
        {
            var ed = Grid.FindResource("EnvironmentData") as EnvironmentData;

            if (ed == null)
            {
                throw new ArgumentNullException();
            }

            ed.ForestColor = routedPropertyChangedEventArgs.NewValue;
        }

        private void SetMountainColor(object sender, RoutedPropertyChangedEventArgs<Color> routedPropertyChangedEventArgs)
        {
            var ed = Grid.FindResource("EnvironmentData") as EnvironmentData;

            if (ed == null)
            {
                throw new ArgumentNullException();
            }

            ed.MountainColor = routedPropertyChangedEventArgs.NewValue;
        }
    }
}
