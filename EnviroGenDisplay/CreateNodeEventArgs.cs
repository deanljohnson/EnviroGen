using System;
using EnviroGenDisplay.ViewModels;

namespace EnviroGenDisplay
{
    public class CreateNodeEventArgs : EventArgs
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public NodeMenuEntry MenuEntry { get; private set; }

        public CreateNodeEventArgs(object menuEntry, double x, double y)
        {
            X = x;
            Y = y;
            MenuEntry = menuEntry as NodeMenuEntry;
        }
    }
}
