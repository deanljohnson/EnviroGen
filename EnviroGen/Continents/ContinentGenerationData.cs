namespace EnviroGen.Continents
{
    public class ContinentGenerationData
    {
        public int NumContinents { get; set; }
        public int MinimumContinentSize { get; set; }
        public int MaximumContinentSize { get; set; }

        public ContinentGenerationData()
        {
            NumContinents = 0;
            MinimumContinentSize = 0;
            MaximumContinentSize = 0;
        }

        public ContinentGenerationData(int numContinents, int minSize, int maxSize)
        {
            NumContinents = numContinents;
            MinimumContinentSize = minSize;
            MaximumContinentSize = maxSize;
        }
    }
}
