using System;

namespace EnviroGen.Nodes
{
    public abstract class TerminalNode : INode
    {
        public INode Input { get; set; }
        public INode Output {
            get { return null; }
            set
            {
                throw new NotSupportedException("TerminalNodes's cannot have an Output INode");
            }
        }

        public abstract void Modify(Environment environment);
    }
}
