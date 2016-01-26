using EnviroGen.Nodes;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class RidgedModifierNodeViewModel : NodeViewModel<ModifierNode<RidgedModifier>>
    {
        public RidgedModifierNodeViewModel()
            : base("Ridged")
        {
            Node = new ModifierNode<RidgedModifier>
            {
                Modifier = new RidgedModifier()
            };
        }
    }
}
