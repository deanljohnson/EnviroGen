using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            DataContextChanged += OnDataContextChange;
        }

        private void OnDataContextChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(DataContext is NodeViewModel))
            {
                return;
            }

            var nvm = (NodeViewModel)DataContext;

            //high-jack the "sender" argument of these events
            //this is a pretty bad hack, but works.
            NodeViewBorder.PreviewMouseUp += delegate (object o, MouseButtonEventArgs mouseE)
            {
                nvm.OnMouseUp.Invoke(nvm, mouseE);
            };
            NodeViewBorder.PreviewMouseDown += delegate (object o, MouseButtonEventArgs mouseE)
            {
                nvm.OnMouseDown.Invoke(nvm, mouseE);
            };
        }
    }
}
