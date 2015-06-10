using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    abstract class ModifierViewModel : ViewModelBase
    {
        public abstract IModifier ToIModifier();
    }
}
