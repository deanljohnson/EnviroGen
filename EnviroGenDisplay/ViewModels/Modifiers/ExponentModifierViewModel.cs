using System;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class ExponentModifierViewModel : InvertableModifierViewModel<ExponentModifier>
    {
        public float Exponent
        {
            get { return Modifier.Exponent; }
            set
            {
                if (Math.Abs(Modifier.Exponent - value) > float.Epsilon)
                {
                    Modifier.Exponent = value;
                    OnPropertyChanged();
                }
            }
        }

        public ExponentModifierViewModel(IEnvironment environment)
            : base(environment)
        {
            Modifier = new ExponentModifier(1f);
        }
    }
}
