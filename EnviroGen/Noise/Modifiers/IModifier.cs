namespace EnviroGen.Noise.Modifiers
{
    public interface IModifier
    {
        void InvertModify(ref float[,] map);
    }
}
