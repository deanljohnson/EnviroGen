using System;
using EnviroGen.Coloring;

namespace EnviroGen.Nodes
{
    public class ColorizerNode<TColorizer> : INode 
        where TColorizer : IColorizer
    {
        public INode Output { get; set; }

        public TColorizer Colorizer { get; set; }

        public event EventHandler Started;
        public event EventHandler Finished;

        public void Modify(Environment environment)
        {
            Started?.Invoke(this, null);
            environment.Terrain.Colorizer = Colorizer;
            Finished?.Invoke(this, null);

            Output?.Modify(environment);
        }
    }
}
