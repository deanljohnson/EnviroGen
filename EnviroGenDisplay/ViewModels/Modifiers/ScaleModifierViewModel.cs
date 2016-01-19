using System;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class ScaleModifierViewModel : ModifierViewModel
    {
        private readonly ScaleModifier m_Modifier;

        public float Scale
        {
            get { return m_Modifier.Scale; }
            set
            {
                if (Math.Abs(m_Modifier.Scale - value) > float.Epsilon)
                {
                    m_Modifier.Scale = value;
                    OnPropertyChanged();
                }
            }
        }

        public ScaleModifierViewModel()
        {
            m_Modifier = new ScaleModifier(1f);
        }

        public override void InvertModify(ref float[,] map)
        {
            m_Modifier.Modify(ref map);
        }
    }
}
