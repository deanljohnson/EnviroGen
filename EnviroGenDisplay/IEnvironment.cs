using EnviroGen.Coloring;
using EnviroGen.Continents;
using EnviroGen.Erosion;

namespace EnviroGenDisplay
{
    interface IEnvironment
    {
        void GenerateTerrain(EnvironmentData data);

        Colorizer GetColorizer();
        void AddColor(ColorRange c);
        void RemoveColor(ColorRange c);
        void ApplyColorizer();

        void ErodeTerrain(IEroder eroder);
        void GenerateContinents(IContinentGenerator generator);
    }
}
