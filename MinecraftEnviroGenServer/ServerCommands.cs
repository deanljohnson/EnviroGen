using System.Collections.Generic;

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
            { UPDATE_REQUEST, 1 },
            { START_SIMULATING, 0 },
            { GET_CHUNK, 2 },
            { RECEIVE_CHUNK, 32768 },
            { DELETE_BLOCK, 5 },
            { SET_BLOCK, 6 }
        };

        public static Dictionary<byte, string> CommandNames = new Dictionary<byte, string>
        {
            { NULL, "NULL" },
            { START_WORLD_GEN, "START_WORLD_GEN" },
            { UPDATE_REQUEST, "UPDATE_REQUEST" },
            { START_SIMULATING, "START_SIMULATING" },
            { GET_CHUNK, "GET_CHUNK" },
            { RECEIVE_CHUNK, "RECEIVE_CHUNK" },
            { DELETE_BLOCK, "DELETE_BLOCK" },
            { SET_BLOCK, "SET_BLOCK" }
        };

        /// <summary>
        /// Sent by the EnviroGen pipe client to signify an empty command.
        /// Args: {}
        /// </summary>
        public const byte NULL = 0;

        /// <summary>
        /// Sent by the java EnviroGen pipe client when world gen is starting, 
        /// so that initial terrain can all be generated. 
        /// Args: {cx, cy}
        /// </summary>
        public const byte START_WORLD_GEN = 1;

        /// <summary>
        /// Sent by the java EnviroGen pipe client when it is 
        /// ready to accept the next block update.
        /// Args: {numUpdates}
        /// </summary>
        public const byte UPDATE_REQUEST = 2;

        /// <summary>
        /// Sent by the EnviroGen pipe client when world gen is completed(and it has received the data),
        /// and things such as erosion can begin to be simulated.
        /// Args: {}
        /// </summary>
        public const byte START_SIMULATING = 3;

        /// <summary>
        /// This command gets the byte id's of all blocks in a certain chunk.
        /// BlockIDs are flattened into 1d array by a triple nested for-loop
        /// in the order z, x, y.
        /// Args: {cx, cy}
        /// </summary>
        public const byte GET_CHUNK = 4;

        /// <summary>
        /// This is the command returned from EnviroGen when sent GET_CHUNK.
        /// BlockIDs are flattened into 1d array by a triple nested for-loop
        /// in the order z, x, y.
        /// Args: {blockID[0]...blockID[32767]}
        /// </summary>
        public const byte RECEIVE_CHUNK = 5;

        /// <summary>
        /// Sent by EnviroGen as a response to an UPDATE_REQUEST.
        /// Args: {cx, cy, x, y, z}
        /// </summary>
        public const byte DELETE_BLOCK = 6;

        /// <summary>
        /// Sent by EnviroGen as a response to an UPDATE_REQUEST.
        /// Args: {cx, cy, x, y, z, id}
        /// </summary>
        public const byte SET_BLOCK = 7;
    }
}