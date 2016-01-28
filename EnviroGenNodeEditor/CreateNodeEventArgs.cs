using System;

namespace EnviroGenNodeEditor
{
    public class CreateNodeEventArgs : EventArgs
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public string Name { get; private set; }

        public CreateNodeEventArgs(double x, double y, string name)
        {
            X = x;
            Y = y;
            Name = name;
        }
    }
}
