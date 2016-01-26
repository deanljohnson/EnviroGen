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
            ContextProvider.SetContextInfo = SetContextInfo;
            ContextProvider.RemoveContextInfo = RemoveContextInfo;
        }

        private static void SetContextInfo(ContextProvider provider)
        {
            EnviroGenDisplay.MainWindow.Instance.SetContextInfoTextSafe(provider.ContextInfo);
        }

        private static void RemoveContextInfo(ContextProvider provider)
        {
            EnviroGenDisplay.MainWindow.Instance.RemoveContextInfoTextSafe(provider.ContextInfo);
        }
    }
}
