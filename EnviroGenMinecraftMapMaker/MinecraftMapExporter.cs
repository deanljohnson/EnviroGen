using System;
using System.IO;
using EnviroGen.HeightMaps;
using EnviroGen.Noise.Modifiers;
using Substrate;
using Substrate.Core;
using Substrate.Nbt;
using Environment = EnviroGen.Environment;

namespace EnviroGenMinecraftMapMaker
{
    public class MinecraftMapExporter : IModifier
    {
        private const int CHUNK_SIZE = 16;
        private const int MAX_TERRAIN_HEIGHT = 128;

        public string Path { get; set; } = @"C:\Users\Dean\AppData\Roaming\.minecraft\saves\EnviroGenExport\";
        public string Name { get; set; }

        static MinecraftMapExporter()
        {
            NbtVerifier.InvalidTagType += (e) =>
            {
                throw new Exception("Invalid Tag Type: " + e.TagName + " [" + e.Tag + "]");
            };
            NbtVerifier.InvalidTagValue += (e) =>
            {
                throw new Exception("Invalid Tag Value: " + e.TagName + " [" + e.Tag + "]");
            };
            NbtVerifier.MissingTag += (e) =>
            {
                throw new Exception("Missing Tag: " + e.TagName);
            };
        }

        public void Modify(Environment environment)
        {
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
            else
            {
                Directory.Delete(Path, true);
                Directory.CreateDirectory(Path);
            }

            NbtWorld world = AnvilWorld.Create(Path);
            world.Level.LevelName = Name;

            //Make sure we can create complete chunks
            var terrain = environment.Terrain.SizeTruncatedToMultiple(CHUNK_SIZE);

            //Normalize to Minecraft height ranges
            terrain.Normalize(0, MAX_TERRAIN_HEIGHT);

            //Convert to integers to better map to MC's values
            var intMap = HeightMapToIntegers(terrain);

            var chunkManager = world.GetChunkManager();
            var chunksX = terrain.Size.X / CHUNK_SIZE;
            var chunksZ = terrain.Size.Y / CHUNK_SIZE; //In MC, z is a horizontal axis and y is vertical

            
            //Build chunks
            for (var cz = 0; cz < chunksZ; cz++)
            {
                for (var cx = 0; cx < chunksX; cx++)
                {
                    var chunk = chunkManager.CreateChunk(cx, cz);

                    chunk.IsTerrainPopulated = true;
                    chunk.Blocks.AutoLight = false;

                    BuildChunk(chunk, intMap, cx * CHUNK_SIZE, cz * CHUNK_SIZE);

                    chunk.Blocks.RebuildHeightMap();
                    chunk.Blocks.RebuildBlockLight();
                    chunk.Blocks.RebuildSkyLight();

                    chunkManager.Save();
                }
            }

            world.Level.GameType = GameType.CREATIVE;

            var spawnX = (chunksX / 2) * CHUNK_SIZE;
            var spawnZ = (chunksZ / 2) * CHUNK_SIZE;
            world.Level.Spawn = new SpawnPoint(spawnX, intMap[spawnX, spawnZ] + 2, spawnZ);

            world.Save();
        }

        private void BuildChunk(IChunk chunk, int[,] heightMap, int startingX, int startingZ)
        {
            for (var z = 0; z < CHUNK_SIZE; z++)
            {
                for (var x = 0; x < CHUNK_SIZE; x++)
                {
                    var height = heightMap[x + startingX, z + startingZ];
                    for (var y = 0; y < height; y++)
                    {
                        chunk.Blocks.SetID(x, y, z, BlockType.DIRT);
                    }
                }
            }
        }

        /// <summary>
        /// Takes the given HeightMap and truncates the float values to return an int[,]
        /// </summary>
        private static int[,] HeightMapToIntegers(HeightMap map)
        {
            var intMap = new int[map.Size.X, map.Size.Y];
            for (var y = 0; y < map.Size.Y; y++)
            {
                for (var x = 0; x < map.Size.X; x++)
                {
                    intMap[x, y] = (int)map[x, y];
                }
            }

            return intMap;
        }
    }
}