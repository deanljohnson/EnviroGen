using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EnviroGen.Coloring;
using EnviroGen.Continents;
using EnviroGen.Erosion;
using EnviroGen.Noise.Modifiers;
using SFML.Graphics;
using Xceed.Wpf.AvalonDock.Controls;

namespace EnviroGenDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static BackgroundWorker DisplayWorker { get; set; }
        private int AddedHeightMapCount { get; set; }
        private int AddedColorRangeCount { get; set; }

        public MainWindow()
        {
            DisplayWorker = new BackgroundWorker();

            InitializeComponent();

            AddDefaultColors();

            AddedHeightMapCount = -1;
            AddMap();
            
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

            EnvironmentDisplay.GenerateHeightMap();
        }

        private void OnSetColoringClick(object sender, RoutedEventArgs e)
        {
            //Voodoo magic, gets all ColorRange objects from the ColorGrid xml control.
            var ranges = (from object elem in ColorGrid.Children select elem as ContentControl).Select(contentControl => contentControl.Content).OfType<ColorRange>().ToList();

            EnvironmentDisplay.SetColorMapping(ranges);
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
            AddMap();
        }

        private void AddMap()
        {
            AddedHeightMapCount++;
            var newHeightMapFields = new ContentControl { Content = new EnvironmentData() };
            Grid.SetColumn(newHeightMapFields, AddedHeightMapCount);

            HeightMapGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            HeightMapGrid.Children.Add(newHeightMapFields);
        }

        private void OnRemoveMapClick(object sender, RoutedEventArgs e)
        {
            RemoveMap();
        }

        private void RemoveMap()
        {
            if (AddedHeightMapCount > 0)
            {
                HeightMapGrid.Children.RemoveAt(HeightMapGrid.Children.Count - 1);
                AddedHeightMapCount--;
            }
        }

        private void AddDefaultColors()
        {
            AddedColorRangeCount++;
            var waterField = new ContentControl 
            {
                    Content = new ColorRange(new Color(0, 0, 116, 255), new Color(0, 0, 255, 255), .0f, .45f) 
            };
            Grid.SetRow(waterField, AddedColorRangeCount);
            AddedColorRangeCount++;
            var sandField = new ContentControl
            {
                Content = new ColorRange(new Color(170, 166, 27, 255), new Color(206, 202, 49, 255), .45f, .5f)
            };
            Grid.SetRow(sandField, AddedColorRangeCount);
            AddedColorRangeCount++;
            var forestField = new ContentControl
            {
                Content = new ColorRange(new Color(0, 159, 21, 255), new Color(22, 88, 31, 255), .5f, .75f)
            };
            Grid.SetRow(forestField, AddedColorRangeCount);
            AddedColorRangeCount++;
            var mountainField = new ContentControl
            {
                Content = new ColorRange(new Color(197, 197, 202, 255), new Color(248, 248, 248, 255), .75f, 1f)
            };
            Grid.SetRow(mountainField, AddedColorRangeCount);

            ColorGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            ColorGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            ColorGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            ColorGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            ColorGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            ColorGrid.Children.Add(waterField);
            ColorGrid.Children.Add(sandField);
            ColorGrid.Children.Add(forestField);
            ColorGrid.Children.Add(mountainField);
        }

        private void OnAddColorRangeClick(object sender, RoutedEventArgs e)
        {
            AddColorRange();
        }

        private void AddColorRange()
        {
            AddedColorRangeCount++;

            var newRange = new ContentControl
            {
                Content = new ColorRange(new Color(0, 0, 0, 255), new Color(255, 255, 255, 255), 1f, 1f)
            };
            Grid.SetRow(newRange, AddedColorRangeCount);

            ColorGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto } );

            ColorGrid.Children.Add(newRange);
        }

        private void OnRemoveColorRangeClick(object sender, RoutedEventArgs e)
        {
            RemoveColorRange(((FrameworkElement)sender).DataContext as ColorRange);
        }

        private void RemoveColorRange(ColorRange range)
        {
            for (var i = 0; i < ColorGrid.Children.Count; i++)
            {
                var child = ColorGrid.Children[i];
                if ((((ContentControl) child).Content as ColorRange) == range)
                {
                    ColorGrid.Children.Remove(child);
                }
            }
        }

        private void AddModifierClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var typeBox = button.Parent.FindLogicalChildren<ComboBox>().First(cb => cb.Name == "ModifierTypeBox").SelectedItem as ComboBoxItem;
                var data = button.DataContext as EnvironmentData;

                if (data == null || typeBox == null) return;

                switch (typeBox.Content.ToString())
                {
                    case "Ridge":
                        data.Modifiers.Add(new RidgedModifier());
                        break;
                    case "Scale":
                        data.Modifiers.Add(new ScaleModifier(1f));
                        break;
                    case "Exponent":
                        data.Modifiers.Add(new ExponentModifier(1f));
                        break;
                    case "Normalize":
                        data.Modifiers.Add(new NormalizeModifier(0f, 1f));
                        break;
                    case "Clamp":
                        data.Modifiers.Add(new ClampModifier(0f, 1f));
                        break;
                }
            }
        }

        private void RemoveModifierClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button == null) return;

            var listBox = button.Parent.FindLogicalChildren<ListBox>().First(lb => lb.Name == "ModifiersListBox");

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < listBox.SelectedItems.Count; i++)
            {
                var selectedItem = (IModifier) listBox.SelectedItems[i];
                ((ObservableCollection<IModifier>) listBox.ItemsSource).Remove(selectedItem);
            }
        }
    }
}
