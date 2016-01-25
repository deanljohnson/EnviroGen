using System;

namespace EnviroGen.Nodes
{
    public interface INode
    {
        INode Output { get; set; }

        event EventHandler Started;
        event EventHandler Finished;

        void Modify(Environment environment);
    }
}