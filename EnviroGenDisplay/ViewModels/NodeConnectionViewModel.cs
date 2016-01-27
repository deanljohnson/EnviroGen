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
            get { return SourcePosition.X; }
            set { SourcePosition = new Point(value, SourcePosition.Y); }
        }
        public double SourceY
        {
            get { return SourcePosition.Y; }
            set { SourcePosition = new Point(SourcePosition.X, value); }
        }
        public double DestX
        {
            get { return DestinationPosition.X; }
            set { DestinationPosition = new Point(value, DestinationPosition.Y); }
        }
        public double DestY
        {
            get { return DestinationPosition.Y; }
            set { DestinationPosition = new Point(DestinationPosition.X, value); }
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

        public Point SourcePosition
        {
            get { return m_SourcePosition; }
            set
            {
                m_SourcePosition = value;
                OnPropertyChanged();
            }
        }

        public Point DestinationPosition
        {
            get { return m_DestinationPosition; }
            set
            {
                m_DestinationPosition = value;
                OnPropertyChanged();
            }
        }

        public NodeConnectionViewModel()
        {
        }
    }
}
