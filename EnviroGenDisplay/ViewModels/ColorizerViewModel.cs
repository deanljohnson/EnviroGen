﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using EnviroGen.Coloring;

namespace EnviroGenDisplay.ViewModels
{
    class ColorizerViewModel : ViewModelBase
    {
        public ObservableCollection<ColorRangeViewModel> ColorRanges { get; }

        public ICommand AddColorCommand { get; set; }
        public ICommand RemoveColorCommand { get; set; }
        public ICommand SetColorsCommand { get; set; }

        public IEnvironment Map { get; set; }

        public ColorizerViewModel()
        {
            ColorRanges = new ObservableCollection<ColorRangeViewModel>();
            AddColorCommand = new RelayCommand(AddColor);
            RemoveColorCommand = new RelayCommand(RemoveColor);
            SetColorsCommand = new RelayCommand(SetColors);
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

        private void SetColors(object c = null)
        {
            var colorRanges = ColorRanges.Select(cr => cr.GetColorRange());
            var colorizer = new Colorizer(colorRanges);

            Map.SetColorMapping(colorizer);
        }
    }
}
