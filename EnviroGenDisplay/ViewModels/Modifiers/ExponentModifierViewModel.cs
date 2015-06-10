using System;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class ExponentModifierViewModel : ModifierViewModel
    {
        private readonly ExponentModifier m_modifier;

        public float Exponent
        {
            get { return m_modifier.Exponent; }
            set
            {
                if (Math.Abs(m_modifier.Exponent - value) > float.Epsilon)
                {
                    m_modifier.Exponent = value;
                    OnPropertyChanged();
                }
            }
        }

        public ExponentModifierViewModel()
        {
            m_modifier = new ExponentModifier(1f);
        }

        public override IModifier ToIModifier()
        {
            return m_modifier;
        }
    }
}
