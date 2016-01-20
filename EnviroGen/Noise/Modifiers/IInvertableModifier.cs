using EnviroGen.HeightMaps;

namespace EnviroGen.Noise.Modifiers
{
    public interface IInvertableModifier : IModifier
    {
        void InvertModify(HeightMap map);
    }
}