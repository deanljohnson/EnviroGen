using System.Collections.ObjectModel;
using EnviroGen.Nodes;

namespace EnviroGenNodeEditor
{
    public class NodeEditor<TNode, TNodeCollection, TNodeConnection, TNodeConnectionCollection>
        where TNode : INode, ISelectable
        where TNodeCollection : Collection<TNode> 
        where TNodeConnection : class, INodeConnection<INode>
        where TNodeConnectionCollection : Collection<TNodeConnection>
    {
        private NodeConnectionManager<TNode, TNodeConnection, TNodeConnectionCollection> m_ConnectionManager { get; }
        private TNode m_SelectedNode;

        public bool MakingConnection => m_ConnectionManager.Connecting;

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

        public TNodeConnection InProgressConnection => m_ConnectionManager.InProgressConnection;

        public NodeEditor()
        {
            m_ConnectionManager = new NodeConnectionManager<TNode, TNodeConnection, TNodeConnectionCollection>();
        }

        public virtual void AddNode(TNode node)
        {
            Nodes.Add(node);
        }

        public virtual void StartConnectionAction(TNodeConnection connection)
        {
            m_ConnectionManager.StartConnectionAction(connection);
        }

        public virtual void EndConnectionAction(TNode destNode)
        {
            if (MakingConnection)
                m_ConnectionManager.EndConnectionAction(destNode);
        }

        public virtual void CancelConnectionAction()
        {
            if (MakingConnection)
                m_ConnectionManager.CancelConnectionAction();
        }
    }
}
