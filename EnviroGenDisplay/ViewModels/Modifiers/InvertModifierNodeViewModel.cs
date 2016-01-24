using System;
using EnviroGen.Nodes;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class InvertModifierNodeViewModel : NodeViewModel<ModifierNode<InvertModifier>>
    {
        public float MaxValue
        {
            get { return Node.Modifier.MaxValue; }
            set
            {
                if (Math.Abs(Node.Modifier.MaxValue - value) > float.Epsilon)
                {
                    Node.Modifier.MaxValue = value;
                    OnPropertyChanged();
                }
                
            }
        }

        public InvertModifierNodeViewModel()
        {
            Node = new ModifierNode<InvertModifier>
            {
                Modifier = new InvertModifier(1f)
            };
        }
    }
}
