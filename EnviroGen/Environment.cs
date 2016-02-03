using EnviroGen.HeightMaps;

namespace EnviroGen
{
    public class Environment
    {
        public HeightMap Terrain { get; set; }

        public Environment(HeightMap terrain = null)
        {
            Terrain = terrain;
        }

        public virtual void Update()
        {
        }
    }
}
