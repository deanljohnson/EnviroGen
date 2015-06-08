namespace EnviroGen.Noise.Modifiers
{
    public class ModifierOptions
    {
        public float Scale { get; set; }
        public float Exponent { get; set; }
        public bool Ridged { get; set; }

        public ModifierOptions()
        {
            Scale = 1f;
            Exponent = 1f;
            Ridged = false;
        }
    }
}
