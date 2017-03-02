using System;
using EnviroGen.Noise.Modifiers;

namespace EnviroGen.Nodes
{
    /// <summary>
    /// A node that can apply a mathematical modifier to an Environment
    /// </summary>
    public class ModifierNode<TModifier> : INode 
        where TModifier : IModifier
    {
        public INode Output { get; set; }

        public TModifier Modifier { get; set; }

        public event EventHandler Started;
        public event EventHandler Finished;

        public virtual void Modify(Environment environment)
        {
            Started?.Invoke(this, null);
            Modifier.Modify(environment);
            Finished?.Invoke(this, null);

            Output?.Modify(environment);
        }
    }
}
