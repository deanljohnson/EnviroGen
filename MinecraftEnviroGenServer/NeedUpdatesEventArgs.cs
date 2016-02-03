using System;
using EnviroGen.HeightMaps;

namespace MinecraftEnviroGenServer
{
    class NeedUpdatesEventArgs : EventArgs
    {
        public HeightMap HeightMap { get; private set; }

        public NeedUpdatesEventArgs(HeightMap map)
        {
            HeightMap = map;
        }
    }
}
