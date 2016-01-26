using System;
using EnviroGen.Nodes;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    class ClampModifierNodeViewModel : NodeViewModel<ModifierNode<ClampModifier>>
    {
        public float LowValue
        {
            get { return Node.Modifier.Low; }
            set
            {
                if (Math.Abs(Node.Modifier.Low - value) > float.Epsilon)
                {
                    Node.Modifier.Low = value;
                    OnPropertyChanged();
                }
            }
        }

        public float HighValue
        {
            get { return Node.Modifier.High; }
            set
            {
                if (Math.Abs(Node.Modifier.High - value) > float.Epsilon)
                {
                    Node.Modifier.High = value;
                    OnPropertyChanged();
                }
            }
        }

        public ClampModifierNodeViewModel()
            : base("Clamp")
        {
            Node = new ModifierNode<ClampModifier>
            {
                Modifier = new ClampModifier(0f, 1f)
            };
        }
    }
}
