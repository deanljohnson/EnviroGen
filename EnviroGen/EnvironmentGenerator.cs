using System.Threading;
using EnviroGen.Erosion;
using EnviroGen.RiverGen;

namespace EnviroGen
{
    public class EnvironmentGenerator
    {
        private Thread TerrainThread { get; set; }
        private Thread CloudThread { get; set; }
        private HeightMap TerrainHeightMap { get; set; }
        private HeightMap CloudHeightMap { get; set; }

        public GenerationOptions GenOptions { get; set; }

        public EnvironmentGenerator()
        {
            TerrainThread = new Thread(GenerateTerrainHeightMap);
            CloudThread = new Thread(GenerateCloudHeightMap);
        }

        /// <summary>
        /// Returns an environment generated based off of the current GenOptions property.
        /// </summary>
        public Environment Generate()
        {
            TerrainThread.Start();
            CloudThread.Start();
            TerrainThread.Join();
            CloudThread.Join();

            var terrain = new Terrain(TerrainHeightMap,
                RiverGenerator.GenerateRivers(TerrainHeightMap, 10, .9f, 1f, .3f));
            terrain.Colorize(GenOptions.TerrainColorizer);

            return new Environment(terrain, new Clouds(CloudHeightMap));
        }

        /// <summary>
        /// Generates a HeightMap and assigns TerrainHeightMap to the generated map.
        /// </summary>
        private void GenerateTerrainHeightMap()
        {
            TerrainHeightMap = HeightMapGenerator.GenerateHeightMap(GenOptions.SizeX, GenOptions.SizeY, GenOptions.HeightMapOctaveCount, GenOptions.NoiseRoughness,
                GenOptions.NoiseFrequency, GenOptions.HeightMapSeed);

            ContinentGenerator.BuildContinents(TerrainHeightMap, GenOptions.NumContinents, GenOptions.MinimumContinentSize, GenOptions.MaximumContinentSize);

            ImprovedThermalErosion.Erode(TerrainHeightMap, GenOptions.ErosionAngle, GenOptions.ErosionIterations);

            TerrainHeightMap.Normalize();
        }

        /// <summary>
        /// Generates a HeightMap and assigns CloudHeightMap to the generated map.
        /// </summary>
        private void GenerateCloudHeightMap()
        {
            CloudHeightMap = HeightMapGenerator.GenerateHeightMap(GenOptions.SizeX, GenOptions.SizeY, GenOptions.CloudMapOctaveCount, GenOptions.NoiseRoughness,
                GenOptions.NoiseFrequency, GenOptions.CloudMapSeed);
        }
    }
}
