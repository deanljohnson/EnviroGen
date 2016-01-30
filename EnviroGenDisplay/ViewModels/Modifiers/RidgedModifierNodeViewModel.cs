using EnviroGen.Nodes;
using EnviroGen.Noise.Modifiers;
using EnviroGenDisplay.Views.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    [EditorNode("Ridged", typeof(RidgedModifierView), Category = App.ModifiersCategory)]
    class RidgedModifierNodeViewModel : NodeViewModel<ModifierNode<RidgedModifier>>
    {
        static RidgedModifierNodeViewModel()
        {
            Name = "Ridged";
        }

        public RidgedModifierNodeViewModel()
            : base("Making Ridges")
        {
            Node = new ModifierNode<RidgedModifier>
            {
                Modifier = new RidgedModifier()
            };
        }
    }
}
