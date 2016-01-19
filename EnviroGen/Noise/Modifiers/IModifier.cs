namespace EnviroGen.Noise.Modifiers
{
    public interface IModifier
    {
        void Modify(ref float[,] map);
    }
}
