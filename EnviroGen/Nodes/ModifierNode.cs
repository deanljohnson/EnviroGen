using EnviroGen.Noise.Modifiers;

namespace EnviroGen.Nodes
{
    public class ModifierNode<TModifier> : INode 
        where TModifier : IModifier
    {
        public INode Output { get; set; }

        public TModifier Modifier { get; set; }

        public void Modify(Environment environment)
        {
            Modifier.Modify(environment.Terrain);

            Output?.Modify(environment);
        }
    }
}
