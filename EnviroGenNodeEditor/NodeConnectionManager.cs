﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using EnviroGen.Nodes;

namespace EnviroGenNodeEditor
{
    public class NodeConnectionManager<TNode, TNodeConnection, TNodeConnectionCollection>
        where TNode : INode, ISelectable
        where TNodeConnection : class, INodeConnection<INode>
        where TNodeConnectionCollection : Collection<TNodeConnection>
    {
        private TNodeConnection m_Connection { get; set; }

        public bool Connecting { get; set; }
        public TNodeConnectionCollection NodeConnections { get; set; }
        public TNodeConnection InProgressConnection => m_Connection;

        public void StartConnectionAction(TNodeConnection connection)
        {
            Debug.Assert(connection != null);

            m_Connection = connection;

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

        public void EndConnectionAction(TNode destNode)
        {
            Debug.Assert(destNode != null);

            Connecting = false;

            //Nodes cannot connect to themselves, 
            //and if the connection is complete we dont change anythiing
            if (!destNode.Equals(m_Connection.Source) &&
                (m_Connection.Source == null ||
                m_Connection.Destination == null))
            {
                m_Connection.Destination = destNode;
            }
        }

        public void CancelConnectionAction()
        {
            Connecting = false;

            if (m_Connection == null) return;

            NodeConnections.Remove(m_Connection);

            m_Connection = null;
        }
    }
}
