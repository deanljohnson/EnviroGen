using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EnviroGenNodeEditor;

namespace EnviroGenDisplay.Views
{
    public partial class NodeEditor : UserControl
    {
        public static NodeEditor Instance { get; private set; }

        private Point m_NodeCreationPoint { get; set; }

        public event EventHandler<EditorMouseEventArgs> EditorMouseMoveEvent;
        public event EventHandler<EditorMouseEventArgs> EditorMouseButtonEvent;
        public event EventHandler<CreateNodeEventArgs> CreateNodeEvent;
        public event EventHandler DeleteSelectedNodeEvent;

        public NodeEditor()
        {
            Instance = this;

            InitializeComponent();
        }

        protected virtual void OnCreateNodeEvent(CreateNodeEventArgs e)
        {
            var handler = CreateNodeEvent;

            handler?.Invoke(this, e);
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

        protected virtual void OnDeleteSelectedNodeEvent()
        {
            var handler = DeleteSelectedNodeEvent;

            handler?.Invoke(this, EventArgs.Empty);
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

            m_NodeCreationPoint = pos;

            OnEditorMouseButtonEvent(new EditorMouseEventArgs(pos.X, pos.Y, EditorMouseButton.Right, EditorMouseButtonState.Up));
        }

        private void NodeMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Debug.Assert(sender is MenuItem);

            var menuItem = (MenuItem) sender;
            
            var menuDataContext = (menuItem).DataContext;
            
            OnCreateNodeEvent(new CreateNodeEventArgs(menuDataContext, m_NodeCreationPoint.X, m_NodeCreationPoint.Y));
        }

        private void NodeCanvas_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                OnDeleteSelectedNodeEvent();
                e.Handled = true;
            }
        }

        private void NodeCanvas_OnMouseEnter(object sender, MouseEventArgs e)
        {
            NodeCanvas.Focus();
        }
    }
}
