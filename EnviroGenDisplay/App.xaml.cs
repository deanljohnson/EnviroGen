using System.Windows;
using EnviroGen;

namespace EnviroGenDisplay
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Environment WorkingEnvironment { get; set; }

        public const string ContinentGeneratorsCategory = "Continent Generators";
        public const string ErosionProcessesCategory = "Erosion Processes";
        public const string ModifiersCategory = "Modifiers";
        public const string ColoringCategory = "Coloring";
        public const string TerrainGeneratorsCategory = "Terrain Generators";

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
