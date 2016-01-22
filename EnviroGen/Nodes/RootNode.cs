using System;

namespace EnviroGen.Nodes
{
    public abstract class RootNode : INode
    {
        public INode Input
        {
            get { return null; }
            set
            {
                throw new NotSupportedException("RootNode's cannot have an Input INode");
            }
        }

        public INode Output { get; set; }

        public abstract void Modify(Environment environment);
    }
}
