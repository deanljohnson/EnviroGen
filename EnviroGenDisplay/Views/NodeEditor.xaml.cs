using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using EnviroGenDisplay.ViewModels;

namespace EnviroGenDisplay.Views
{
    //TODO: Wrap mouse events to pass info of the exact parameters that NodeEditorViewModel needs
    public partial class NodeEditor : UserControl
    {
        public static NodeEditor Instance { get; private set; }

        //TODO: This connection between View and ViewModel is bad, 
        //but it's in an effort to move as much logic as possible to the view model.
        //Hopefully, we will eventually be able to get rid of this
        private NodeEditorViewModel m_Vm;
        private NodeEditorViewModel m_ViewModel => m_Vm ?? (m_Vm = DataContext as NodeEditorViewModel);

        public event EventHandler<EditorMouseEventArgs> EditorMouseMoveEvent;
        public event EventHandler<EditorMouseEventArgs> EditorMouseButtonEvent;

        public NodeEditor()
        {
            Instance = this;

            InitializeComponent();
        }

        public void StartConnectionAction(NodeViewModel node, Control c)
        {
            var connection = new NodeConnectionViewModel(NodeCanvas)
            {
                Source = node,
                SourcePosition = Mouse.GetPosition(NodeCanvas),
                SourceControl = c
            };

            m_ViewModel.Editor.StartConnectionAction(connection);
        }

        public void EndConnectionAction(NodeViewModel node, Control c)
        {
            if (m_ViewModel.Editor.MakingConnection)
            {
                m_ViewModel.Editor.InProgressConnection.DestinationControl = c;
                m_ViewModel.Editor.EndConnectionAction(node);
            }
        }

        public void UpdateConnectionPositions()
        {
            foreach (var nodeConnection in Connections.Items.OfType<NodeConnectionViewModel>())
            {
                nodeConnection.SetLineEndsToNodeLocations();
            }
        }

        protected virtual void OnEditorMouseButtonEvent(EditorMouseEventArgs e)
        {
            var handler = EditorMouseButtonEvent;

            handler?.Invoke(this, e);
        }

        protected virtual void OnEditorMouseMove(EditorMouseEventArgs e)
        {
            var handler = EditorMouseMoveEvent;

            handler?.Invoke(this, e);
        }

        private void NodeCanvas_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var pos = Mouse.GetPosition(NodeCanvas);
            
            OnEditorMouseButtonEvent(new EditorMouseEventArgs(pos.X, pos.Y, EditorMouseButton.Left, EditorMouseButtonState.Down));
        }

        private void NodeCanvas_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var pos = Mouse.GetPosition(NodeCanvas);

            OnEditorMouseButtonEvent(new EditorMouseEventArgs(pos.X, pos.Y, EditorMouseButton.Left, EditorMouseButtonState.Up));
        }

        private void NodeCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            var pos = Mouse.GetPosition(NodeCanvas);

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                OnEditorMouseMove(new EditorMouseEventArgs(pos.X, pos.Y, EditorMouseButton.Left, 
                    EditorMouseButtonState.Down));
            }
            else if (Mouse.RightButton == MouseButtonState.Pressed)
            {
                OnEditorMouseMove(new EditorMouseEventArgs(pos.X, pos.Y, EditorMouseButton.Right,
                    EditorMouseButtonState.Down));
            }
            else
            {
                OnEditorMouseMove(new EditorMouseEventArgs(pos.X, pos.Y, EditorMouseButton.None,
                    EditorMouseButtonState.None));
            }
        }

        private void NodeCanvas_OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var pos = Mouse.GetPosition(NodeCanvas);

            OnEditorMouseButtonEvent(new EditorMouseEventArgs(pos.X, pos.Y, EditorMouseButton.Right, EditorMouseButtonState.Up));
        }
    }
}
