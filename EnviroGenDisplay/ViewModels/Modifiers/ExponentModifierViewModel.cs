using System;
using EnviroGen.HeightMaps;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class ExponentModifierViewModel : ModifierViewModel
    {
        private readonly ExponentModifier m_Modifier;

        public float Exponent
        {
            get { return m_Modifier.Exponent; }
            set
            {
                if (Math.Abs(m_Modifier.Exponent - value) > float.Epsilon)
                {
                    m_Modifier.Exponent = value;
                    OnPropertyChanged();
                }
            }
        }

        public ExponentModifierViewModel()
        {
            m_Modifier = new ExponentModifier(1f);
        }

        public override void Modify(HeightMap map)
        {
            m_Modifier.Modify(map);
        }
    }
}
