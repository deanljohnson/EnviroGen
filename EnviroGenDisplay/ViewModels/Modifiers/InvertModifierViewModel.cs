using System;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class InvertModifierViewModel : ModifierViewModel
    {
        private readonly InvertModifier m_modifier;

        public float MaxValue
        {
            get { return m_modifier.MaxValue; }
            set
            {
                if (Math.Abs(m_modifier.MaxValue - value) > float.Epsilon)
                {
                    m_modifier.MaxValue = value;
                    OnPropertyChanged();
                }
                
            }
        }

        public InvertModifierViewModel()
        {
            m_modifier = new InvertModifier(1f);
        }

        public override IModifier ToIModifier()
        {
            return m_modifier;
        }
    }
}
