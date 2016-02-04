using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using EnviroGen;
using EnviroGen.HeightMaps;
using Substrate;
using Environment = EnviroGen.Environment;

namespace MinecraftEnviroGenServer
{
    public class EnviroGenServerHandler : ICommandHandler
    {
        private static readonly byte[] EMPTY_CHUNK;
        private static readonly byte[] NULL_RESPONSE = {0};
        private const int CHUNK_SIZE = 16;
        private const int SEA_LEVEL = 63;
        private const int MAX_HEIGHT = 128;
        private const int NORMALIZE_LOW = 24;
        private const int NORMALIZE_HIGH = 96;

        private Environment m_Environment { get; } = new Environment();

        private Queue<byte[]> m_WaitingUpdates { get; } = new Queue<byte[]>();

        public IEnvironmentUpdater EnvironmentUpdater { get; set; }

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
                Frequency = .007f,
                Roughness = .4f,
                NoiseType = NoiseType.Simplex,
                OctaveCount = 6,
                //By default we will just pick a random seed, cap at 10,000 because really high seeds cause problems during noise generation
                Seed = new Random().Next(10000), 
                SizeX = width,
                SizeY = height
            };

            var floatMap = HeightMapGenerator.GenerateHeightMap(genOptions);
            floatMap.Normalize(NORMALIZE_LOW, NORMALIZE_HIGH);

            if (m_Environment.Terrain == null)
            {
                m_Environment.Terrain = new DualHeightMap(floatMap)
                {
                    ByteChangeAction = OnByteChange
                };
                return;
            }

            lock (m_Environment.Terrain)
            {
                m_Environment.Terrain = new DualHeightMap(floatMap)
                {
                    ByteChangeAction = OnByteChange
                };
            }
        }

        //TODO: We could apply all SET_BLOCK and DELETE_BLOCK commands currently waiting.
        private byte[] GetChunk(byte cx, byte cz)
        {
            var initialX = cx * CHUNK_SIZE;
            var initialZ = cz * CHUNK_SIZE;

            if (initialX > m_Environment.Terrain.Size.X || initialZ > m_Environment.Terrain.Size.Y)
            {
                return EMPTY_CHUNK;
            }

            var dualHeightMap = (DualHeightMap) m_Environment.Terrain;
            var blockIDs = new byte[MAX_HEIGHT * CHUNK_SIZE * CHUNK_SIZE];
            for (var k = 0; k < CHUNK_SIZE; k++)
            {
                var kAmount = k * CHUNK_SIZE * MAX_HEIGHT;
                for (var i = 0; i < CHUNK_SIZE; i++)
                {
                    var iAmount = i * MAX_HEIGHT;
                    var height = dualHeightMap.GetByte(i + initialX, k + initialZ);

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

                    //TODO: Find a way to avoid setting these - it is needless computation because MC will automatically place air
                    //We have to do this right now because our current command structure requires that each side know how many bytes are coming/going
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

        private void OnByteChange(int x, int z, byte oldHeight, byte newHeight)
        {
            var cx = (byte)(x / CHUNK_SIZE);
            var cz = (byte)(z / CHUNK_SIZE);

            var xInChunk = (byte)(x % CHUNK_SIZE);
            var zInChunk = (byte)(z % CHUNK_SIZE);

            var newID = IDByHeight(newHeight);

            //If terrain got lower, delete blocks on top
            if (oldHeight > newHeight)
            {
                var height = oldHeight;
                lock (m_WaitingUpdates)
                {
                    while (height > newHeight)
                    {
                        m_WaitingUpdates.Enqueue(new[] { ServerCommands.DELETE_BLOCK,
                                                         cx, cz, xInChunk, height, zInChunk });
                        height--;
                    }
                }
            }
            //If terrain got higher, add blocks below
            else if (oldHeight < newHeight)
            {
                var height = newHeight;
                lock (m_WaitingUpdates)
                {
                    while (height > oldHeight)
                    {
                        m_WaitingUpdates.Enqueue(new[] { ServerCommands.SET_BLOCK, cx, cz,
                                                         xInChunk, newHeight, zInChunk, newID });
                        height--;
                    }
                }
                
            }
        }

        //TODO: This isnt physically correct behvaior, or even accurate with how the simulated erosion works
        private byte IDByHeight(byte height)
        {
            if (height <= SEA_LEVEL)
            {
                return BlockType.SAND;
            }

            return BlockType.GRASS;
        }

        private byte[] GetUpdate()
        {
            lock (m_WaitingUpdates)
            {
                if (m_WaitingUpdates.Any())
                {
                    return m_WaitingUpdates.Dequeue();
                }
            }

            EnvironmentUpdater.DoUpdate(m_Environment);

            return new[] {ServerCommands.NULL};
        }
    }
}