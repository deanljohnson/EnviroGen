﻿using System;
using System.Collections.ObjectModel;
using EnviroGenDisplay.ViewModels.Continents;
using EnviroGenDisplay.ViewModels.Erosion;
using EnviroGenDisplay.ViewModels.Modifiers;
using EnviroGenNodeEditor;
using Editor = EnviroGenNodeEditor.NodeEditor<EnviroGenDisplay.ViewModels.NodeViewModel, System.Collections.ObjectModel.ObservableCollection<EnviroGenDisplay.ViewModels.NodeViewModel>,
                                            EnviroGenDisplay.ViewModels.NodeConnectionViewModel, System.Collections.ObjectModel.ObservableCollection<EnviroGenDisplay.ViewModels.NodeConnectionViewModel>>;

namespace EnviroGenDisplay.ViewModels
{
    public class NodeEditorViewModel : ViewModelBase
    {
        public IDisplayedEnvironment Environment { get; set; }

        public Editor Editor { get; set; }

        public ObservableCollection<NodeViewModel> Nodes
        {
            get { return Editor.Nodes; }
            set { Editor.Nodes = value; }
        }

        public ObservableCollection<NodeConnectionViewModel> NodeConnections
        {
            get { return Editor.NodeConnections; }
            set { Editor.NodeConnections = value; }
        }

        public NodeEditorViewModel(IDisplayedEnvironment environment, Editor editor)
        {
            Environment = environment;
            Editor = editor;

            Nodes = new ObservableCollection<NodeViewModel>();
            NodeConnections = new ObservableCollection<NodeConnectionViewModel>();
        }

        public void OnDeleteSelectedNodeEvent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnCreateNodeEvent(object sender, CreateNodeEventArgs e)
        {
            var nodeViewModel = GetNodeViewModel(e.Name);

            Editor.AddNode(nodeViewModel, e.X, e.Y);
        }

        public void OnEditorMouseButtonEvent(object sender, EditorMouseEventArgs e)
        {
            Editor.PushMouseButtonEvent(e);
        }

        public void OnEditorMouseMoveEvent(object sender, EditorMouseEventArgs e)
        {
            Editor.PushMouseMoveEvent(e);
        }

        private NodeViewModel GetNodeViewModel(string nodeName)
        {
            //TODO: Fix these hard coded mappings.... this is not neat by any means
            if (nodeName == "Simplex Noise")
            {
                return new TerrainGeneratorNodeViewModel(Environment);
            }
            if (nodeName == "Add")
            {
                return new AddModifierNodeViewModel();
            }
            if (nodeName == "Clamp")
            {
                return new ClampModifierNodeViewModel();
            }
            if (nodeName == "Exponent")
            {
                return new ExponentModifierNodeViewModel();
            }
            if (nodeName == "Invert")
            {
                return new InvertModifierNodeViewModel();
            }
            if (nodeName == "Normalize")
            {
                return new NormalizeModifierNodeViewModel();
            }
            if (nodeName == "Ridged")
            {
                return new RidgedModifierNodeViewModel();
            }
            if (nodeName == "Scale")
            {
                return new ScaleModifierNodeViewModel();
            }
            if (nodeName == "Hydraulic")
            {
                return new HydraulicErosionNodeViewModel();
            }
            if (nodeName == "Improved Thermal")
            {
                return new ImprovedThermalErosionNodeViewModel();
            }
            if (nodeName == "Thermal")
            {
                return new ThermalErosionNodeViewModel();
            }
            if (nodeName == "Square")
            {
                return new SquareContinentNodeViewModel();
            }
            if (nodeName == "Simple Colorizer")
            {
                return new ColorizerNodeViewModel();
            }

            return null;
        }
    }
}
