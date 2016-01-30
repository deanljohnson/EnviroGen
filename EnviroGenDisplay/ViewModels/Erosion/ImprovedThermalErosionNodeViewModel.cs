using System;
using EnviroGen.Erosion;
using EnviroGen.Nodes;
using EnviroGenDisplay.Views.Erosion;

namespace EnviroGenDisplay.ViewModels.Erosion
{
    [EditorNode("Improved Thermal Eroder", typeof(ImprovedThermalErosionView), Category = App.ErosionProcessesCategory)]
    class ImprovedThermalErosionNodeViewModel : NodeViewModel<EroderNode<ImprovedThermalEroder>>
    {
        public int Iterations
        {
            get { return Node.Eroder.Iterations; }
            set
            {
                if (Node.Eroder.Iterations != value)
                {
                    Node.Eroder.Iterations = value;
                    OnPropertyChanged();
                }
            }
        }

        public float TalusAngle
        {
            get { return Node.Eroder.TalusAngle; }
            set
            {
                if (Math.Abs(Node.Eroder.TalusAngle - value) > float.Epsilon)
                {
                    Node.Eroder.TalusAngle = value;
                    OnPropertyChanged();
                }
            }
        }

        static ImprovedThermalErosionNodeViewModel()
        {
            Name = "Improved Thermal Eroder";
        }

        public ImprovedThermalErosionNodeViewModel()
            : base("Performing Improved Thermal Erosion")
        {
            Node = new EroderNode<ImprovedThermalEroder>
            {
                Eroder = new ImprovedThermalEroder()
            };
        }
    }
}
