using System;
using EnviroGen.Noise.Modifiers;

namespace EnviroGen.Nodes
{
    public class ModifierNode<TModifier> : INode 
        where TModifier : IModifier
    {
        public INode Output { get; set; }

        public TModifier Modifier { get; set; }

        public event EventHandler Started;
        public event EventHandler Finished;

        public void Modify(Environment environment)
        {
            Started?.Invoke(this, null);
            Modifier.Modify(environment.Terrain);
            Finished?.Invoke(this, null);

            Output?.Modify(environment);
        }
    }
}
