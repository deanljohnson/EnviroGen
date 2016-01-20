using System.Windows.Input;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    public abstract class InvertableModifierViewModel<TIModifier> : ModifierViewModel<TIModifier>
        where TIModifier : IInvertableModifier
    {
        public ICommand InvertApplyCommand { get; set; }

        protected InvertableModifierViewModel(IEnvironment environment)
            : base(environment)
        {
            InvertApplyCommand = new RelayCommand(InvertApply);
        }

        public void InvertApply(object n = null)
        {
            Environment.ApplyTerrainModifierInverted(Modifier);
        }
    }
}
