namespace EnviroGen
{
    public class Environment
    {
        public Terrain Terrain { get; set; }

        public Environment(Terrain terrain = null)
        {
            Terrain = terrain;
        }

        public virtual void Update()
        {
        }
    }
}
