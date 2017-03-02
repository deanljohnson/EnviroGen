namespace EnviroGen
{
    public enum NoiseType
    {
        Simplex
    }

    public class GenerationOptions
    {
        private const int DEFUALT_SIZE_X = 1000;
        private const int DEFUALT_SIZE_Y = 780;
        private const int DEFUALT_OCTAVE_COUNT = 6;
        private const int DEFAULT_SEED = -1;
        private const float DEFAULT_ROUGHNESS = .55f;
        private const float DEFAULT_FREQUENCY = .005f;

        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int OctaveCount { get; set; }
        public int Seed { get; set; }
        public float Roughness { get; set; }
        public float Frequency { get; set; }

        public NoiseType NoiseType { get; set; } = NoiseType.Simplex;

        public GenerationOptions()
        {
            SizeX = DEFUALT_SIZE_X;
            SizeY = DEFUALT_SIZE_Y;
            OctaveCount = DEFUALT_OCTAVE_COUNT;
            Seed = DEFAULT_SEED;
            Roughness = DEFAULT_ROUGHNESS;
            Frequency = DEFAULT_FREQUENCY;
        }

        public GenerationOptions(GenerationOptions copy)
        {
            SizeX = copy.SizeX;
            SizeY = copy.SizeY;
            OctaveCount = copy.OctaveCount;
            Seed = copy.Seed;
            Roughness = copy.Roughness;
            Frequency = copy.Frequency;
            NoiseType = copy.NoiseType;
        }
    }
}
