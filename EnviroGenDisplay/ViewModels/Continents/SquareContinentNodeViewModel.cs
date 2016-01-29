using System;
using EnviroGen.Continents;
using EnviroGen.Nodes;

namespace EnviroGenDisplay.ViewModels.Continents
{
    [EditorNodeName("Square Continent Generator", Category = App.ContinentGeneratorsCategory)]
    class SquareContinentNodeViewModel : NodeViewModel<ContinentGeneratorNode<SquareContinentGenerator>>
    {
        public int MaximumContinentSize {
            get { return Node.ContinentGenerator.MaximumContinentSize; }
            set
            {
                if (Node.ContinentGenerator.MaximumContinentSize != value)
                {
                    Node.ContinentGenerator.MaximumContinentSize = value;
                    OnPropertyChanged();
                }
            }
        }

        public int MinimumContinentSize
        {
            get { return Node.ContinentGenerator.MinimumContinentSize; }
            set
            {
                if (Node.ContinentGenerator.MinimumContinentSize != value)
                {
                    Node.ContinentGenerator.MinimumContinentSize = value;
                    OnPropertyChanged();
                }
            }
        }

        public float ScaleAmount {
            get { return Node.ContinentGenerator.ScaleAmount; }
            set
            {
                if (Math.Abs(Node.ContinentGenerator.ScaleAmount - value) > float.Epsilon)
                {
                    Node.ContinentGenerator.ScaleAmount = value;
                    OnPropertyChanged();
                }
            }
        }

        static SquareContinentNodeViewModel()
        {
            Name = "Square Continent Generator";
        }

        public SquareContinentNodeViewModel()
            : base("Generating Continents (Square)")
        {
            Node = new ContinentGeneratorNode<SquareContinentGenerator>
            {
                ContinentGenerator = new SquareContinentGenerator()
            };
        }
    }
}
