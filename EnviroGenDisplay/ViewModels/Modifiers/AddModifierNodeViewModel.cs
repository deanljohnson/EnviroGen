using System;
using EnviroGen.Nodes;
using EnviroGen.Noise.Modifiers;
using EnviroGenDisplay.Views.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    [EditorNode("Add", typeof(AddModifierView), Category = App.ModifiersCategory)]
    class AddModifierNodeViewModel : NodeViewModel<ModifierNode<AddModifier>>
    {
        public float Value
        {
            get { return Node.Modifier.Value; }
            set
            {
                if (Math.Abs(Node.Modifier.Value - value) > float.Epsilon)
                {
                    Node.Modifier.Value = value;
                    OnPropertyChanged();
                }
            }
        }

        static AddModifierNodeViewModel()
        {
            Name = "Add";
        }

        public AddModifierNodeViewModel()
            : base("Adding")
        {
            Node = new ModifierNode<AddModifier>
            {
                Modifier = new AddModifier(1f)
            };
        }
    }
}
