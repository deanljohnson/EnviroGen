namespace EnviroGen
{
    public class Environment
    {
        public Terrain Terrain { get; set; }
        public Clouds Clouds { get; set; }

        public Environment(Terrain terrain, Clouds clouds)
        {
            Terrain = terrain;
            Clouds = clouds;
        }
    }
}
