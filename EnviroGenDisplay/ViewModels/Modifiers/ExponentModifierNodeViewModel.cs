using System;
using EnviroGen.Nodes;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
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

        public ExponentModifierNodeViewModel()
        {
            Node = new ModifierNode<ExponentModifier>
            {
                Modifier = new ExponentModifier(1f)
            };
        }
    }
}
