using System.Collections.ObjectModel;
using System.Windows.Controls;
using EnviroGen.Nodes;
using EnviroGenDisplay.ViewModels;

namespace EnviroGenDisplay
{
    public class NodeConnectionManager
    {
        private NodeConnectionViewModel m_NodeConnection { get; set; }
        private Panel m_NodePanel { get; }

        public ObservableCollection<NodeConnectionViewModel> Connections { get; set; } 
            = new ObservableCollection<NodeConnectionViewModel>();

        public bool Connecting { get; private set; }

        public NodeConnectionViewModel InProgressConnection
            => (Connecting && m_NodeConnection != null) ? m_NodeConnection : null;

        public NodeConnectionManager(Panel nodePanel)
        {
            m_NodePanel = nodePanel;
        }

        public void StartConnectionAction(INode node, Control control)
        {
            Connecting = true;

            m_NodeConnection =
                new NodeConnectionViewModel(m_NodePanel)
                {
                    Source = node,
                    SourceControl = control
                };

            //Removed connections from the same source
            for (var i = 0; i < Connections.Count; i++)
            {
                if (ReferenceEquals(Connections[i].SourceControl, control))
                {
                    Connections.RemoveAt(i);
                    i--;
                }
            }

            node.Output = null;

            m_NodeConnection.DestinationPosition = m_NodeConnection.SourcePosition;

            Connections.Add(m_NodeConnection);
        }

        public void EndConnectionAction(INode node, Control control)
        {
            Connecting = false;

            //Nodes are not allowed to connect to themselves
            if (node != m_NodeConnection.Source && !m_NodeConnection.Connected)
            {
                m_NodeConnection.Destination = node;
                m_NodeConnection.DestinationControl = control;
            }
        }

        public void CancelConnection()
        {
            Connecting = false;

            if (m_NodeConnection == null) return;

            Connections.Remove(m_NodeConnection);

            m_NodeConnection = null;
        }
    }
}
