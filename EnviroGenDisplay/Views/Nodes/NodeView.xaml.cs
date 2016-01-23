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
        public NodeView(ViewModelBase nodeViewModel)
        {
            InitializeComponent();

            NodeContainer.Child = new ContentControl {Content = nodeViewModel};
        }
    }
}
