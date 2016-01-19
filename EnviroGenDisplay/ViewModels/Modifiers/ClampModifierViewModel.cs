using System;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class ClampModifierViewModel : ModifierViewModel
    {
        private readonly ClampModifier m_Modifier;

        public float LowValue
        {
            get { return m_Modifier.Low; }
            set
            {
                if (Math.Abs(m_Modifier.Low - value) > float.Epsilon)
                {
                    m_Modifier.Low = value;
                    OnPropertyChanged();
                }
            }
        }

        public float HighValue
        {
            get { return m_Modifier.High; }
            set
            {
                if (Math.Abs(m_Modifier.High - value) > float.Epsilon)
                {
                    m_Modifier.High = value;
                    OnPropertyChanged();
                }
            }
        }

        public ClampModifierViewModel()
        {
            m_Modifier = new ClampModifier(0f, 1f);
        }

        public override void InvertModify(ref float[,] map)
        {
            m_Modifier.InvertModify(ref map);
        }
    }
}
