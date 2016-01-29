using System;
using System.Windows;
using System.Windows.Input;
using EnviroGen.Nodes;
using EnviroGenDisplay.Views;
using EnviroGenNodeEditor;
using Environment = EnviroGen.Environment;

namespace EnviroGenDisplay.ViewModels
{
    public abstract class NodeViewModel : ViewModelBase, IEditorNode, 
        IEquatable<NodeViewModel>
    {
        public static string Name { get; set; }

        private bool m_Dragging { get; set; }
        private Point m_DragStartRelativeMousePos { get; set; }

        public bool HasInput { get; set; } = true;
        public bool HasOutput { get; set; } = true;

        public event EventHandler<EditorMouseEventArgs> OnLeftMouseDown;
        public event EventHandler<EditorMouseEventArgs> OnLeftMouseUp;
        public event EventHandler<NodeDraggedEventArgs> OnNodeDragged;
        public event EventHandler<StartConnectionEventArgs> OnStartConnection;
        public event EventHandler<EndConnectionEventArgs> OnEndConnection;

        public abstract INode Output { get; set; }
        public event EventHandler Started;
        public event EventHandler Finished;
        public abstract void Modify(Environment environment);
        public abstract bool Equals(NodeViewModel other);

        public Point InputControlOffset { get; set; }
        public Point OutputControlOffset { get; set; }

        private double m_X;
        public double X
        {
            get { return m_X; }
            set {
                if (m_X == value) return;
                m_X = value;
                OnPropertyChanged();
            }
        }

        private double m_Y;
        public double Y
        {
            get { return m_Y; }
            set
            {
                if (m_Y == value) return;
                m_Y = value;
                OnPropertyChanged();
            }
        }

        private int m_Z;
        public int Z
        {
            get { return m_Z; }
            set
            {
                if (m_Z == value) return;
                m_Z = value;
                OnPropertyChanged();
            }
        }

        private bool m_Selected;

        public bool Selected
        {
            get { return m_Selected; }
            set
            {
                if (m_Selected == value) return;
                m_Selected = value;
                OnPropertyChanged();
            }
        }

        public ICommand StartDragCommand { get; set; }
        public ICommand ContinueDragCommand { get; set; }
        public ICommand EndDragCommand { get; set; }

        public ICommand StartConnectionCommand { get; set; }
        public ICommand EndConnectionCommand { get; set; }

        protected NodeViewModel()
        {
            StartDragCommand = new RelayCommand(OnStartDrag);
            ContinueDragCommand = new RelayCommand(OnContinueDrag);
            EndDragCommand = new RelayCommand(OnEndDrag);

            StartConnectionCommand = new RelayCommand(OnStartConnectionCommand);
            EndConnectionCommand = new RelayCommand(OnEndConnectionCommand);
        }

        public void OnMouseButtonEvent(object sender, EditorMouseEventArgs e)
        {
            if (e.Button == EditorMouseButton.Left)
            {
                if (e.ButtonState == EditorMouseButtonState.Down)
                {
                    OnLeftMouseDown?.Invoke(this, e);
                }
                else if (e.ButtonState == EditorMouseButtonState.Up)
                {
                    OnLeftMouseUp?.Invoke(this, e);
                }
            }
        }

        protected void OnFinishModify(object sender, EventArgs e)
        {
            Finished?.Invoke(this, null);
        }

        protected void OnStartModify(object sender, EventArgs e)
        {
            Started?.Invoke(this, null);
        }

        private void OnStartDrag(object n = null)
        {
            m_Dragging = true;

            m_DragStartRelativeMousePos = Mouse.GetPosition(NodeEditor.Instance);
        }

        private void OnContinueDrag(object n = null)
        {
            if (m_Dragging && Mouse.LeftButton == MouseButtonState.Pressed)
            {
                var relMousePos = Mouse.GetPosition(NodeEditor.Instance);
                var delta = relMousePos - m_DragStartRelativeMousePos;
                m_DragStartRelativeMousePos = relMousePos;

                X += delta.X;
                Y += delta.Y;

                OnNodeDragged?.Invoke(this, new NodeDraggedEventArgs(this, X, Y, delta.X, delta.Y));
            }
        }

        private void OnEndDrag(object n = null)
        {
            m_Dragging = false;
        }

        private void OnStartConnectionCommand(object sourceControl)
        {
            OnStartConnection?.Invoke(this, new StartConnectionEventArgs(this, 
                X + OutputControlOffset.X, 
                Y + OutputControlOffset.Y));
        }

        private void OnEndConnectionCommand(object destControl)
        {
            OnEndConnection?.Invoke(this, new EndConnectionEventArgs(this,
                X + InputControlOffset.X,
                Y + InputControlOffset.Y));
        }
    }

    public abstract class NodeViewModel<TNode> : NodeViewModel
        where TNode : INode
    {
        private TNode m_Node;

        protected TNode Node {
            get { return m_Node; }
            set
            {
                if (m_Node != null)
                {
                    m_Node.Started -= OnStartModify;
                    m_Node.Finished -= OnFinishModify;
                }

                m_Node = value;
                m_Node.Started += OnStartModify;
                m_Node.Finished += OnFinishModify;
            }
        }

        public override INode Output
        {
            get { return Node.Output; }
            set { Node.Output = value; }
        }

        protected NodeViewModel(string statusMessage = null)
        {
            if (statusMessage != null)
            {
                Started += delegate {
                    MainWindow.Instance.SetStatusTextSafe(statusMessage);
                };

                Finished += delegate {
                    MainWindow.Instance.RemoveStatusTextSafe(statusMessage);
                };
            }
        }

        public override void Modify(Environment environment)
        {
            Node.Modify(environment);
        }

        public override bool Equals(NodeViewModel other)
        {
            return ReferenceEquals(this, other);
        }
    }
}