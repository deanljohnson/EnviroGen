using System;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class AddModifierViewModel : ModifierViewModel
    {
        private readonly AddModifier m_Modifier;

        public float Value
        {
            get { return m_Modifier.Value; }
            set
            {
                if (Math.Abs(m_Modifier.Value - value) > float.Epsilon)
                {
                    m_Modifier.Value = value;
                    OnPropertyChanged();
                }
            }
        }

        public AddModifierViewModel(float value = 1f)
        {
            m_Modifier = new AddModifier(value);
        }

        public override void Modify(ref float[,] map)
        {
            m_Modifier.Modify(ref map);
        }
    }
}
