using EnviroGen.Noise.Modifiers;

namespace EnviroGen.Nodes
{
    public class ModifierNode<TModifier> : INode 
        where TModifier : IModifier
    {
        public INode Input { get; set; }
        public INode Output { get; set; }

        public TModifier Modifier { get; set; }

        public void Modify(Environment environment)
        {
            environment.ApplyTerrainModifier(Modifier);

            Output?.Modify(environment);
        }
    }
}
