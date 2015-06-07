using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
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
        private int AddedHeightMapCount { get; set; }

        public MainWindow()
        {
            DisplayWorker = new BackgroundWorker();
            GenerationThread = new Thread(EnvironmentDisplay.GenerateHeightMap);

            InitializeComponent();

            AddedHeightMapCount = 0;

            DisplayWorker.DoWork += EnvironmentDisplay.Update;
            DisplayWorker.RunWorkerAsync();
        }

        private void OnGenerateClick(object sender, RoutedEventArgs e)
        {
            var data = ((FrameworkElement)sender).DataContext as EnvironmentData;

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
            var data = ((FrameworkElement)sender).DataContext as EnvironmentData;
            
            if (data == null)
            {
                throw new ArgumentNullException();
            }

            EnvironmentDisplay.SetColorMapping(data);
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

        private void OnImprovedThermalErodeClick(object sender, RoutedEventArgs e)
        {
            var data = ((FrameworkElement)sender).DataContext as ThermalErosionData;

            if (data == null)
            {
                throw new ArgumentNullException();
            }

            EnvironmentDisplay.ErodeHeightMap(data, true);
        }

        private void OnThermalErodeClick(object sender, RoutedEventArgs e)
        {
            var data = ((FrameworkElement)sender).DataContext as ThermalErosionData;

            if (data == null)
            {
                throw new ArgumentNullException();
            }

            EnvironmentDisplay.ErodeHeightMap(data);
        }

        private void OnHydraulicErodeClick(object sender, RoutedEventArgs e)
        {
            var data = ((FrameworkElement)sender).DataContext as HydraulicErosionData;

            if (data == null)
            {
                throw new ArgumentNullException();
            }

            EnvironmentDisplay.ErodeHeightMap(data);
        }

        private void OnAddMapClick(object sender, RoutedEventArgs e)
        {
            var data = new EnvironmentData();
            data.GenOptions.SizeX = ((EnvironmentData)(GlobalGrid.FindResource("EnvironmentData"))).GenOptions.SizeX;
            data.GenOptions.SizeY = ((EnvironmentData)(GlobalGrid.FindResource("EnvironmentData"))).GenOptions.SizeY;
            var intConverter = (IValueConverter) GlobalGrid.FindResource("IntToStringConverter");
            var floatConverter = (IValueConverter) GlobalGrid.FindResource("FloatToStringConverter");
            var heightOctaveBinding = new Binding("GenOptions.HeightMapOctaveCount") { Source = data, Converter = intConverter };
            var cloudOctaveBinding = new Binding("GenOptions.CloudMapOctaveCount") { Source = data, Converter = intConverter };
            var heightMapSeedBinding = new Binding("GenOptions.HeightMapSeed") { Source = data, Converter = intConverter };
            var cloudMapSeedBinding = new Binding("GenOptions.CloudMapSeed") { Source = data, Converter = intConverter };
            var noiseRoughnessBinding = new Binding("GenOptions.NoiseRoughness") { Source = data, Converter = floatConverter };
            var noiseFrequencyBinding = new Binding("GenOptions.NoiseFrequency") { Source = data, Converter = floatConverter };

            AddedHeightMapCount++;

            var column = AddedHeightMapCount * 2;

            HeightMapGrid.ColumnDefinitions.Add(new ColumnDefinition{ Width = GridLength.Auto });
            HeightMapGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            var heightOctaveCountLabel = new Label { Content = "Height Map Octave Count:" };
            Grid.SetColumn(heightOctaveCountLabel, column);
            Grid.SetRow(heightOctaveCountLabel, 2);
            var cloudOctavecountLabel = new Label { Content = "Cloud Map Octave Count:" };
            Grid.SetColumn(cloudOctavecountLabel, column);
            Grid.SetRow(cloudOctavecountLabel, 3);
            var heightMapSeedLabel = new Label { Content = "Height Map Seed:" };
            Grid.SetColumn(heightMapSeedLabel, column);
            Grid.SetRow(heightMapSeedLabel, 4);
            var cloudMapSeedLabel = new Label { Content = "Cloud Map Seed:" };
            Grid.SetColumn(cloudMapSeedLabel, column);
            Grid.SetRow(cloudMapSeedLabel, 5);
            var noiseRoughnessLabel = new Label { Content = "Noise Roughness:" };
            Grid.SetColumn(noiseRoughnessLabel, column);
            Grid.SetRow(noiseRoughnessLabel, 6);
            var noiseFrequencyLabel = new Label { Content = "Noise Frequency:" };
            Grid.SetColumn(noiseFrequencyLabel, column);
            Grid.SetRow(noiseFrequencyLabel, 7);
            var generateButton = new Button { Content = "Generate", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Width = 75};
            generateButton.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(OnGenerateClick));
            generateButton.DataContext = data;
            Grid.SetColumn(generateButton, column);
            Grid.SetRow(generateButton, 8);
            var heightOctaveCountTextBox = new TextBox { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Center, Width = 40, Height = double.NaN, TextWrapping = TextWrapping.NoWrap };
            heightOctaveCountTextBox.SetBinding(TextBox.TextProperty, heightOctaveBinding);
            Grid.SetColumn(heightOctaveCountTextBox, column + 1);
            Grid.SetRow(heightOctaveCountTextBox, 2);
            var cloudOctaveCountTextBox = new TextBox { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Center, Width = 40, Height = double.NaN, TextWrapping = TextWrapping.NoWrap };
            cloudOctaveCountTextBox.SetBinding(TextBox.TextProperty, cloudOctaveBinding);
            Grid.SetColumn(cloudOctaveCountTextBox, column + 1);
            Grid.SetRow(cloudOctaveCountTextBox, 3);
            var heightMapSeedTextBox = new TextBox { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Center, Width = 40, Height = double.NaN, TextWrapping = TextWrapping.NoWrap };
            heightMapSeedTextBox.SetBinding(TextBox.TextProperty, heightMapSeedBinding);
            Grid.SetColumn(heightMapSeedTextBox, column + 1);
            Grid.SetRow(heightMapSeedTextBox, 4);
            var cloudMapSeedTextBox = new TextBox { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Center, Width = 40, Height = double.NaN, TextWrapping = TextWrapping.NoWrap };
            cloudMapSeedTextBox.SetBinding(TextBox.TextProperty, cloudMapSeedBinding);
            Grid.SetColumn(cloudMapSeedTextBox, column + 1);
            Grid.SetRow(cloudMapSeedTextBox, 5);
            var noiseRoughnessTextBox = new TextBox { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Center, Width = 40, Height = double.NaN, TextWrapping = TextWrapping.NoWrap };
            noiseRoughnessTextBox.SetBinding(TextBox.TextProperty, noiseRoughnessBinding);
            Grid.SetColumn(noiseRoughnessTextBox, column + 1);
            Grid.SetRow(noiseRoughnessTextBox, 6);
            var noiseFrequencyTextBox = new TextBox { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Center, Width = 40, Height = double.NaN, TextWrapping = TextWrapping.NoWrap };
            noiseFrequencyTextBox.SetBinding(TextBox.TextProperty, noiseFrequencyBinding);
            Grid.SetColumn(noiseFrequencyTextBox, column + 1);
            Grid.SetRow(noiseFrequencyTextBox, 7);
            var minusButton = new Button { Content = "-", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Width = 20};
            minusButton.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(OnRemoveMapClick));
            Grid.SetColumn(minusButton, column + 1);
            Grid.SetRow(minusButton, 8);

            HeightMapGrid.Children.Add(heightOctaveCountLabel);
            HeightMapGrid.Children.Add(cloudOctavecountLabel);
            HeightMapGrid.Children.Add(heightMapSeedLabel);
            HeightMapGrid.Children.Add(cloudMapSeedLabel);
            HeightMapGrid.Children.Add(noiseRoughnessLabel);
            HeightMapGrid.Children.Add(noiseFrequencyLabel);
            HeightMapGrid.Children.Add(generateButton);
            HeightMapGrid.Children.Add(heightOctaveCountTextBox);
            HeightMapGrid.Children.Add(cloudOctaveCountTextBox);
            HeightMapGrid.Children.Add(heightMapSeedTextBox);
            HeightMapGrid.Children.Add(cloudMapSeedTextBox);
            HeightMapGrid.Children.Add(noiseRoughnessTextBox);
            HeightMapGrid.Children.Add(noiseFrequencyTextBox);
            HeightMapGrid.Children.Add(minusButton);
        }

        private void OnRemoveMapClick(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < HeightMapGrid.Children.Count; i++)
            {
                var elem = HeightMapGrid.Children[i];

                if (Grid.GetColumn(elem) == (AddedHeightMapCount * 2) ||
                    Grid.GetColumn(elem) == (AddedHeightMapCount * 2) + 1)
                {
                    HeightMapGrid.Children.Remove(elem);
                    i--;
                }
            }

            AddedHeightMapCount--;
        }
    }
}
