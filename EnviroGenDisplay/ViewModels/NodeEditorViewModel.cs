using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using EnviroGenNodeEditor;
using Editor = EnviroGenNodeEditor.NodeEditor<EnviroGenDisplay.ViewModels.NodeViewModel, System.Collections.ObjectModel.ObservableCollection<EnviroGenDisplay.ViewModels.NodeViewModel>,
                                            EnviroGenDisplay.ViewModels.NodeConnectionViewModel, System.Collections.ObjectModel.ObservableCollection<EnviroGenDisplay.ViewModels.NodeConnectionViewModel>>;

namespace EnviroGenDisplay.ViewModels
{
    public class NodeMenuEntry
    {
        public string Header { get; }

        public ObservableCollection<NodeMenuEntry> ChildMenus { get; set; }
        public Func<NodeViewModel> NodeCreator { get; }

        public NodeMenuEntry(string header, Func<NodeViewModel> nodeCreator)
        {
            Header = header;
            ChildMenus = new ObservableCollection<NodeMenuEntry>();
            NodeCreator = nodeCreator;
        }
    }

    public class NodeEditorViewModel : ViewModelBase
    {
        public static ObservableCollection<NodeMenuEntry> NodeMenuEntries { get; set; } = new ObservableCollection<NodeMenuEntry>(); 

        static NodeEditorViewModel()
        {
            var type = typeof(NodeViewModel);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);

            foreach (var t in types)
            {
                var nodeNameAttribute = t.GetCustomAttribute(typeof (EditorNodeNameAttribute)) as EditorNodeNameAttribute;

                Debug.Assert(nodeNameAttribute != null);

                var nme = NodeMenuEntries.FirstOrDefault(n => n.Header == nodeNameAttribute.Category);

                if (nme == null)
                {
                    nme = new NodeMenuEntry(nodeNameAttribute.Category, null);
                    NodeMenuEntries.Add(nme);
                }
                
                nme.ChildMenus.Add(new NodeMenuEntry(nodeNameAttribute.Name, () => Activator.CreateInstance(t) as NodeViewModel));
            }
        }

        public Editor Editor { get; set; }

        public ObservableCollection<NodeViewModel> Nodes
        {
            get { return Editor.Nodes; }
            set { Editor.Nodes = value; }
        }

        public ObservableCollection<NodeConnectionViewModel> NodeConnections
        {
            get { return Editor.NodeConnections; }
            set { Editor.NodeConnections = value; }
        }

        public NodeEditorViewModel(Editor editor)
        {
            Editor = editor;

            Nodes = new ObservableCollection<NodeViewModel>();
            NodeConnections = new ObservableCollection<NodeConnectionViewModel>();
        }

        public void OnDeleteSelectedNodeEvent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnCreateNodeEvent(object sender, CreateNodeEventArgs e)
        {
            if (e.MenuEntry.NodeCreator == null) return;

            var nodeViewModel = e.MenuEntry.NodeCreator();

            nodeViewModel.X = e.X;
            nodeViewModel.Y = e.Y;

            Editor.AddNode(nodeViewModel);
        }

        public void OnEditorMouseButtonEvent(object sender, EditorMouseEventArgs e)
        {
            Editor.PushMouseButtonEvent(e);
        }

        public void OnEditorMouseMoveEvent(object sender, EditorMouseEventArgs e)
        {
            Editor.PushMouseMoveEvent(e);
        }
    }
}
