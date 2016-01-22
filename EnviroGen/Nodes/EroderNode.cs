using EnviroGen.Erosion;

namespace EnviroGen.Nodes
{
    public class EroderNode<TEroder> : INode 
        where TEroder : IEroder
    {
        public INode Input { get; set; }
        public INode Output { get; set; }

        public TEroder Eroder { get; set; }

        public void Modify(Environment environment)
        {
            Eroder.Erode(environment.Terrain);

            Output?.Modify(environment);
        }
    }
}
