using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class RidgedModifierViewModel : ModifierViewModel
    {
        private readonly RidgedModifier m_modifier;

        public RidgedModifierViewModel()
        {
            m_modifier = new RidgedModifier();
        }

        public override IModifier ToIModifier()
        {
            return m_modifier;
        }
    }
}
