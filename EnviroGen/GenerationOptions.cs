namespace EnviroGen
{
    public enum NoiseType
    {
        Simplex
    }

    public class GenerationOptions
    {
        public static int DefualtSizeX { get; set; } = 1000;
        public static int DefualtSizeY { get; set; } = 780;
        public static int DefualtOctaveCount { get; set; } = 6;
        public static int DefaultSeed { get; set; } = -1;
        public static float DefaultRoughness { get; set; } = .55f;
        public static float DefaultFrequency { get; set; } = .005f;

        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int OctaveCount { get; set; }
        public int Seed { get; set; }
        public float Gain { get; set; }
        public float Frequency { get; set; }

        public NoiseType NoiseType { get; set; } = NoiseType.Simplex;

        public GenerationOptions()
        {
            SizeX = DefualtSizeX;
            SizeY = DefualtSizeY;
            OctaveCount = DefualtOctaveCount;
            Seed = DefaultSeed;
            Gain = DefaultRoughness;
            Frequency = DefaultFrequency;
        }

        public GenerationOptions(GenerationOptions copy)
        {
            SizeX = copy.SizeX;
            SizeY = copy.SizeY;
            OctaveCount = copy.OctaveCount;
            Seed = copy.Seed;
            Gain = copy.Gain;
            Frequency = copy.Frequency;
            NoiseType = copy.NoiseType;
        }
    }
}
