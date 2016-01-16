using EnviroGen.Coloring;
using EnviroGen.Continents;
using EnviroGen.Erosion;

namespace EnviroGenDisplay
{
    interface IEnvironment
    {
        void GenerateHeightMap(EnvironmentData data);
        void SetColorMapping(Colorizer colorizer);
        void ErodeHeightMap(IEroder eroder);
        void GenerateContinents(IContinentGenerator generator);
    }
}
