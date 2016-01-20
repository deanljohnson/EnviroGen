using EnviroGen;
using EnviroGen.Coloring;
using EnviroGen.Continents;
using EnviroGen.Erosion;
using EnviroGen.Noise.Modifiers;

namespace EnviroGenDisplay
{
    public interface IEnvironment
    {
        void GenerateTerrain(GenerationOptions data);

        Colorizer GetColorizer();
        void AddColor(ColorRange c);
        void RemoveColor(ColorRange c);
        void ApplyColorizer();

        void ErodeTerrain(IEroder eroder);
        void GenerateContinents(IContinentGenerator generator);

        void ApplyTerrainModifier(IModifier modifier);
        void ApplyTerrainModifierInverted(IInvertableModifier modifier);
    }
}
