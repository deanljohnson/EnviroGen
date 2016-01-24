using EnviroGen.Coloring;

namespace EnviroGen.Nodes
{
    public class ColorizerNode<TColorizer> : INode 
        where TColorizer : IColorizer
    {
        public INode Output { get; set; }

        public TColorizer Colorizer { get; set; }

        public void Modify(Environment environment)
        {
            environment.Terrain.Colorize(Colorizer);

            Output?.Modify(environment);
        }
    }
}
