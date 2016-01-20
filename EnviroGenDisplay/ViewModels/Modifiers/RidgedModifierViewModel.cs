using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class RidgedModifierViewModel : ModifierViewModel<RidgedModifier>
    {
        public RidgedModifierViewModel(IEnvironment environment)
            : base(environment)
        {
            Modifier = new RidgedModifier();
        }
    }
}
