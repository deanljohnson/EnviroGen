﻿using System;
using EnviroGen.Erosion;
using EnviroGen.Nodes;
using EnviroGenDisplay.Views.Erosion;

namespace EnviroGenDisplay.ViewModels.Erosion
{
    [EditorNode("Hydraulic Eroder", typeof(HydraulicErosionView), Category = App.ErosionProcessesCategory)]
    class HydraulicErosionNodeViewModel : NodeViewModel<EroderNode<HydraulicEroder>>
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

        public float RainAmount
        {
            get { return Node.Eroder.RainAmount; }
            set
            {
                if (Math.Abs(Node.Eroder.RainAmount - value) > float.Epsilon)
                {
                    Node.Eroder.RainAmount = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Solubility
        {
            get { return Node.Eroder.Solubility; }
            set
            {
                if (Math.Abs(Node.Eroder.Solubility - value) > float.Epsilon)
                {
                    Node.Eroder.Solubility = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Evaporation
        {
            get { return Node.Eroder.Evaporation; }
            set
            {
                if (Math.Abs(Node.Eroder.Evaporation - value) > float.Epsilon)
                {
                    Node.Eroder.Evaporation = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Capacity
        {
            get { return Node.Eroder.Capacity; }
            set
            {
                if (Math.Abs(Node.Eroder.Capacity - value) > float.Epsilon)
                {
                    Node.Eroder.Capacity = value;
                    OnPropertyChanged();
                }
            }
        }

        static HydraulicErosionNodeViewModel()
        {
            Name = "Hydraulic Eroder";
        }

        public HydraulicErosionNodeViewModel()
            : base("Performing Hydraulic Erosion")
        {
            Node = new EroderNode<HydraulicEroder>
            {
                Eroder = new HydraulicEroder()
            };
        }
    }
}
