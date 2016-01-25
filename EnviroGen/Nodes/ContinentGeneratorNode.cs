using System;
using EnviroGen.Continents;

namespace EnviroGen.Nodes
{
    public class ContinentGeneratorNode<TContinentGenerator> : INode 
        where TContinentGenerator : IContinentGenerator
    {
        public INode Output { get; set; }

        public TContinentGenerator ContinentGenerator { get; set; }

        public event EventHandler Started;
        public event EventHandler Finished;

        public void Modify(Environment environment)
        {
            Started?.Invoke(this, null);
            ContinentGenerator.GenerateContinents(environment.Terrain);
            Finished?.Invoke(this, null);

            Output?.Modify(environment);
        }
    }
}
