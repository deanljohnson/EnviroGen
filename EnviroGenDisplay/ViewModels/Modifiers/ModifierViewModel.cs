using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    abstract class ModifierViewModel : ViewModelBase, IModifier
    {
        public abstract void Modify(ref float[,] map);
    }
}
