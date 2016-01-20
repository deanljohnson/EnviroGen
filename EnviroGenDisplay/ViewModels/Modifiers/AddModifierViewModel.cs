using System;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class AddModifierViewModel : InvertableModifierViewModel<AddModifier>
    {
        public float Value
        {
            get { return Modifier.Value; }
            set
            {
                if (Math.Abs(Modifier.Value - value) > float.Epsilon)
                {
                    Modifier.Value = value;
                    OnPropertyChanged();
                }
            }
        }

        public AddModifierViewModel(IEnvironment environment, float value = 1f)
            : base(environment)
        {
            Modifier = new AddModifier(value);
        }
    }
}
