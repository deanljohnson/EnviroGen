using System.Collections.ObjectModel;
using System.Linq;
using EnviroGen.Nodes;

namespace EnviroGenNodeEditor
{
    public class NodeEditor<TNode, TNodeCollection, TNodeConnection, TNodeConnectionCollection>
        where TNode : class, IEditorNode
        where TNodeCollection : Collection<TNode> 
        where TNodeConnection : class, INodeConnection<INode>, new()
        where TNodeConnectionCollection : Collection<TNodeConnection>
    {
        private NodeConnectionManager<TNodeConnection, TNodeConnectionCollection> m_ConnectionManager { get; }
        private TNode m_SelectedNode;
        private bool m_MouseUpOnNode { get; set; }

        public TNode SelectedNode {
            get { return m_SelectedNode; }
            set
            {
                if (m_SelectedNode != null)
                {
                    m_SelectedNode.Selected = false;
                }

                m_SelectedNode = value;

                if (m_SelectedNode != null)
                {
                    m_SelectedNode.Selected = true;
                }
            }
        }

        public virtual TNodeCollection Nodes { get; set; }

        public virtual TNodeConnectionCollection NodeConnections
        {
            get { return m_ConnectionManager.NodeConnections; }
            set { m_ConnectionManager.NodeConnections = value; }
        }

        public NodeEditor()
        {
            m_ConnectionManager = new NodeConnectionManager<TNodeConnection, TNodeConnectionCollection>();
        }

        public virtual void AddNode(TNode node, double x, double y)
        {
            node.X = x;
            node.Y = y;

            node.OnStartConnection += OnStartConnectionAction;
            node.OnEndConnection += OnEndConnectionAction;
            node.OnLeftMouseDown += OnNodeMouseDown;
            node.OnLeftMouseUp += OnNodeMouseUp;
            node.OnNodeDragged += OnNodeDragged;

            Nodes.Add(node);
        }

        public virtual void PushMouseButtonEvent(EditorMouseEventArgs e)
        {
            switch (e.Button)
            {
                case EditorMouseButton.Left:
                    switch (e.ButtonState)
                    {
                        case EditorMouseButtonState.Up:
                            OnLeftButtonUp(e.X, e.Y);
                            break;
                        case EditorMouseButtonState.Down:
                            OnLeftButtonDown(e.X, e.Y);
                            break;
                    }
                    break;
                case EditorMouseButton.Right:
                    switch (e.ButtonState)
                    {
                        case EditorMouseButtonState.Up:
                            OnRightButtonUp(e.X, e.Y);
                            break;
                        case EditorMouseButtonState.Down:
                            OnRightButtonDown(e.X, e.Y);
                            break;
                    }
                    break;
            }
        }

        public virtual void PushMouseMoveEvent(EditorMouseEventArgs e)
        {
            if (e.Button == EditorMouseButton.Left && e.ButtonState == EditorMouseButtonState.Down)
            {
                OnLeftMouseDrag(e.X, e.Y);
            }
        }

        public virtual void CancelConnectionAction()
        {
            if (m_ConnectionManager.Connecting)
                m_ConnectionManager.CancelConnectionAction();
        }

        protected virtual void OnStartConnectionAction(object sender, StartConnectionEventArgs e)
        {
            m_ConnectionManager.StartConnectionAction(e);
        }

        protected virtual void OnEndConnectionAction(object sender, EndConnectionEventArgs e)
        {
            if (m_ConnectionManager.Connecting)
                m_ConnectionManager.EndConnectionAction(e);
        }

        protected virtual void OnNodeDragged(object sender, NodeDraggedEventArgs e)
        {
            foreach (var connection in NodeConnections)
            {
                if (connection.Source == e.Node)
                {
                    connection.SourceX += e.DeltaX;
                    connection.SourceY += e.DeltaY;
                }
                else if (connection.Destination == e.Node)
                {
                    connection.DestX += e.DeltaX;
                    connection.DestY += e.DeltaY;
                }
            }
        }

        protected virtual void OnNodeMouseUp(object sender, EditorMouseEventArgs e)
        {
            m_MouseUpOnNode = true;
        }

        protected virtual void OnNodeMouseDown(object sender, EditorMouseEventArgs e)
        {
            SelectedNode = sender as TNode;

            BringToFront(SelectedNode);
        }

        protected virtual void BringToFront(TNode node)
        {
            var otherZIndices = Nodes
              .Where(x => !ReferenceEquals(x, node))
              .Select(nvm => nvm.Z).ToArray();

            var maxZ = -1;
            if (otherZIndices.Any())
            {
                maxZ = otherZIndices.Max();
            }

            node.Z = maxZ + 1;
        }

        protected virtual void OnRightButtonDown(double mouseX, double mouseY)
        {
        }

        protected virtual void OnRightButtonUp(double mouseX, double mouseY)
        {
        }

        protected virtual void OnLeftButtonDown(double mouseX, double mouseY)
        {
        }

        protected virtual void OnLeftButtonUp(double mouseX, double mouseY)
        {
            if (SelectedNode != null && !m_MouseUpOnNode)
                SelectedNode = null;

            m_MouseUpOnNode = false;

            if (m_ConnectionManager.Connecting)
            {
                CancelConnectionAction();
            }
        }

        protected virtual void OnLeftMouseDrag(double mouseX, double mouseY)
        {
            if (m_ConnectionManager.Connecting)
            {
                m_ConnectionManager.SetConnectionDestination(mouseX, mouseY);
            }
        }

    }
}
