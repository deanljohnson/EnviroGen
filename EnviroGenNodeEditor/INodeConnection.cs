using EnviroGen.Nodes;

namespace EnviroGenNodeEditor
{
    public interface INodeConnection<TNode>
        where TNode : INode
    {
        TNode Source { get; set; }
        TNode Destination { get; set; }

        double SourceX { get; set; }
        double SourceY { get; set; }

        double DestX { get; set; }
        double DestY { get; set; }
    }
}