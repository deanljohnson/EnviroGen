using System.Collections.ObjectModel;
using System.Linq;
using EnviroGen;
using EnviroGen.Noise.Modifiers;
using EnviroGenDisplay.ViewModels.Modifiers;

namespace EnviroGenDisplay
{
    class EnvironmentData : GenerationOptions
    {
        public new ObservableCollection<ModifierViewModel> Modifiers { get; set; }

        public EnvironmentData()
        {
            Modifiers = new ObservableCollection<ModifierViewModel>();
        }

        public GenerationOptions ToGenerationOptions()
        {
            var options = new GenerationOptions(this) {Modifiers = Modifiers.Select(m => m as IModifier).ToList()};
            return options;
        }
    }
}
