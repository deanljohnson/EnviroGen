using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EnviroGen.Nodes;
using EnviroGenDisplay.ViewModels;

namespace EnviroGenDisplay.Views.Nodes
{
    /// <summary>
    /// Interaction logic for Node.xaml
    /// </summary>
    public partial class NodeView : UserControl
    {
        private Point m_DragStartRelativeMousePos { get; set; }
        private bool m_Dragging { get; set; }
        private NodeEditor m_Editor { get; }

        public Point CanvasPoint => new Point(CanvasLeft, CanvasTop);

        public double CanvasLeft
        {
            get { return (double)GetValue(Canvas.LeftProperty); }
            set { SetValue(Canvas.LeftProperty, value); }
        }

        public double CanvasTop
        {
            get { return (double)GetValue(Canvas.TopProperty); }
            set { SetValue(Canvas.TopProperty, value); }
        }

        public NodeView(ViewModelBase nodeViewModel, string nodeName, NodeEditor editor)
        {
            InitializeComponent();

            m_Editor = editor;
            NodeContainer.Content = nodeViewModel;
            NodeNameTextBlock.Text = nodeName;
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NodeContainer.CaptureMouse();

            BringToFront();

            m_Dragging = true;

            m_DragStartRelativeMousePos = Mouse.GetPosition(this);
        }

        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (m_Dragging && e.LeftButton == MouseButtonState.Pressed)
            {
                var relMousePos = Mouse.GetPosition(this);
                var delta = relMousePos - m_DragStartRelativeMousePos;

                CanvasLeft += delta.X;
                CanvasTop += delta.Y;

                m_Editor.UpdateConnectionPositions();
            }
        }

        private void UIElement_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            m_Dragging = false;

            NodeContainer.ReleaseMouseCapture();
        }

        private void BringToFront()
        {
            var parent = Parent as Panel;
            if (parent == null) return;

            var otherZIndices = parent.Children.OfType<UIElement>()
              .Where(x => !ReferenceEquals(x, this))
              .Select(Panel.GetZIndex).ToArray();

            var maxZ = -1;
            if (otherZIndices.Any())
            {
                maxZ = otherZIndices.Max();
            }

            SetValue(Panel.ZIndexProperty, maxZ + 1);
        }

        private void Connection_OnMouseLeftButtonEvent(object sender, MouseButtonEventArgs e)
        {
            var node = NodeContainer.Content as INode;
            var type = ReferenceEquals(sender, InputControl) ? "Input" : "Output";

            if (node != null && 
                type == "Output" && 
                e.ButtonState == MouseButtonState.Pressed)
            {
                m_Editor?.StartConnectionAction(node, sender as Control);
            }
            else if (node != null && 
                type == "Input" && 
                e.ButtonState == MouseButtonState.Released)
            {
                m_Editor?.EndConnectionAction(node, sender as Control);
            }
        }
    }
}
