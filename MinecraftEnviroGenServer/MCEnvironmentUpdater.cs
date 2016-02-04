using EnviroGen;
using EnviroGen.Erosion;

namespace MinecraftEnviroGenServer
{
    class MCEnvironmentUpdater : IEnvironmentUpdater
    {
        public IEroder Eroder { get; set; }

        public MCEnvironmentUpdater()
        {
            Eroder = new HydraulicEroder
            {
                RainAmount = .2f,
                Solubility = .1f,
                Evaporation = .1f,
                Capacity = .5f,
                Iterations = 1
            };
        }

        public void DoUpdate(Environment environment)
        {
            Eroder.Erode(environment.Terrain);
        }
    }
}
