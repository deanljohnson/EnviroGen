using System;
using EnviroGen.Nodes;

namespace EnviroGenNodeEditor
{
    public class EndConnectionEventArgs : EventArgs
    {
        public INode DestNode { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }

        public EndConnectionEventArgs(INode dest, double x, double y)
        {
            DestNode = dest;
            X = x;
            Y = y;
        }
    }
}
