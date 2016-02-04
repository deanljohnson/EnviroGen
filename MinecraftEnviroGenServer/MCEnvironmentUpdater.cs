using EnviroGen;
using EnviroGen.Erosion;

namespace MinecraftEnviroGenServer
{
    class MCEnvironmentUpdater : IEnvironmentUpdater
    {
        public IEroder Eroder { get; set; }

        public MCEnvironmentUpdater()
        {
            Eroder = new ImprovedThermalEroder
            {
                TalusAngle = 3f,
                Iterations = 1
            };
        }

        public void DoUpdate(Environment environment)
        {
            Eroder.Erode(environment.Terrain);
        }
    }
}
