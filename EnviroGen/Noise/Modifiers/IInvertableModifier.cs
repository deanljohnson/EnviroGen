namespace EnviroGen.Noise.Modifiers
{
    public interface IInvertableModifier : IModifier
    {
        void InvertModify(Environment environment);
    }
}