using System;
using System.Collections.ObjectModel;
using EnviroGenNodeEditor;
using Editor = EnviroGenNodeEditor.NodeEditor<EnviroGenDisplay.ViewModels.NodeViewModel, System.Collections.ObjectModel.ObservableCollection<EnviroGenDisplay.ViewModels.NodeViewModel>,
                                            EnviroGenDisplay.ViewModels.NodeConnectionViewModel, System.Collections.ObjectModel.ObservableCollection<EnviroGenDisplay.ViewModels.NodeConnectionViewModel>>;

namespace EnviroGenDisplay.ViewModels
{
    public class NodeEditorViewModel : ViewModelBase
    {
        public static ObservableCollection<NodeMenuEntry> NodeMenuEntries { get; set; } = App.NodeMenuEntries; 

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
            if (Editor.SelectedNode != null)
                Editor.DeleteNode(Editor.SelectedNode);
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
