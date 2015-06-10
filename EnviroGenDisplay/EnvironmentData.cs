using System.Collections.ObjectModel;
using System.Linq;
using EnviroGen;
using EnviroGenDisplay.ViewModels.Modifiers;

namespace EnviroGenDisplay
{
    class EnvironmentData : GenerationOptions
    {
        public new ObservableCollection<ModifierViewModel> Modifiers { get; set; }

        public bool Combining { get; set; }

        public EnvironmentData()
        {
            Modifiers = new ObservableCollection<ModifierViewModel>();
        }

        public GenerationOptions ToGenerationOptions()
        {
            // ReSharper disable once ArrangeThisQualifier
            var options = new GenerationOptions(this) { Modifiers = this.Modifiers.Select(m => m.ToIModifier()).ToList() };
            return options;
        }
    }
}
