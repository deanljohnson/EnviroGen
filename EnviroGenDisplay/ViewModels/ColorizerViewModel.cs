using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace EnviroGenDisplay.ViewModels
{
    class ColorizerViewModel : ViewModelBase
    {
        public ObservableCollection<ColorRangeViewModel> ColorRanges { get; }

        public ICommand AddColorCommand { get; set; }
        public ICommand RemoveColorCommand { get; set; }
        public ICommand SetColorsCommand { get; set; }

        public IEnvironment Map { get; set; }

        public ColorizerViewModel(IEnvironment environment)
        {
            AddColorCommand = new RelayCommand(AddColor);
            RemoveColorCommand = new RelayCommand(RemoveColor);
            SetColorsCommand = new RelayCommand(SetColors);

            Map = environment;

            var colorRangeViewModels = Map.GetColorizer().BaseColorRanges
                .Select(colorRange => new ColorRangeViewModel(colorRange)).ToList();

            ColorRanges = new ObservableCollection<ColorRangeViewModel>(colorRangeViewModels);
            ColorRanges.CollectionChanged += OnColorRangesChange;
        }

        private void AddColor(object c = null)
        {
            ColorRanges.Add(new ColorRangeViewModel());
        }

        private void RemoveColor(object c = null)
        {
            if (ColorRanges.Any())
            {
                ColorRanges.RemoveAt(ColorRanges.Count - 1);
            }
        }

        private void OnColorRangesChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ColorRangeViewModel newItem in e.NewItems)
                {
                    Debug.Assert(newItem.ColorRange.GetType().IsClass, "Expected a class. Cannot accept value types");
                    //Note that changes to newItem.ColorRange will automatically 
                    //propogate to the Maps colors -> reference types
                    Map.AddColor(newItem.ColorRange);
                }
            }

            if (e.OldItems != null)
            {
                foreach (ColorRangeViewModel oldItem in e.OldItems)
                {
                    Map.RemoveColor(oldItem.ColorRange);
                }
            }
        }

        private void SetColors(object c = null)
        {
            Map.ApplyColorizer();
        }
    }
}
