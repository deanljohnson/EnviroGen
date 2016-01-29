using System;
using EnviroGen.Nodes;

namespace EnviroGenNodeEditor
{
    public interface IEditorNode : INode
    {
        event EventHandler<EditorMouseEventArgs> OnLeftMouseDown;
        event EventHandler<EditorMouseEventArgs> OnLeftMouseUp;
        event EventHandler<NodeDraggedEventArgs> OnNodeDragged;
        event EventHandler<StartConnectionEventArgs> OnStartConnection;
        event EventHandler<EndConnectionEventArgs> OnEndConnection;

        bool Selected { get; set; }

        double X { get; set; }
        double Y { get; set; }
        int Z { get; set; }
    }
}