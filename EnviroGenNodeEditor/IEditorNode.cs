using System;
using EnviroGen.Nodes;

namespace EnviroGenNodeEditor
{
    public interface IEditorNode : INode
    {
        EventHandler<EditorMouseEventArgs> OnLeftMouseDown { get; set; }
        EventHandler<EditorMouseEventArgs> OnLeftMouseUp { get; set; }
        EventHandler<NodeDraggedEventArgs> OnNodeDragged { get; set; }
        EventHandler<StartConnectionEventArgs> OnStartConnection { get; set; }
        EventHandler<EndConnectionEventArgs> OnEndConnection { get; set; }

        bool Selected { get; set; }

        double X { get; set; }
        double Y { get; set; }
        int Z { get; set; }
    }
}