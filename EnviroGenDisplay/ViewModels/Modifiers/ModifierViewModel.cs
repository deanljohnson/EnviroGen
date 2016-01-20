using System.Windows.Input;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    public abstract class ModifierViewModel<TModifier> : ViewModelBase 
        where TModifier : IModifier
    {
        public TModifier Modifier { get; set; }
        public ICommand ApplyCommand { get; set; }
        public IEnvironment Environment { get; set; }

        protected ModifierViewModel(IEnvironment environment)
        {
            Environment = environment;
            ApplyCommand = new RelayCommand(Apply);
        }

        public void Apply(object n = null)
        {
            Environment.ApplyTerrainModifier(Modifier);
        }
    }
}
