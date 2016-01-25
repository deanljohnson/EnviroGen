using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EnviroGenDisplay
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void PropertyLabel_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Debug.Assert(sender is Label);
            EnviroGenDisplay.MainWindow.Instance.SetContextInfoTextSafe(((Label) sender).ToolTip.ToString());
        }

        private void PropertyLabel_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Debug.Assert(sender is Label);
            EnviroGenDisplay.MainWindow.Instance.RemoveContextInfoTextSafe(((Label) sender).ToolTip.ToString());
        }
    }
}
