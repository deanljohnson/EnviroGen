using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EnviroGen.Nodes;
using EnviroGenDisplay.Actions;
using EnviroGenDisplay.ViewModels;
using EnviroGenDisplay.Views.Nodes;

namespace EnviroGenDisplay.Views
{
    //TODO: This has all sorts of nasty hacks. However, I could not find a better solution and so am stuck with this for now.
    // Problem 1: The view knows it's ViewModel. MVVM be damned I guess...
    // Problem 2: Tracking the right click position like this is very sketchy.
    // Problem 3: Throwing an exception when the right click position is not set correctly is pretty bad.
    /// <summary>
    /// Interaction logic for NodeEditor.xaml
    /// </summary>
    public partial class NodeEditor : UserControl
    {
        private bool m_DraggingNodeConnection { get; set; }
        private NodeConnectionViewModel m_NodeConnection { get; set; }
        private CreateConnectionAction m_CreateConnectionAction { get; set; }

        private Point? m_RightClickPosition { get; set; }

        private NodeEditorViewModel m_VM;
        private NodeEditorViewModel m_ViewModel => m_VM ?? (m_VM = DataContext as NodeEditorViewModel);

        public ObservableCollection<NodeConnectionViewModel> NodeConnections { get; set; } = new ObservableCollection<NodeConnectionViewModel>();

        public NodeEditor()
        {
            InitializeComponent();

            Connections.DataContext = this;
        }

        public void StartConnectionAction(INode sourceNode, Control sourceControl)
        {
            m_CreateConnectionAction = new CreateConnectionAction(sourceNode, sourceControl);

            //Removed connections from the same source
            for (var i = 0; i < NodeConnections.Count; i++)
            {
                if (ReferenceEquals(NodeConnections[i].InputControl, sourceControl))
                {
                    NodeConnections.RemoveAt(i);
                    i--;
                }
            }

            sourceNode.Output = null;

            m_NodeConnection = new NodeConnectionViewModel(NodeCanvas)
            {
                InputControl = m_CreateConnectionAction.SourceControl
            };

            m_NodeConnection.OutputPosition = m_NodeConnection.InputPosition;

            NodeConnections.Add(m_NodeConnection);

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                m_DraggingNodeConnection = true;
            }
        }

        public void EndConnectionAction(INode destNode, Control destControl)
        {
            m_DraggingNodeConnection = false;
            m_CreateConnectionAction.Finished = true;
            
            //Nodes are not allowed to connect to themselves
            if (destNode != m_CreateConnectionAction.Source)
                m_CreateConnectionAction.Source.Output = destNode;

            m_NodeConnection.OutputControl = destControl;
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
            m_DraggingNodeConnection = false;
            m_CreateConnectionAction = null;

            NodeConnections.Remove(m_NodeConnection);
        }

        private void NodeMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;

            if (menuItem == null) return;

            var nodeName = menuItem.Header.ToString();
            var nodeViewModel = m_ViewModel.GetNodeViewModel(nodeName);

            var nodeViewControl = new NodeView(nodeViewModel, nodeName, this);

            //Right click shouldnt ever be null here - if so this will fail hard and tell us there is a problem
            if (m_RightClickPosition == null) throw new Exception("Right click position was not properly set on the canvas");
            nodeViewControl.CanvasLeft = m_RightClickPosition.Value.X;
            nodeViewControl.CanvasTop = m_RightClickPosition.Value.Y;
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
            if (m_CreateConnectionAction != null && !m_CreateConnectionAction.Finished)
                CancelConnectionAction();
        }

        private void NodeCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (m_DraggingNodeConnection)
            {
                //Sanity check here
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    m_NodeConnection.OutputPosition = Mouse.GetPosition(NodeCanvas);
                }
                else
                {
                    CancelConnectionAction();
                }
            }
        }
    }
}
