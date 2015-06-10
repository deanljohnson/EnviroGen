using System;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class AddModifierViewModel : ModifierViewModel
    {
        private readonly AddModifier m_modifier;

        public float Value
        {
            get { return m_modifier.Value; }
            set
            {
                if (Math.Abs(m_modifier.Value - value) > float.Epsilon)
                {
                    m_modifier.Value = value;
                    OnPropertyChanged();
                }
            }
        }

        public AddModifierViewModel()
        {
            m_modifier = new AddModifier(1f);
        }

        public override IModifier ToIModifier()
        {
            return m_modifier;
        }
    }
}
