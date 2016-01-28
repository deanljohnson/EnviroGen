using System;
using EnviroGen.Nodes;

namespace EnviroGenNodeEditor
{
    public class NodeDraggedEventArgs : EventArgs
    {
        public double NewX { get; private set; }
        public double NewY { get; private set; }
        public double DeltaX { get; private set; }
        public double DeltaY { get; private set; }
        public INode Node { get; private set; }

        public NodeDraggedEventArgs(INode node, double x, double y, double deltaX, double deltaY)
        {
            NewX = x;
            NewY = y;
            DeltaX = deltaX;
            DeltaY = deltaY;
            Node = node;
        }
    }
}
