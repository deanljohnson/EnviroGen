using System;
using System.Collections.ObjectModel;
using EnviroGenDisplay.ViewModels;

namespace EnviroGenDisplay
{
    public class NodeMenuEntry 
        : MenuEntry<Func<NodeViewModel>>
    {
        public ObservableCollection<NodeMenuEntry> ChildMenus { get; set; }
         
        public NodeMenuEntry(string header, Func<NodeViewModel> onClick)
            : base(header, onClick)
        {
            ChildMenus = new ObservableCollection<NodeMenuEntry>();
        }
    }
}