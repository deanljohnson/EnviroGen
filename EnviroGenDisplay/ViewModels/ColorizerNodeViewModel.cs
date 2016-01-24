using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using EnviroGen;
using EnviroGen.Coloring;
using EnviroGen.Nodes;

namespace EnviroGenDisplay.ViewModels
{
    class ColorizerNodeViewModel : NodeViewModel<ColorizerNode<Colorizer>>
    {
        public ObservableCollection<ColorRangeViewModel> ColorRanges { get; }

        public ICommand AddColorCommand { get; set; }
        public ICommand RemoveColorCommand { get; set; }

        public IEnvironment Map { get; set; }

        public ColorizerNodeViewModel(IEnvironment environment)
        {
            AddColorCommand = new RelayCommand(AddColor);
            RemoveColorCommand = new RelayCommand(RemoveColor);

            Map = environment;

            Node = new ColorizerNode<Colorizer>
            {
                Colorizer = new Colorizer(Map.GetColorizer().BaseColorRanges)
            };

            var colorRangeViewModels = Map.GetColorizer().BaseColorRanges
                .Select(colorRange => new ColorRangeViewModel(colorRange)).ToList();

            ColorRanges = new ObservableCollection<ColorRangeViewModel>(colorRangeViewModels);
            ColorRanges.CollectionChanged += OnColorRangesChange;
        }

        public override void Modify(Environment environment)
        {
            Node.Colorizer.BaseColorRanges = ColorRanges.Select(c => c.ColorRange).ToList();

            base.Modify(environment);
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
            /*if (e.NewItems != null)
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
            }*/
        }
    }
}
