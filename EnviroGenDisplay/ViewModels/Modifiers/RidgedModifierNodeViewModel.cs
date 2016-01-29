using EnviroGen.Nodes;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    [EditorNodeName("Ridged", Category = App.ModifiersCategory)]
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
