using System;
using EnviroGen.HeightMaps;

namespace MinecraftEnviroGenServer
{
    /// <summary>
    /// A class that keeps a byte float representation of the same HeightMap.
    /// Also tracks updates to the byte map.
    /// </summary>
    class DualHeightMap : HeightMap
    {
        private byte[,] m_ByteMap { get; set; }

        public Action<int, int, byte, byte> ByteChangeAction { get; set; }

        public override float this[int x, int y]
        {
            get { return base[x, y]; }
            set
            {
                base[x, y] = value;

                var newByte = (byte) value;
                if (m_ByteMap[x, y] == newByte) return;

                ByteChangeAction?.Invoke(x, y, m_ByteMap[x, y], newByte);
                m_ByteMap[x, y] = newByte;
            }
        }

        public DualHeightMap(float[,] map) 
            : base(map)
        {
            m_ByteMap = HeightMapToBytes();
        }

        public DualHeightMap(HeightMap map) 
            : base(map)
        {
            m_ByteMap = HeightMapToBytes();
        }

        public byte GetByte(int x, int y)
        {
            return m_ByteMap[x, y];
        }

        public override void Normalize(float min = 0, float max = 1)
        {
            base.Normalize(min, max);

            m_ByteMap = HeightMapToBytes();
        }

        /// <summary>
        /// Takes this HeightMap and truncates the float values to return an int[,]
        /// </summary>
        private byte[,] HeightMapToBytes()
        {
            var byteMap = new byte[Size.X, Size.Y];
            for (var y = 0; y < Size.Y; y++)
            {
                for (var x = 0; x < Size.X; x++)
                {
                    byteMap[x, y] = (byte)this[x, y];
                }
            }

            return byteMap;
        }
    }
}
