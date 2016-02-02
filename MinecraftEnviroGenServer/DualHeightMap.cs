using EnviroGen.HeightMaps;

namespace MinecraftEnviroGenServer
{
    class DualHeightMap : HeightMap
    {
        public byte[,] ByteMap { get; private set; }

        public override float this[int x, int y]
        {
            get { return base[x, y]; }
            set
            {
                base[x, y] = value;
                ByteMap[x, y] = (byte) value;
            }
        }

        public DualHeightMap(float[,] map) 
            : base(map)
        {
            ByteMap = HeightMapToBytes();
        }

        public DualHeightMap(HeightMap map) 
            : base(map)
        {
            ByteMap = HeightMapToBytes();
        }

        public override void Normalize(float min = 0, float max = 1)
        {
            base.Normalize(min, max);

            ByteMap = HeightMapToBytes();
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
