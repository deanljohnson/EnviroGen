using System;
using EnviroGen.Nodes;

namespace EnviroGenNodeEditor
{
    public class StartConnectionEventArgs : EventArgs
    {
        public INode SourceNode { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }

        public StartConnectionEventArgs(INode source, double x, double y)
        {
            SourceNode = source;
            X = x;
            Y = y;
        }
    }
}
