using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class RidgedModifierViewModel : ModifierViewModel
    {
        private readonly RidgedModifier m_Modifier;

        public RidgedModifierViewModel()
        {
            m_Modifier = new RidgedModifier();
        }

        public override void Modify(ref float[,] map)
        {
            m_Modifier.Modify(ref map);
        }
    }
}
