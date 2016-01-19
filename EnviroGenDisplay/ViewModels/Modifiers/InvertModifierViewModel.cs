using System;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class InvertModifierViewModel : ModifierViewModel
    {
        private readonly InvertModifier m_Modifier;

        public float MaxValue
        {
            get { return m_Modifier.MaxValue; }
            set
            {
                if (Math.Abs(m_Modifier.MaxValue - value) > float.Epsilon)
                {
                    m_Modifier.MaxValue = value;
                    OnPropertyChanged();
                }
                
            }
        }

        public InvertModifierViewModel()
        {
            m_Modifier = new InvertModifier(1f);
        }

        public override void Modify(ref float[,] map)
        {
            m_Modifier.Modify(ref map);
        }
    }
}
