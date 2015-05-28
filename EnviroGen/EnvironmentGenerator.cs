using SFML.Window;

namespace EnviroGen
{
    class EnvironmentGenerator
    {
        private Vector2i Size { get; set; }
        public Environment Environment;

        public readonly HeightMapGenerator HeightMapGenerator;
        public readonly CloudGenerator CloudGenerator;

        public EnvironmentGenerator(Vector2i size, int heightOctaveCount, int cloudOctaveCount)
        {
            Size = size;
            HeightMapGenerator = new HeightMapGenerator(size, heightOctaveCount, Program.Random);
            CloudGenerator = new CloudGenerator(size, cloudOctaveCount, Program.Random);

            HeightMapGenerator.NumContinents = 1;
            HeightMapGenerator.MinContinentSize = 400;
            HeightMapGenerator.MaxContinentSize = 450;
        }

        public void Generate()
        {
            HeightMapGenerator.GenerateHeightMap();
            CloudGenerator.GenerateCloudMap();

            Environment = new Environment(new Terrain(HeightMapGenerator.HeightMap, Size), 
                new Clouds(CloudGenerator.CloudMap));
        }
    }
}
