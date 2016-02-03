using System;
using System.Diagnostics;
using System.Threading;
using EnviroGen;
using EnviroGen.HeightMaps;
using Substrate;

namespace MinecraftEnviroGenServer
{
    public class EnviroGenServerHandler : ICommandHandler
    {
        private static readonly byte[] EMPTY_CHUNK;
        private static readonly byte[] NULL_RESPONSE = {0};
        private const int CHUNK_SIZE = 16;
        private const int SEA_LEVEL = 63;
        private const int MAX_HEIGHT = 128;

        private DualHeightMap m_HeightMap { get; set; }

        static EnviroGenServerHandler()
        {
            EMPTY_CHUNK = new byte[CHUNK_SIZE * CHUNK_SIZE * MAX_HEIGHT];

            for (var k = 0; k < CHUNK_SIZE; k++)
            {
                var kAmount = k * CHUNK_SIZE * MAX_HEIGHT;
                for (var i = 0; i < CHUNK_SIZE; i++)
                {
                    var iAmount = i * MAX_HEIGHT;

                    //Bedrock Layer
                    for (var j = 0; j < MAX_HEIGHT; j++)
                    {
                        EMPTY_CHUNK[j + iAmount + kAmount] = BlockType.AIR;
                    }
                }
            }
        }

        public byte[] HandleRequest(byte[] request)
        {
            switch (request[0])
            {
                case ServerCommands.START_WORLD_GEN:
                    new Thread(StartGenerateWorld).Start(request);
                    return new[] {ServerCommands.START_WORLD_GEN, request[1], request[2]};
                case ServerCommands.UPDATE_REQUEST:
                    return GetUpdate();
                case ServerCommands.GET_CHUNK:
                    return GetChunk(request[1], request[2]);
            }

            return NULL_RESPONSE;
        }

        private void StartGenerateWorld(object bytes)
        {
            var request = bytes as byte[];

            Debug.Assert(request != null);

            GenerateWorld(request[1], request[2]);
        }

        private void GenerateWorld(byte cx, byte cy)
        {
            var width = cx * CHUNK_SIZE;
            var height = cy * CHUNK_SIZE;

            var genOptions = new GenerationOptions
            {
                Frequency = .003f,
                Roughness = .3f,
                NoiseType = NoiseType.Simplex,
                OctaveCount = 6,
                Seed = new Random().Next(10000),
                SizeX = width,
                SizeY = height
            };

            var floatMap = HeightMapGenerator.GenerateHeightMap(genOptions);
            floatMap.Normalize(0, MAX_HEIGHT);

            if (m_HeightMap == null)
            {
                m_HeightMap = new DualHeightMap(floatMap);
                return;
            }

            lock (m_HeightMap)
            {
                m_HeightMap = new DualHeightMap(floatMap);
            }
        }

        private byte[] GetChunk(byte cx, byte cz)
        {
            var initialX = cx * CHUNK_SIZE;
            var initialZ = cz * CHUNK_SIZE;

            if (initialX > m_HeightMap.Size.X || initialZ > m_HeightMap.Size.Y)
            {
                return EMPTY_CHUNK;
            }

            var blockIDs = new byte[MAX_HEIGHT * CHUNK_SIZE * CHUNK_SIZE];
            for (var k = 0; k < CHUNK_SIZE; k++)
            {
                var kAmount = k * CHUNK_SIZE * MAX_HEIGHT;
                for (var i = 0; i < CHUNK_SIZE; i++)
                {
                    var iAmount = i * MAX_HEIGHT;
                    var height = m_HeightMap.ByteMap[i + initialX, k + initialZ];

                    //Bedrock Layer
                    for (var j = 0; j < 2; j++)
                    {
                        blockIDs[j + iAmount + kAmount] = BlockType.BEDROCK;
                    }

                    //Stone Layer
                    for (var j = 2; j < height - 5; j++)
                    {
                        blockIDs[j + iAmount + kAmount] = BlockType.STONE;
                    }

                    //Dirt/Grass Layer
                    for (var j = height > 5 ? height - 5 : 0; j < height; j++)
                    {
                        if (j == height - 1 && j >= SEA_LEVEL)
                        {
                            blockIDs[j + iAmount + kAmount] = BlockType.GRASS;
                        }
                        else
                        {
                            blockIDs[j + iAmount + kAmount] = BlockType.DIRT;
                        }
                    }

                    //Sand/Sea Level Layer
                    if (height <= SEA_LEVEL)
                    {
                        blockIDs[height + iAmount + kAmount] = BlockType.SAND;
                        for (var j = height + 1; j < SEA_LEVEL; j++)
                        {
                            blockIDs[j + iAmount + kAmount] = BlockType.WATER;
                        }
                    }

                    //Air
                    for (var j = height > SEA_LEVEL ? height + 1 : SEA_LEVEL + 1; j < MAX_HEIGHT; j++)
                    {
                        blockIDs[j + iAmount + kAmount] = BlockType.AIR;
                    }
                }
            }

            var chunkResponse = new byte[1 + blockIDs.Length];
            chunkResponse[0] = ServerCommands.RECEIVE_CHUNK;
            blockIDs.CopyTo(chunkResponse, 1);

            return chunkResponse;
        }

        private byte[] GetUpdate()
        {
            return new[] {ServerCommands.UPDATE_REQUEST};
        }
    }
}