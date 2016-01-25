using System;
using EnviroGen.Nodes;
using Environment = EnviroGen.Environment;

namespace EnviroGenDisplay.ViewModels
{
    public abstract class NodeViewModel<TNode> : ViewModelBase, INode
        where TNode : INode
    {
        private TNode m_Node;

        protected TNode Node {
            get { return m_Node; }
            set
            {
                if (m_Node != null)
                {
                    m_Node.Started -= OnStart;
                    m_Node.Finished -= OnFinish;
                }

                m_Node = value;
                m_Node.Started += OnStart;
                m_Node.Finished += OnFinish;
            }
        }

        public INode Output
        {
            get { return Node.Output; }
            set { Node.Output = value; }
        }

        public event EventHandler Started;
        public event EventHandler Finished;

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

        public virtual void Modify(Environment environment)
        {
            Node.Modify(environment);
        }

        private void OnFinish(object sender, EventArgs e)
        {
            Finished?.Invoke(this, null);
        }

        private void OnStart(object sender, EventArgs e)
        {
            Started?.Invoke(this, null);
        }
    }
}