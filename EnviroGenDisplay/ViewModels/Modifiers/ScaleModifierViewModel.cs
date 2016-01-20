using System;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class ScaleModifierViewModel : InvertableModifierViewModel<ScaleModifier>
    {
        public float Scale
        {
            get { return Modifier.Scale; }
            set
            {
                if (Math.Abs(Modifier.Scale - value) > float.Epsilon)
                {
                    Modifier.Scale = value;
                    OnPropertyChanged();
                }
            }
        }

        public ScaleModifierViewModel(IEnvironment environment)
            : base(environment)
        {
            Modifier = new ScaleModifier(1f);
        }
    }
}
