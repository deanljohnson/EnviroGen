using System.Windows;
using EnviroGenDisplay.Views;

namespace EnviroGenDisplay
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            ContextInfoProvider.SetContextInfo = SetContextInfo;
            ContextInfoProvider.RemoveContextInfo = RemoveContextInfo;
        }

        private static void SetContextInfo(ContextInfoProvider provider)
        {
            EnviroGenDisplay.MainWindow.Instance.SetContextInfoTextSafe(provider.ContextInfo);
        }

        private static void RemoveContextInfo(ContextInfoProvider provider)
        {
            EnviroGenDisplay.MainWindow.Instance.RemoveContextInfoTextSafe(provider.ContextInfo);
        }
    }
}
