using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EnviroGenDisplay.ViewModels;
using EnviroGenDisplay.Views.Nodes;

namespace EnviroGenDisplay.Views
{
    //TODO: This has all sorts of nasty hacks. However, I could not find a better solution and so am stuck with this for now.
    // Problem 1: The view knows it's ViewModel. MVVM be damned I guess...
    // Problem 2: Tracking the right click position like this is very sketchy.
    // Problem 3: Throwing an exception when the right click position is not set correctly is pretty bad.
    /// <summary>
    /// Interaction logic for NodeEditor.xaml
    /// </summary>
    public partial class NodeEditor : UserControl
    {
        private Point? m_RightClickPosition { get; set; }

        private NodeEditorViewModel m_VM;
        private NodeEditorViewModel m_ViewModel => m_VM ?? (m_VM = DataContext as NodeEditorViewModel);

        public NodeEditor()
        {
            InitializeComponent();
        }

        private void NodeMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;

            if (menuItem == null) return;

            var nodeViewModel = m_ViewModel.GetNodeViewModel(menuItem.Header.ToString());

            var nodeViewControl = new NodeView(nodeViewModel);

            if (m_RightClickPosition == null) throw new Exception("Right click position was not properly set on the canvas");
            nodeViewControl.SetValue(Canvas.LeftProperty, m_RightClickPosition?.X);
            nodeViewControl.SetValue(Canvas.TopProperty, m_RightClickPosition?.Y);
            m_RightClickPosition = null;

            NodeCanvas.Children.Add(nodeViewControl);
        }

        private void NodeCanvas_OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            m_RightClickPosition = Mouse.GetPosition(NodeCanvas);
        }
    }
}
