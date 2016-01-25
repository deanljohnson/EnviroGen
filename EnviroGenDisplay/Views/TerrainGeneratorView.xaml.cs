using System.Windows.Controls;
using System.Windows.Input;

namespace EnviroGenDisplay.Views
{
    /// <summary>
    /// Interaction logic for EnvironmentDataView.xaml
    /// </summary>
    public partial class TerrainGeneratorView : UserControl
    {
        public TerrainGeneratorView()
        {
            InitializeComponent();
        }

        private void PropertyLabel_OnMouseEnter(object sender, MouseEventArgs e)
        {
            MainWindow.Instance.SetContextInfoTextSafe((sender as Label).ToolTip.ToString());
        }

        private void PropertyLabel_OnMouseLeave(object sender, MouseEventArgs e)
        {
            MainWindow.Instance.RemoveContextInfoTextSafe((sender as Label).ToolTip.ToString());
        }
    }
}
