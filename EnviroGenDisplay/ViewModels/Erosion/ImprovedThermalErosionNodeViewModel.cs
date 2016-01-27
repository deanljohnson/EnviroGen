﻿using System;
using EnviroGen.Erosion;
using EnviroGen.Nodes;

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
            : base("Improved Thermal Eroder", "Performing Improved Thermal Erosion")
        {
            Node = new EroderNode<ImprovedThermalEroder>
            {
                Eroder = new ImprovedThermalEroder()
            };
        }
    }
}