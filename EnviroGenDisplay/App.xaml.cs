﻿using System;
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

        public App()
        {
            ContextProvider.SetContextInfo = SetContextInfo;
            ContextProvider.RemoveContextInfo = RemoveContextInfo;

            LoadPlugins();
            var nvmTypes = GetNodeViewModelTypes();
            CreateUIInformation(nvmTypes);
        }

        private void LoadPlugins()
        {
            
        }

        private List<Type> GetNodeViewModelTypes()
        {
            var type = typeof(NodeViewModel);
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract).ToList();
        }

        private void CreateUIInformation(List<Type> types)
        {
            foreach (var t in types)
            {
                var editorNodeAttribute = t.GetCustomAttribute(typeof(EditorNodeAttribute)) as EditorNodeAttribute;

                Debug.Assert(editorNodeAttribute != null);

                CreateMenuEntry(editorNodeAttribute.Name, editorNodeAttribute.Category, t);
                CreateViewResource(t, editorNodeAttribute.DisplayControlType);
            }
        }

        private void CreateMenuEntry(string name, string cat, Type t)
        {
            var nme = NodeMenuEntries.FirstOrDefault(n => n.Header == cat);

            //Create NodeMenuEntry for category if needed
            if (nme == null)
            {
                nme = new NodeMenuEntry(cat, null);
                NodeMenuEntries.Add(nme);
            }

            nme.ChildMenus.Add(new NodeMenuEntry(name, () => Activator.CreateInstance(t) as NodeViewModel));
        }

        private void CreateViewResource(Type viewModelType, Type displayControlType)
        {
            var dataTemplate = new DataTemplate(viewModelType)
            {
                VisualTree = new FrameworkElementFactory(displayControlType)
            };

            Resources.Add(new DataTemplateKey(viewModelType), dataTemplate);
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
