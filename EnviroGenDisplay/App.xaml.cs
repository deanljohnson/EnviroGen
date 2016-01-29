using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using EnviroGenDisplay.ViewModels;
using Environment = EnviroGen.Environment;

namespace EnviroGenDisplay
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Environment WorkingEnvironment { get; set; }

        public static ObservableCollection<NodeMenuEntry> NodeMenuEntries { get; set; } = new ObservableCollection<NodeMenuEntry>();

        public const string ContinentGeneratorsCategory = "Continent Generators";
        public const string ErosionProcessesCategory = "Erosion Processes";
        public const string ModifiersCategory = "Modifiers";
        public const string ColoringCategory = "Coloring";
        public const string TerrainGeneratorsCategory = "Terrain Generators";

        static App()
        {
            ContextProvider.SetContextInfo = SetContextInfo;
            ContextProvider.RemoveContextInfo = RemoveContextInfo;

            LoadPlugins();
            var nvmTypes = GetNodeViewModelTypes();
            CreateMenuEntries(nvmTypes);
        }

        public void AddNodeViewModelDataTemplate(Type nodeViewModelType, Type displayObjectType)
        {
            //TODO: Type Checks!
            //TODO: Or scrap this and auto create the NodeViews....
            var dataTemplate = new DataTemplate(nodeViewModelType)
            {
                VisualTree = new FrameworkElementFactory(displayObjectType)
            };

            Resources.Add(new DataTemplateKey(nodeViewModelType), dataTemplate);
        }

        private static void LoadPlugins()
        {
            
        }

        private static List<Type> GetNodeViewModelTypes()
        {
            var type = typeof(NodeViewModel);
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract).ToList();
        }

        private static void CreateMenuEntries(List<Type> types)
        {
            foreach (var t in types)
            {
                var nodeNameAttribute = t.GetCustomAttribute(typeof(EditorNodeNameAttribute)) as EditorNodeNameAttribute;

                Debug.Assert(nodeNameAttribute != null);

                var nme = NodeMenuEntries.FirstOrDefault(n => n.Header == nodeNameAttribute.Category);

                //Create NodeMenuEntry for category if needed
                if (nme == null)
                {
                    nme = new NodeMenuEntry(nodeNameAttribute.Category, null);
                    NodeMenuEntries.Add(nme);
                }

                //Fixes access to foreach variable in closure warning/possible problem
                var t1 = t;

                nme.ChildMenus.Add(new NodeMenuEntry(nodeNameAttribute.Name, () => Activator.CreateInstance(t1) as NodeViewModel));
            }
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
