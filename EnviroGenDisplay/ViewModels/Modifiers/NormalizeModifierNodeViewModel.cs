using System;
using EnviroGen.Nodes;
using EnviroGen.Noise.Modifiers;
using EnviroGenDisplay.Views.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    [EditorNode("Normalize", typeof(NormalizeModifierView), Category = App.ModifiersCategory)]
    class NormalizeModifierNodeViewModel : NodeViewModel<ModifierNode<NormalizeModifier>>
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

        static NormalizeModifierNodeViewModel()
        {
            Name = "Normalize";
        }

        public NormalizeModifierNodeViewModel()
            : base("Normalizing")
        {
            Node = new ModifierNode<NormalizeModifier>
            {
                Modifier = new NormalizeModifier(0f, 1f)
            };
        }
    }
}
