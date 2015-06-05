namespace EnviroGen.Continents
{
    public class ContinentGenerationData
    {
        public int NumContinents { get; set; }
        public int MinimumContinentSize { get; set; }
        public int MaximumContinentSize { get; set; }
        public float Scale { get; set; }

        public ContinentGenerationData()
            : this(0, 0, 0, 2.5f)
        {
        }

        public ContinentGenerationData(int numContinents, int minSize, int maxSize, float scale)
        {
            NumContinents = numContinents;
            MinimumContinentSize = minSize;
            MaximumContinentSize = maxSize;
            Scale = scale;
        }
    }
}
