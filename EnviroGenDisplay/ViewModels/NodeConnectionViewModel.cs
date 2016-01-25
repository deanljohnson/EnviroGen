using System.Windows;
using System.Windows.Controls;
using EnviroGen.Nodes;

namespace EnviroGenDisplay.ViewModels
{
    public class NodeConnectionViewModel : ViewModelBase
    {
        private Panel m_GraphContainer { get; }

        private INode m_Destination;
        private Control m_SourceControl;
        private Control m_DestinationControl;
        private Point m_SourcePosition;
        private Point m_DestinationPosition;

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

        public Control SourceControl
        {
            get { return m_SourceControl; }
            set
            {
                SourcePosition = CenterPositionOfElement(value);
                m_SourceControl = value;
            }
        }

        public Control DestinationControl
        {
            get { return m_DestinationControl; }
            set
            {
                DestinationPosition = CenterPositionOfElement(value);
                m_DestinationControl = value;
            }
        }

        public NodeConnectionViewModel(Panel graphContainer)
        {
            m_GraphContainer = graphContainer;
        }

        public void SetLineEndsToControlLocations()
        {
            if (m_SourceControl != null)
            {
                SourcePosition = CenterPositionOfElement(m_SourceControl);
            }
            if (m_DestinationControl != null)
            {
                DestinationPosition = CenterPositionOfElement(m_DestinationControl);
            }
        }

        private Point CenterPositionOfElement(FrameworkElement c)
        {
            var pos = c.TransformToAncestor(m_GraphContainer).Transform(new Point(0, 0));

            //Adjust to be in the center of the element
            pos.X += c.ActualWidth / 2.0;
            pos.Y += c.ActualHeight / 2.0;

            return pos;
        }
    }
}
