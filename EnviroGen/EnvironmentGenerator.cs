using SFML.Window;

namespace EnviroGen
{
    class EnvironmentGenerator
    {
        public Environment Environment;

        public readonly HeightMapGenerator HeightGenerator;
        public readonly CloudGenerator CloudGenerator;

        public EnvironmentGenerator(Vector2u size, int heightOctaveCount, int cloudOctaveCount)
        {
            HeightGenerator = new HeightMapGenerator(size, heightOctaveCount, Program.Random);
            CloudGenerator = new CloudGenerator(size, cloudOctaveCount, Program.Random);
        }

        public void Generate()
        {
            HeightGenerator.GenerateHeightMap();
            CloudGenerator.GenerateCloudMap();

            Environment = new Environment(new Terrain(HeightGenerator.HeightMap), 
                new Clouds(CloudGenerator.CloudMap));
        }
    }
}
