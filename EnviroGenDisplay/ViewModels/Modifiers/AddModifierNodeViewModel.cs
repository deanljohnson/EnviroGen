﻿using System;
using EnviroGen.Nodes;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay.ViewModels.Modifiers
{
    [EditorNodeName("Add", Category = App.ModifiersCategory)]
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
