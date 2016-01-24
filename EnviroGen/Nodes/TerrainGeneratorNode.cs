using EnviroGen.HeightMaps;

namespace EnviroGen.Nodes
{
    public class TerrainGeneratorNode : GenerationOptions, INode
    {
        public INode Input { get; set; }
        public INode Output { get; set; }

        public TerrainGeneratorNode()
        {
        }

        public TerrainGeneratorNode(GenerationOptions options)
            : base(options)
        {
        }

        public void Modify(Environment environment)
        {
            environment.Terrain = new Terrain(HeightMapGenerator.GenerateHeightMap(this));
            Output?.Modify(environment);
        }
    }
}
