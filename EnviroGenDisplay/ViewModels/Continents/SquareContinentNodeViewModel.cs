using System;
using EnviroGen.Continents;
using EnviroGen.Nodes;

namespace EnviroGenDisplay.ViewModels.Continents
{
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

        public SquareContinentNodeViewModel()
        {
            Node = new ContinentGeneratorNode<SquareContinentGenerator>
            {
                ContinentGenerator = new SquareContinentGenerator()
            };
        }
    }
}
