using EnviroGen.Coloring;
using EnviroGen.Continents;
using EnviroGen.Erosion;

namespace EnviroGenDisplay
{
    interface IEnvironment
    {
        void GenerateHeightMap(EnvironmentData data);

        void SetColorMapping(Colorizer colorizer);
        Colorizer GetColorizer();
        void AddColor(ColorRange c);
        void RemoveColor(ColorRange c);
        void ApplyColorizer();

        void ErodeHeightMap(IEroder eroder);
        void GenerateContinents(IContinentGenerator generator);
    }
}
