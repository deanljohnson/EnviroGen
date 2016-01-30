using System;
using EnviroGen.Nodes;
using EnviroGen.Noise.Modifiers;
using EnviroGenDisplay.Views.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    [EditorNode("Exponent", typeof(ExponentModifierView), Category = App.ModifiersCategory)]
    class ExponentModifierNodeViewModel : NodeViewModel<ModifierNode<ExponentModifier>>
    {
        public float Exponent
        {
            get { return Node.Modifier.Exponent; }
            set
            {
                if (Math.Abs(Node.Modifier.Exponent - value) > float.Epsilon)
                {
                    Node.Modifier.Exponent = value;
                    OnPropertyChanged();
                }
            }
        }

        static ExponentModifierNodeViewModel()
        {
            Name = "Exponent";
        }

        public ExponentModifierNodeViewModel()
            : base ("Exponentiating")
        {
            Node = new ModifierNode<ExponentModifier>
            {
                Modifier = new ExponentModifier(1f)
            };
        }
    }
}
