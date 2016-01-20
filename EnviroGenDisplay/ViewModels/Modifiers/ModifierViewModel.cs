using EnviroGen.HeightMaps;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    abstract class ModifierViewModel : ViewModelBase, IModifier
    {
        public abstract void Modify(HeightMap map);
    }
}
