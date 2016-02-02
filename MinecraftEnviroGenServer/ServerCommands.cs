using System.Collections.Generic;
using System.Security.Policy;

namespace MinecraftEnviroGenServer
{
    public static class ServerCommands
    {
        /// <summary>
        /// A dictionary that maps commands to the length in bytes of the arguments for that command.
        /// </summary>
        public static Dictionary<byte, int> CommandLengths = new Dictionary<byte, int>
        {
            { NULL, 0 },
            { START_WORLD_GEN, 2 },
            { UPDATE_REQUEST, 0 },
            { START_SIMULATING, 0 },
            { GET_CHUNK, 2 },
            { RECEIVE_CHUNK, 32768 }
        };

        public static Dictionary<byte, string> CommandNames = new Dictionary<byte, string>
        {
            { NULL, "NULL" },
            { START_WORLD_GEN, "START_WORLD_GEN" },
            { UPDATE_REQUEST, "UPDATE_REQUEST" },
            { START_SIMULATING, "START_SIMULATING" },
            { GET_CHUNK, "GET_CHUNK" },
            { RECEIVE_CHUNK, "RECEIVE_CHUNK" }
        };

        /// <summary>
        /// Sent by the EnviroGen pipe client to signify an empty field
        /// </summary>
        public const byte NULL = 0;

        /// <summary>
        /// Sent by the java EnviroGen pipe client when world gen is starting, 
        /// so that initial terrain can all be generated.
        /// </summary>
        public const byte START_WORLD_GEN = 1;

        /// <summary>
        /// Sent by the java EnviroGen pipe client when it is 
        /// ready to accept the next block update.
        /// </summary>
        public const byte UPDATE_REQUEST = 2;

        /// <summary>
        /// Sent by the EnviroGen pipe client when world gen is completed(and it has received the data),
        /// and things such as erosion can begin to be simulated.
        /// </summary>
        public const byte START_SIMULATING = 3;

        /// <summary>
        /// This command gets the byte id's of all blocks in a certain chunk.
        /// The block id's should be read by a triple nested for-loop
        /// with z as the outer most, y as the middle, and x as the inner.
        /// Also, note that EnviroGen only generates to height 128
        /// </summary>
        public const byte GET_CHUNK = 4;

        /// <summary>
        /// This is the command returned from EnviroGen when sent GET_CHUNK
        /// </summary>
        public const byte RECEIVE_CHUNK = 5;
    }
}