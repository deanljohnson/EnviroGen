namespace EnviroGen
{
    public class Environment
    {
        public Terrain Terrain { get; set; }

        public Environment(Terrain terrain)
        {
            Terrain = terrain;
        }
    }
}
