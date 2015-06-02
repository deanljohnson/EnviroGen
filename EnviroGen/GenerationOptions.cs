using EnviroGen.Coloring;

namespace EnviroGen
{
    public class GenerationOptions
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int HeightMapOctaveCount { get; set; }
        public int CloudMapOctaveCount { get; set; }
        public int NumContinents { get; set; }
        public int MinimumContinentSize { get; set; }
        public int MaximumContinentSize { get; set; }
        public float SeaLevel { get; set; }
        public float SandDistance { get; set; }
        public float ForestDistance { get; set; }
        public float MountainDistance { get; set; }
        public int HeightMapSeed { get; set; }
        public int CloudMapSeed { get; set; }
        public float NoiseRoughness { get; set; }
        public float NoiseFrequency { get; set; }
        public float ErosionAngle { get; set; }
        public int ErosionIterations { get; set; }
        public Colorizer TerrainColorizer { get; set; }

        public GenerationOptions()
        {
            SizeX = 1400;
            SizeY = 800;
            HeightMapOctaveCount = 8;
            CloudMapOctaveCount = 8;
            NumContinents = 1;
            MinimumContinentSize = 400;
            MaximumContinentSize = 450;
            SeaLevel = .4f;
            SandDistance = .45f;
            ForestDistance = .75f;
            MountainDistance = 1f;
            HeightMapSeed = -1;
            CloudMapSeed = -1;
            NoiseRoughness = .55f;
            NoiseFrequency = .005f;
            ErosionAngle = .022f;
            ErosionIterations = 40;
            TerrainColorizer = new Colorizer();
        }
    }
}
