using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using EnviroGen;
using EnviroGen.Coloring;
using EnviroGen.Nodes;

namespace EnviroGenDisplay.ViewModels
{
    [EditorNodeName("Colorizer", Category = App.ColoringCategory)]
    class ColorizerNodeViewModel : NodeViewModel<ColorizerNode<Colorizer>>
    {
        public ObservableCollection<ColorRangeViewModel> ColorRanges { get; }

        public ICommand AddColorCommand { get; set; }
        public ICommand RemoveColorCommand { get; set; }

        static ColorizerNodeViewModel()
        {
            Name = "Colorizer";
        }

        public ColorizerNodeViewModel()
            : base("Coloring")
        {
            AddColorCommand = new RelayCommand(AddColor);
            RemoveColorCommand = new RelayCommand(RemoveColor);

            Node = new ColorizerNode<Colorizer>
            {
                Colorizer = new Colorizer(Terrain.DefaultColorizer.BaseColorRanges)
            };

            var colorRangeViewModels = Terrain.DefaultColorizer.BaseColorRanges
                .Select(colorRange => new ColorRangeViewModel(colorRange)).ToList();

            ColorRanges = new ObservableCollection<ColorRangeViewModel>(colorRangeViewModels);
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
    }
}
