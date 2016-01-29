using System;
using System.Collections.ObjectModel;
using EnviroGenDisplay.ViewModels;

namespace EnviroGenDisplay
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
}