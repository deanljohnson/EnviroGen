using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EnviroGenDisplay.Views;

namespace EnviroGenDisplay
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ContextInfoProvider_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Debug.Assert(sender is ContextInfoProvider);
            EnviroGenDisplay.MainWindow.Instance.SetContextInfoTextSafe(((ContextInfoProvider)sender).ContextInfo);
        }

        private void ContextInfoProvider_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Debug.Assert(sender is ContextInfoProvider);
            EnviroGenDisplay.MainWindow.Instance.RemoveContextInfoTextSafe(((ContextInfoProvider)sender).ContextInfo);
        }
    }
}
