using System;
using EnviroGen.Erosion;

namespace EnviroGen.Nodes
{
    /// <summary>
    /// A node that can apply an eroder to a HeightMap
    /// </summary>
    public class EroderNode<TEroder> : INode 
        where TEroder : IEroder
    {
        public INode Output { get; set; }

        public TEroder Eroder { get; set; }

        public event EventHandler Started;
        public event EventHandler Finished;

        public void Modify(Environment environment)
        {
            Started?.Invoke(this, null);
            Eroder.Erode(environment.Terrain);
            Finished?.Invoke(this, null);

            Output?.Modify(environment);
        }
    }
}
