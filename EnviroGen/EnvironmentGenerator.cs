using SFML.Window;

namespace EnviroGen
{
    public class EnvironmentGenerator
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int HeightMapOctaveCount { get; set; }
        public int CloudMapOctaveCount { get; set; }
        public int NumContinents { get; set; }
        public int MinimumContinentSize { get; set; }
        public int MaximumContinentSize { get; set; }
        public int SeaLevel { get; set; }
        public int SandDistance { get; set; }
        public int ForestDistance { get; set; }
        public int MountainDistance { get; set; }
        public int HeightMapSeed { get; set; }
        public int CloudMapSeed { get; set; }

        public Environment Generate()
        {
            var heightMap = HeightMapGenerator.GenerateHeightMap(SizeX, SizeY, HeightMapOctaveCount, NumContinents, MinimumContinentSize, MaximumContinentSize, HeightMapSeed);
            var cloudMap = HeightMapGenerator.GenerateHeightMap(SizeX, SizeY, CloudMapOctaveCount, 0, 0, 0, CloudMapSeed);
            
            ContinentGenerator.BuildContinents(heightMap, NumContinents, MinimumContinentSize, MaximumContinentSize);

            heightMap.Normalize();

            return new Environment(new Terrain(heightMap), new Clouds(cloudMap));
        }
    }
}
