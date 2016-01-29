using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using EnviroGen.Nodes;

namespace EnviroGenNodeEditor
{
    public class NodeConnectionManager<TNode, TNodeConnection, TNodeConnectionCollection>
        where TNode : class, IEditorNode
        where TNodeConnection : class, INodeConnection<INode>, new()
        where TNodeConnectionCollection : Collection<TNodeConnection>
    {
        private TNodeConnection m_Connection { get; set; }

        public bool Connecting { get; set; }
        public TNodeConnectionCollection NodeConnections { get; set; }
        public TNodeConnection InProgressConnection => m_Connection;

        public void StartConnectionAction(StartConnectionEventArgs e)
        {
            m_Connection = new TNodeConnection
            {
                Source = e.SourceNode,
                SourceX = e.X,
                SourceY = e.Y
            };

            Connecting = true;

            //Removed connections from the same source
            for (var i = 0; i < NodeConnections.Count; i++)
            {
                if (ReferenceEquals(NodeConnections[i].Source, m_Connection.Source))
                {
                    NodeConnections.RemoveAt(i);
                    i--;
                }
            }

            //Disconnect source from any current output
            if (m_Connection.Source.Output != null)
                m_Connection.Source.Output = null;

            //For lack of a better plan...
            m_Connection.DestX = m_Connection.SourceX;
            m_Connection.DestY = m_Connection.SourceY;

            NodeConnections.Add(m_Connection);
        }

        public void EndConnectionAction(EndConnectionEventArgs e)
        {
            Debug.Assert(e.DestNode != null);

            Connecting = false;

            //Nodes cannot connect to themselves, 
            //and if the connection is complete we dont change anythiing
            if (!e.DestNode.Equals(m_Connection.Source) &&
                (m_Connection.Source == null ||
                m_Connection.Destination == null))
            {
                m_Connection.Destination = e.DestNode;
                m_Connection.DestX = e.X;
                m_Connection.DestY = e.Y;
            }
            else { CancelConnectionAction(); }
        }

        public void CancelConnectionAction()
        {
            Connecting = false;

            if (m_Connection == null) return;

            NodeConnections.Remove(m_Connection);

            m_Connection = null;
        }

        public void SetConnectionDestination(double x, double y)
        {
            if (Connecting)
            {
                InProgressConnection.DestX = x;
                InProgressConnection.DestY = y;
            }
        }

        public void RemoveConnectionsToNode(TNode node)
        {
            if (Connecting && m_Connection.Source == node)
            {
                CancelConnectionAction();
            }

            var referenced = NodeConnections.Where(c => c.Source == node || c.Destination == node).ToArray();

            foreach (var connection in referenced)
            {
                //Break the conncetion between nodes
                connection.Source.Output = null;
                NodeConnections.Remove(connection);
            }
        }
    }
}
