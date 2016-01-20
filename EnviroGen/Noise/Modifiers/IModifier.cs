using EnviroGen.HeightMaps;

namespace EnviroGen.Noise.Modifiers
{
    public interface IModifier
    {
        void Modify(HeightMap map);
    }
}
