using System;
using EnviroGen.Nodes;
using EnviroGen.Noise.Modifiers;
using EnviroGenDisplay.Views.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    [EditorNode("Scale", typeof(ScaleModifierView), Category = App.ModifiersCategory)]
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

        static ScaleModifierNodeViewModel()
        {
            Name = "Scale";
        }

        public ScaleModifierNodeViewModel()
            : base("Scaling")
        {
            Node = new ModifierNode<ScaleModifier>
            {
                Modifier = new ScaleModifier(1f)
            };
        }
    }
}
