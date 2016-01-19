using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    abstract class ModifierViewModel : ViewModelBase, IModifier
    {
        public abstract void InvertModify(ref float[,] map);
    }
}
