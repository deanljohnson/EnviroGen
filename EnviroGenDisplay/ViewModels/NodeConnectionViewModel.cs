using System.Windows;
using System.Windows.Controls;

namespace EnviroGenDisplay.ViewModels
{
    public class NodeConnectionViewModel : ViewModelBase
    {
        private Panel m_GraphContainer { get; }

        private Control m_SourceControl;
        private Control m_DestControl;
        private Point m_One;
        private Point m_Two;

        public Point One
        {
            get { return m_One; }
            set
            {
                m_One = value;
                OnPropertyChanged();
            }
        }

        public Point Two
        {
            get { return m_Two; }
            set
            {
                m_Two = value;
                OnPropertyChanged();
            }
        }

        public Control SourceControl
        {
            get { return m_SourceControl; }
            set
            {
                One = PositionOfElement(value);
                m_SourceControl = value;
            }
        }

        public Control DestControl
        {
            get { return m_DestControl; }
            set
            {
                Two = PositionOfElement(value);
                m_DestControl = value;
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
                One = PositionOfElement(m_SourceControl);
            }
            if (m_DestControl != null)
            {
                Two = PositionOfElement(m_DestControl);
            }
        }

        private Point PositionOfElement(FrameworkElement c)
        {
            var pos = c.TransformToAncestor(m_GraphContainer).Transform(new Point(0, 0));

            pos.X += c.ActualWidth / 2.0;
            pos.Y += c.ActualHeight / 2.0;

            return pos;
        }
    }
}
