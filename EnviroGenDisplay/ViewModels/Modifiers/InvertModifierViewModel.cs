using System;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class InvertModifierViewModel : ModifierViewModel<InvertModifier>
    {
        public float MaxValue
        {
            get { return Modifier.MaxValue; }
            set
            {
                if (Math.Abs(Modifier.MaxValue - value) > float.Epsilon)
                {
                    Modifier.MaxValue = value;
                    OnPropertyChanged();
                }
                
            }
        }

        public InvertModifierViewModel(IEnvironment environment)
            : base(environment)
        {
            Modifier = new InvertModifier(1f);
        }
    }
}
