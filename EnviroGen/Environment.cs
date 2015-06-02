using SFML.Graphics;

namespace EnviroGen
{
    public class Environment : Transformable, Drawable
    {
        public Terrain Terrain { get; set; }
        public Clouds Clouds { get; set; }

        public Environment(Terrain terrain, Clouds clouds)
        {
            Terrain = terrain;
            Clouds = clouds;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform.Combine(Transform);
            if (Terrain != null) target.Draw(Terrain, states);
            if (Clouds != null) target.Draw(Clouds, states);
        }
    }
}
