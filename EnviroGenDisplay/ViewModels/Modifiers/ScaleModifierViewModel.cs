using System;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class ScaleModifierViewModel : ModifierViewModel
    {
        private readonly ScaleModifier m_modifier;

        public float Scale
        {
            get { return m_modifier.Scale; }
            set
            {
                if (Math.Abs(m_modifier.Scale - value) > float.Epsilon)
                {
                    m_modifier.Scale = value;
                    OnPropertyChanged();
                }
            }
        }

        public ScaleModifierViewModel()
        {
            m_modifier = new ScaleModifier(1f);
        }

        public override IModifier ToIModifier()
        {
            return m_modifier;
        }
    }
}
