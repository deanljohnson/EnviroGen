using System.Windows;
using EnviroGen.Nodes;
using EnviroGenNodeEditor;

namespace EnviroGenDisplay.ViewModels
{
    public class NodeConnectionViewModel : ViewModelBase, INodeConnection<INode>
    {
        private INode m_Destination;
        private Point m_SourcePosition;
        private Point m_DestinationPosition;

        public double SourceX
        {
            get { return m_SourcePosition.X; }
            set
            {
                m_SourcePosition = new Point(value, m_SourcePosition.Y);
                OnPropertyChanged();
            }
        }
        public double SourceY
        {
            get { return m_SourcePosition.Y; }
            set
            {
                m_SourcePosition = new Point(m_SourcePosition.X, value);
                OnPropertyChanged();
            }
        }
        public double DestX
        {
            get { return m_DestinationPosition.X; }
            set
            {
                m_DestinationPosition = new Point(value, m_DestinationPosition.Y);
                OnPropertyChanged();
            }
        }
        public double DestY
        {
            get { return m_DestinationPosition.Y; }
            set
            {
                m_DestinationPosition = new Point(m_DestinationPosition.X, value);
                OnPropertyChanged();
            }
        }

        public INode Source { get; set; }
        public INode Destination {
            get { return m_Destination; }
            set
            {
                m_Destination = value;
                if (Source != null)
                {
                    Source.Output = m_Destination;
                }
            }
        }

        public bool Connected => Source != null && Destination != null;
    }
}
