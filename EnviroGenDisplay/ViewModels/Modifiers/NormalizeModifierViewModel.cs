using System;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class NormalizeModifierViewModel : ModifierViewModel
    {
        private readonly NormalizeModifier m_modifier;

        public float LowValue
        {
            get { return m_modifier.Low; }
            set
            {
                if (Math.Abs(m_modifier.Low - value) > float.Epsilon)
                {
                    m_modifier.Low = value;
                    OnPropertyChanged();
                }
            }
        }

        public float HighValue
        {
            get { return m_modifier.High; }
            set
            {
                if (Math.Abs(m_modifier.High - value) > float.Epsilon)
                {
                    m_modifier.High = value;
                    OnPropertyChanged();
                }
            }
        }

        public NormalizeModifierViewModel()
        {
            m_modifier = new NormalizeModifier(0f, 1f);
        }

        public override IModifier ToIModifier()
        {
            return m_modifier;
        }
    }
}
