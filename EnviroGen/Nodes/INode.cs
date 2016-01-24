namespace EnviroGen.Nodes
{
    public interface INode
    {
        INode Output { get; set; }

        void Modify(Environment environment);
    }
}