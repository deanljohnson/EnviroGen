namespace EnviroGen.Noise.Modifiers
{
    public interface IInvertableModifier : IModifier
    {
        void InvertModify(ref float[,] map);
    }
}