using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EnviroGen.Nodes;
using EnviroGenDisplay.ViewModels;
using EnviroGenDisplay.Views.Nodes;

namespace EnviroGenDisplay.Views
{
    //TODO: This has all sorts of nasty hacks. However, I could not find a better solution and so am stuck with this for now.
    // Problem 1: The view knows it's ViewModel. MVVM be damned I guess...
    // Problem 2: Tracking the right click position like this is very sketchy.
    /// <summary>
    /// Interaction logic for NodeEditor.xaml
    /// </summary>
    public partial class NodeEditor : UserControl
    {
        public static NodeEditor Instance { get; private set; }

        private bool m_DraggingNodeConnection { get; set; }

        private Point? m_RightClickPosition { get; set; }

        private NodeEditorViewModel m_Vm;
        private NodeEditorViewModel m_ViewModel => m_Vm ?? (m_Vm = DataContext as NodeEditorViewModel);

        public NodeConnectionManager NodeConnectionManager { get; set; }

        public ObservableCollection<NodeConnectionViewModel> NodeConnections
        {
            get { return NodeConnectionManager.Connections; }
            set { NodeConnectionManager.Connections = value; }
        }

        public NodeEditor()
        {
            Instance = this;

            InitializeComponent();

            NodeConnectionManager = new NodeConnectionManager(NodeCanvas);
            Connections.DataContext = this;
        }

        public void StartConnectionAction(INode sourceNode, Control sourceControl)
        {
            NodeConnectionManager.StartConnectionAction(sourceNode, sourceControl);

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                m_DraggingNodeConnection = true;
            }
        }

        public void EndConnectionAction(INode destNode, Control destControl)
        {
            m_DraggingNodeConnection = false;
            
            NodeConnectionManager.EndConnectionAction(destNode, destControl);
        }

        public void UpdateConnectionPositions()
        {
            foreach (var nodeConnection in Connections.Items.OfType<NodeConnectionViewModel>())
            {
                nodeConnection.SetLineEndsToControlLocations();
            }
        }

        private void CancelConnectionAction()
        {
            NodeConnectionManager.CancelConnection();
            m_DraggingNodeConnection = false;
        }

        private void NodeMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;

            if (menuItem == null) return;

            var nodeName = menuItem.Header.ToString();

            //Right click shouldnt ever be null here - if so this will fail hard and tell us there is a problem
            if (m_RightClickPosition == null) throw new Exception("Right click position was not properly set on the canvas");
            var nodeViewModel = m_ViewModel.GetNodeViewModel(nodeName);
            var nodeViewControl = new NodeView(nodeViewModel, nodeName, m_RightClickPosition.Value);
            m_RightClickPosition = null;

            NodeCanvas.Children.Add(nodeViewControl);
        }

        private void NodeCanvas_OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            m_RightClickPosition = Mouse.GetPosition(NodeCanvas);
        }

        private void NodeCanvas_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void NodeCanvas_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (NodeConnectionManager.Connecting)
                CancelConnectionAction();
        }

        private void NodeCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (m_DraggingNodeConnection)
            {
                //Sanity check here
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    if (NodeConnectionManager.Connecting)
                    {
                        NodeConnectionManager.InProgressConnection.DestinationPosition = Mouse.GetPosition(NodeCanvas);
                    }
                }
                else
                {
                    CancelConnectionAction();
                }
            }
        }
    }
}
