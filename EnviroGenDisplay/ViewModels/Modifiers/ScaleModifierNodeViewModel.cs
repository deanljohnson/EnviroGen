using System;
using EnviroGen.Nodes;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class ScaleModifierNodeViewModel : NodeViewModel<ModifierNode<ScaleModifier>>
    {
        public float Scale
        {
            get { return Node.Modifier.Scale; }
            set
            {
                if (Math.Abs(Node.Modifier.Scale - value) > float.Epsilon)
                {
                    Node.Modifier.Scale = value;
                    OnPropertyChanged();
                }
            }
        }

        public ScaleModifierNodeViewModel()
        {
            Node = new ModifierNode<ScaleModifier>
            {
                Modifier = new ScaleModifier(1f)
            };
        }
    }
}
