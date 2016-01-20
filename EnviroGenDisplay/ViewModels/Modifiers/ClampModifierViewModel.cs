using System;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class ClampModifierViewModel : ModifierViewModel<ClampModifier>
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

        public ClampModifierViewModel(IEnvironment environment)
            : base(environment)
        {
            Modifier = new ClampModifier(0f, 1f);
        }
    }
}
