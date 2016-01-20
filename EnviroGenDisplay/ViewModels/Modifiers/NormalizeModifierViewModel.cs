using System;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class NormalizeModifierViewModel : ModifierViewModel<NormalizeModifier>
    {
        public float LowValue
        {
            get { return Modifier.Low; }
            set
            {
                if (Math.Abs(Modifier.Low - value) > float.Epsilon)
                {
                    Modifier.Low = value;
                    OnPropertyChanged();
                }
            }
        }

        public float HighValue
        {
            get { return Modifier.High; }
            set
            {
                if (Math.Abs(Modifier.High - value) > float.Epsilon)
                {
                    Modifier.High = value;
                    OnPropertyChanged();
                }
            }
        }

        public NormalizeModifierViewModel(IEnvironment environment)
            : base(environment)
        {
            Modifier = new NormalizeModifier(0f, 1f);
        }
    }
}
