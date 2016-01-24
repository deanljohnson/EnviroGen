using System.Windows.Controls;
using EnviroGen.Nodes;

namespace EnviroGenDisplay.Actions
{
    public class CreateConnectionAction
    {
        public INode Source { get; set; }
        public Control SourceControl { get; set; }
        public bool Finished { get; set; }

        public CreateConnectionAction(INode source, Control sourceControl)
        {
            Source = source;
            SourceControl = sourceControl;
        }
    }
}
