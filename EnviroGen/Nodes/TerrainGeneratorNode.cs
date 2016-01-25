using System;
using EnviroGen.HeightMaps;

namespace EnviroGen.Nodes
{
    public class TerrainGeneratorNode : GenerationOptions, INode
    {
        public INode Output { get; set; }

        public TerrainGeneratorNode()
        {
        }

        public TerrainGeneratorNode(GenerationOptions options)
            : base(options)
        {
        }

        public event EventHandler Started;
        public event EventHandler Finished;

        public void Modify(Environment environment)
        {
            Started?.Invoke(this, null);
            environment.Terrain = new Terrain(HeightMapGenerator.GenerateHeightMap(this));
            Finished?.Invoke(this, null);

            Output?.Modify(environment);
        }
    }
}
