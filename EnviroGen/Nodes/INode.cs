namespace EnviroGen.Nodes
{
    public interface INode
    {
        INode Input { get; set; }
        INode Output { get; set; }

        void Modify(Environment environment);
    }
}