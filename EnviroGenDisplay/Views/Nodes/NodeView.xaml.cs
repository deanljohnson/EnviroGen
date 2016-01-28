using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EnviroGenDisplay.ViewModels;
using EnviroGenNodeEditor;

namespace EnviroGenDisplay.Views.Nodes
{
    /// <summary>
    /// Interaction logic for Node.xaml
    /// </summary>
    public partial class NodeView : UserControl
    {
        public event EventHandler<EditorMouseEventArgs> NodeMouseButtonEvent;

        public NodeView()
        {
            InitializeComponent();

            //eww... but it works, and it's not TOO horrible...
            Loaded += delegate
            {
                if (!(DataContext is NodeViewModel)) return;

                var nvm = (NodeViewModel) DataContext;

                //Get position relative to the NodeView
                var inputPosition = InputControl.TranslatePoint(new Point(0, 0), NodeViewBorder);
                var outputPosition = OutputControl.TranslatePoint(new Point(0, 0), NodeViewBorder);

                //Center on the connection
                inputPosition.X += InputConnection.ActualWidth / 2.0;
                inputPosition.Y += InputConnection.ActualHeight / 2.0;
                outputPosition.X += OutputConnection.ActualWidth / 2.0;
                outputPosition.Y += OutputConnection.ActualHeight / 2.0;

                nvm.InputControlOffset = inputPosition;
                nvm.OutputControlOffset = outputPosition;
            };
        }

        protected virtual void OnNodeMouseButtonEvent(EditorMouseEventArgs e)
        {
            var handler = NodeMouseButtonEvent;

            handler?.Invoke(this, e);
        }

        private void NodeBorder_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(NodeViewBorder);

            if (e.ChangedButton == MouseButton.Left)
            {
                OnNodeMouseButtonEvent(new EditorMouseEventArgs(pos.X, pos.Y, EditorMouseButton.Left,
                    EditorMouseButtonState.Down));
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                OnNodeMouseButtonEvent(new EditorMouseEventArgs(pos.X, pos.Y, EditorMouseButton.Right,
                    EditorMouseButtonState.Down));
            }
        }

        private void NodeBorder_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(NodeViewBorder);

            if (e.ChangedButton == MouseButton.Left)
            {
                OnNodeMouseButtonEvent(new EditorMouseEventArgs(pos.X, pos.Y, EditorMouseButton.Left,
                    EditorMouseButtonState.Up));
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                OnNodeMouseButtonEvent(new EditorMouseEventArgs(pos.X, pos.Y, EditorMouseButton.Right,
                    EditorMouseButtonState.Up));
            }
        }
    }
}
