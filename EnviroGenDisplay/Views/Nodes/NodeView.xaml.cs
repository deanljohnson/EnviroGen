using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using EnviroGenDisplay.ViewModels;

namespace EnviroGenDisplay.Views.Nodes
{
    /// <summary>
    /// Interaction logic for Node.xaml
    /// </summary>
    public partial class NodeView : UserControl
    {
        public NodeView()
        {
            InitializeComponent();

            //eww... but it works, and it's not TOO horrible...
            Loaded += delegate
            {
                Debug.Assert(DataContext is NodeViewModel);
                var nvm = (NodeViewModel) DataContext;

                //Get position relative to the NodeView
                var inputPosition = InputControl.TranslatePoint(new Point(0, 0), NodeViewBorder);
                var outputPosition = OutputControl.TranslatePoint(new Point(0, 0), NodeViewBorder);

                //Center on the connection
                inputPosition.X += InputConnection.ActualWidth / 2.0;
                inputPosition.Y += InputConnection.ActualHeight / 2.0;
                outputPosition.X += OutputConnection.ActualWidth / 2.0;
                outputPosition.Y += OutputConnection.ActualHeight / 2.0;

                nvm.InputControlOffset = inputPosition;
                nvm.OutputControlOffset = outputPosition;
            };
        }
    }
}
