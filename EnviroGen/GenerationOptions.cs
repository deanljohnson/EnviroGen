namespace EnviroGen
{
    public class GenerationOptions
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int OctaveCount { get; set; }
        public int Seed { get; set; }
        public float Gain { get; set; }
        public float Frequency { get; set; }
        public bool CombineWithExisting { get; set; }

        public GenerationOptions()
        {
            SizeX = 1000;
            SizeY = 780;
            OctaveCount = 6;
            Seed = -1;
            Gain = .55f;
            Frequency = .005f;
        }

        public GenerationOptions(GenerationOptions copy)
        {
            SizeX = copy.SizeX;
            SizeY = copy.SizeY;
            OctaveCount = copy.OctaveCount;
            Seed = copy.Seed;
            Gain = copy.Gain;
            Frequency = copy.Frequency;
        }
    }
}
