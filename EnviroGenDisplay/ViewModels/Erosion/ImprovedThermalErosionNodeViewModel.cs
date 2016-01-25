using System;
using EnviroGen.Erosion;
using EnviroGen.Nodes;
using Environment = EnviroGen.Environment;

namespace EnviroGenDisplay.ViewModels.Erosion
{
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

        public ImprovedThermalErosionNodeViewModel()
        {
            Node = new EroderNode<ImprovedThermalEroder>()
            {
                Eroder = new ImprovedThermalEroder()
            };
        }

        public override void Modify(Environment environment)
        {
            MainWindow.Instance.SetStatusTextSafe("Performing Improved Thermal Erosion");
            base.Modify(environment);
            MainWindow.Instance.RemoveStatusTextSafe("Performing Improved Thermal Erosion");
        }
    }
}
