using EnviroGen.Nodes;
using Environment = EnviroGen.Environment;

namespace EnviroGenDisplay.ViewModels
{
    class NodeViewModel<TNode> : ViewModelBase, INode
        where TNode : INode
    {
        protected TNode Node;

        public INode Input
        {
            get { return Node.Input; }
            set { Node.Input = value; }
        }

        public INode Output
        {
            get { return Node.Output; }
            set { Node.Output = value; }
        }

        public void Modify(Environment environment)
        {
            Node.Modify(environment);
        }
    }
}
