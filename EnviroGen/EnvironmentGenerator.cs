using System;
using System.ComponentModel;
using System.Threading;

namespace EnviroGen
{
    public class EnvironmentGenerator
    {
        private Thread TerrainThread { get; set; }
        private Thread CloudThread { get; set; }
        private HeightMap TerrainHeightMap { get; set; }
        private HeightMap CloudHeightMap { get; set; }

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
        public float NoiseRoughness { get; set; }
        public float NoiseScale { get; set; }

        public EnvironmentGenerator()
        {
            TerrainThread = new Thread(GenerateTerrainHeightMap);
            CloudThread = new Thread(GenerateCloudHeightMap);
        }

        public Environment Generate()
        {
            TerrainThread.Start();
            CloudThread.Start();
            TerrainThread.Join();
            CloudThread.Join();

            return new Environment(new Terrain(TerrainHeightMap), new Clouds(CloudHeightMap));
        }

        private void GenerateTerrainHeightMap()
        {
            TerrainHeightMap = HeightMapGenerator.GenerateHeightMap(SizeX, SizeY, HeightMapOctaveCount, NoiseRoughness,
                NoiseScale, HeightMapSeed);

            ContinentGenerator.BuildContinents(TerrainHeightMap, NumContinents, MinimumContinentSize, MaximumContinentSize);

            TerrainHeightMap.Normalize();
        }

        private void GenerateCloudHeightMap()
        {
            CloudHeightMap = HeightMapGenerator.GenerateHeightMap(SizeX, SizeY, CloudMapOctaveCount, NoiseRoughness,
                NoiseScale, CloudMapSeed);
        }
    }
}
