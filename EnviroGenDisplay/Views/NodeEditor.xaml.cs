using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EnviroGenDisplay.ViewModels;
using EnviroGenNodeEditor;

namespace EnviroGenDisplay.Views
{
    public partial class NodeEditor : UserControl
    {
        public static NodeEditor Instance { get; private set; }

        private readonly NodeEditor<NodeViewModel, 
            ObservableCollection<NodeViewModel>, 
            NodeConnectionViewModel, 
            ObservableCollection<NodeConnectionViewModel>> 
                m_Editor = new NodeEditor<NodeViewModel, ObservableCollection<NodeViewModel>, NodeConnectionViewModel, ObservableCollection<NodeConnectionViewModel>>();

        private bool m_MouseUpOnNode { get; set; }

        private bool m_DraggingNodeConnection { get; set; }

        private Point? m_RightClickPosition { get; set; }

        private NodeEditorViewModel m_Vm;
        private NodeEditorViewModel m_ViewModel => m_Vm ?? (m_Vm = DataContext as NodeEditorViewModel);

        public ObservableCollection<NodeViewModel> Nodes
        {
            get { return m_Editor.Nodes; }
            set { m_Editor.Nodes = value; }
        }

        public ObservableCollection<NodeConnectionViewModel> NodeConnections
        {
            get { return m_Editor.NodeConnections; }
            set { m_Editor.NodeConnections = value; }
        }

        public NodeEditor()
        {
            Instance = this;

            InitializeComponent();

            Nodes = new ObservableCollection<NodeViewModel>();
            NodeConnections = new ObservableCollection<NodeConnectionViewModel>();

            Connections.DataContext = this;

            Nodes.CollectionChanged += OnNodesChange;
        }

        public void StartConnectionAction(NodeViewModel node, Control c)
        {
            var connection = new NodeConnectionViewModel(NodeCanvas)
            {
                Source = node,
                SourcePosition = Mouse.GetPosition(NodeCanvas),
                SourceControl = c
            };

            m_Editor.StartConnectionAction(connection);
            m_DraggingNodeConnection = true;
        }

        public void EndConnectionAction(NodeViewModel node, Control c)
        {
            if (m_Editor.MakingConnection)
            {
                m_Editor.InProgressConnection.DestinationControl = c;
                m_Editor.EndConnectionAction(node);
            }
            
            m_DraggingNodeConnection = false;
        }

        public void UpdateConnectionPositions()
        {
            foreach (var nodeConnection in Connections.Items.OfType<NodeConnectionViewModel>())
            {
                nodeConnection.SetLineEndsToNodeLocations();
            }
        }

        private void OnNodesChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var newItem in e.NewItems)
                {
                    NodesContainer.Items.Add(newItem);
                }
            }

            if (e.OldItems != null)
            {
                foreach (var oldItem in e.OldItems)
                {
                    NodesContainer.Items.Remove(oldItem);
                }
            }
        }

        private void OnSelectableMouseDown(object sender, MouseEventArgs e)
        {
            Debug.Assert(sender is NodeViewModel);

            var nvm = (NodeViewModel) sender;
            m_Editor.SelectedNode = nvm;

            BringToFront(nvm);
        }

        private void OnSelectableMouseUp(object sender, MouseEventArgs e)
        {
            Debug.Assert(sender is NodeViewModel);

            m_MouseUpOnNode = true;
        }

        private void BringToFront(NodeViewModel e)
        {
            var otherZIndices = NodesContainer.Items.OfType<NodeViewModel>()
              .Where(x => !ReferenceEquals(x, e))
              .Select(nvm => nvm.Z).ToArray();

            var maxZ = -1;
            if (otherZIndices.Any())
            {
                maxZ = otherZIndices.Max();
            }

            e.Z = maxZ + 1;
        }

        private void NodeMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;

            if (menuItem == null) return;

            var nodeName = menuItem.Header.ToString();

            var nodeViewModel = m_ViewModel.GetNodeViewModel(nodeName);
            
            nodeViewModel.OnMouseDown += OnSelectableMouseDown;
            nodeViewModel.OnMouseUp += OnSelectableMouseUp;

            if (m_RightClickPosition != null)
            {
                nodeViewModel.Position = m_RightClickPosition.Value;
            }

            Nodes.Add(nodeViewModel);
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
            if (m_Editor.SelectedNode != null && !m_MouseUpOnNode)
                m_Editor.SelectedNode = null;

            m_MouseUpOnNode = false;

            if (m_Editor.MakingConnection)
            {
                m_Editor.CancelConnectionAction();
                m_DraggingNodeConnection = false;
            }
        }

        private void NodeCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (m_DraggingNodeConnection)
            {
                //Sanity check here
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    if (m_Editor.MakingConnection)
                    {
                        var mousePos = Mouse.GetPosition(NodeCanvas);

                        m_Editor.InProgressConnection.DestX = mousePos.X;
                        m_Editor.InProgressConnection.DestY = mousePos.Y;
                    }
                }
                else
                {
                    m_Editor.CancelConnectionAction();
                }
            }
        }
    }
}
