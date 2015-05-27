using SFML.Graphics;

namespace EnviroGen
{
    class Environment : Transformable, Drawable
    {
        private readonly Terrain m_terrain;
        private readonly Clouds m_clouds;

        public Environment(Terrain terrain, Clouds clouds)
        {
            m_terrain = terrain;
            m_clouds = clouds;
        }

        public void Update()
        {
            m_clouds.Update();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform.Combine(Transform);
            target.Draw(m_terrain, states);
            target.Draw(m_clouds, states);
        }
    }
}
