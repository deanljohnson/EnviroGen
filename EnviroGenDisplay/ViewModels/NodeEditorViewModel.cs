using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using EnviroGenDisplay.ViewModels.Continents;
using EnviroGenDisplay.ViewModels.Erosion;
using EnviroGenDisplay.ViewModels.Modifiers;
using Editor = EnviroGenNodeEditor.NodeEditor<EnviroGenDisplay.ViewModels.NodeViewModel, System.Collections.ObjectModel.ObservableCollection<EnviroGenDisplay.ViewModels.NodeViewModel>,
                                            EnviroGenDisplay.ViewModels.NodeConnectionViewModel, System.Collections.ObjectModel.ObservableCollection<EnviroGenDisplay.ViewModels.NodeConnectionViewModel>>;

namespace EnviroGenDisplay.ViewModels
{
    public class NodeEditorViewModel : ViewModelBase
    {
        private bool m_MouseUpOnNode { get; set; }

        public Point? NodeCreationPoint { get; set; }

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

        public ICommand CreateNodeCommand { get; set; }

        public NodeEditorViewModel(IDisplayedEnvironment environment, Editor editor)
        {
            Environment = environment;
            Editor = editor;

            Nodes = new ObservableCollection<NodeViewModel>();
            NodeConnections = new ObservableCollection<NodeConnectionViewModel>();

            CreateNodeCommand = new RelayCommand(CreateNode);
        }

        public void OnEditorMouseButtonEvent(object sender, EditorMouseEventArgs e)
        {
            if (e.Button == EditorMouseButton.Left)
            {
                switch (e.ButtonState)
                {
                    case EditorMouseButtonState.Up:
                        OnLeftButtonUp(e.X, e.Y);
                        break;
                    case EditorMouseButtonState.Down:
                        OnLeftButtonDown(e.X, e.Y);
                        break;
                }
            }
            else if (e.Button == EditorMouseButton.Right)
            {
                if (e.ButtonState == EditorMouseButtonState.Up)
                {
                    OnRightButtonUp(e.X, e.Y);
                }
            }
        }

        public void OnEditorMouseMoveEvent(object sender, EditorMouseEventArgs e)
        {
            if (e.Button == EditorMouseButton.Left && e.ButtonState == EditorMouseButtonState.Down)
            {
                OnLeftMouseDrag(e.X, e.Y);
            }
        }

        private void OnRightButtonUp(double mouseX, double mouseY)
        {
            NodeCreationPoint = new Point(mouseX, mouseY);
        }

        private void OnLeftButtonDown(double mouseX, double mouseY)
        {

        }

        private void OnLeftButtonUp(double mouseX, double mouseY)
        {
            if (Editor.SelectedNode != null && !m_MouseUpOnNode)
                Editor.SelectedNode = null;

            m_MouseUpOnNode = false;

            if (Editor.MakingConnection)
            {
                Editor.CancelConnectionAction();
            }
        }

        private void OnLeftMouseDrag(double mouseX, double mouseY)
        {
            if (Editor.MakingConnection)
            {
                Editor.InProgressConnection.DestX = mouseX;
                Editor.InProgressConnection.DestY = mouseY;
            }
        }

        private void CreateNode(object name)
        {
            Debug.Assert(name is string);
            var nodeViewModel = GetNodeViewModel((string) name);

            nodeViewModel.OnMouseDown += OnSelectableMouseDown;
            nodeViewModel.OnMouseUp += OnSelectableMouseUp;

            if (NodeCreationPoint != null)
            {
                nodeViewModel.Position = NodeCreationPoint.Value;
            }

            Nodes.Add(nodeViewModel);
        }

        private void OnSelectableMouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.Assert(sender is NodeViewModel);

            var nvm = (NodeViewModel)sender;
            Editor.SelectedNode = nvm;

            BringToFront(nvm);
        }

        private void OnSelectableMouseUp(object sender, MouseButtonEventArgs e)
        {
            Debug.Assert(sender is NodeViewModel);

            m_MouseUpOnNode = true;
        }

        private void BringToFront(NodeViewModel e)
        {
            var otherZIndices = Nodes
              .Where(x => !ReferenceEquals(x, e))
              .Select(nvm => nvm.Z).ToArray();

            var maxZ = -1;
            if (otherZIndices.Any())
            {
                maxZ = otherZIndices.Max();
            }

            e.Z = maxZ + 1;
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
