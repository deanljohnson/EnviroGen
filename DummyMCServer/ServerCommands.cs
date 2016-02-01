using System.Collections.Generic;

namespace DummyMCServer
{
    public class ServerCommands
    {
        /// <summary>
        /// A dictionary that maps commands to the length in bytes of the arguments for that command.
        /// </summary>
        public static Dictionary<byte, int> CommandLengths = new Dictionary<byte, int>
        {
            { NULL, 0 },
            { START_WORLD_GEN, 2 },
            { UPDATE_REQUEST, 0 },
            { START_SIMULATING, 0 }
        };

        public static Dictionary<byte, string> CommandNames = new Dictionary<byte, string>
        {
            { NULL, "NULL" },
            { START_WORLD_GEN, "START_WORLD_GEN" },
            { UPDATE_REQUEST, "UPDATE_REQUEST" },
            { START_SIMULATING, "START_SIMULATING" }
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
    }
}