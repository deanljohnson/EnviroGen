using System.Windows;
using System.Windows.Controls;

namespace EnviroGenDisplay.ViewModels
{
    public class NodeConnectionViewModel : ViewModelBase
    {
        private Panel m_GraphContainer { get; }

        private Control m_InputControl;
        private Control m_OutputControl;
        private Point m_InputPosition;
        private Point m_OutputPosition;

        public Point InputPosition
        {
            get { return m_InputPosition; }
            set
            {
                m_InputPosition = value;
                OnPropertyChanged();
            }
        }

        public Point OutputPosition
        {
            get { return m_OutputPosition; }
            set
            {
                m_OutputPosition = value;
                OnPropertyChanged();
            }
        }

        public Control InputControl
        {
            get { return m_InputControl; }
            set
            {
                InputPosition = CenterPositionOfElement(value);
                m_InputControl = value;
            }
        }

        public Control OutputControl
        {
            get { return m_OutputControl; }
            set
            {
                OutputPosition = CenterPositionOfElement(value);
                m_OutputControl = value;
            }
        }

        public NodeConnectionViewModel(Panel graphContainer)
        {
            m_GraphContainer = graphContainer;
        }

        public void SetLineEndsToControlLocations()
        {
            if (m_InputControl != null)
            {
                InputPosition = CenterPositionOfElement(m_InputControl);
            }
            if (m_OutputControl != null)
            {
                OutputPosition = CenterPositionOfElement(m_OutputControl);
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
