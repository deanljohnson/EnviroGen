using EnviroGen.Continents;

namespace EnviroGen.Nodes
{
    public class ContinentGeneratorNode<TContinentGenerator> : INode 
        where TContinentGenerator : IContinentGenerator
    {
        public INode Output { get; set; }

        public TContinentGenerator ContinentGenerator { get; set; }

        public void Modify(Environment environment)
        {
            ContinentGenerator.GenerateContinents(environment.Terrain);

            Output?.Modify(environment);
        }
    }
}
