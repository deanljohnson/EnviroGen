using EnviroGen;
using SFML.Graphics;
using Color = System.Windows.Media.Color;
using Environment = EnviroGen.Environment;
using Image = EnviroGen.Image;

namespace EnviroGenDisplay
{
    public class EnvironmentDrawable : Environment, Drawable
    {
        /// <summary>
        /// Gets or sets a value indicating whether or not the EnvironmentDrawable has been changed.
        /// </summary>
        public bool Dirty { get; set; }

        public new Terrain Terrain {
            get { return base.Terrain; }
            set
            {
                base.Terrain = value;
                Dirty = true;
            }
        }

        public new Clouds Clouds
        {
            get { return base.Clouds; }
            set
            {
                base.Clouds = value;
                Dirty = true;
            }
        }

        public EnvironmentDrawable(Terrain terrain, Clouds clouds) 
            : base(terrain, clouds)
        {
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            var terrainImage = ToSFMLImage(Terrain?.Image);
            var cloudsImage = ToSFMLImage(Clouds?.Image);

            if (terrainImage != null)
            {
                var terrainSprite = new Sprite(new Texture(terrainImage));
                target.Draw(terrainSprite, states);
            }
            if (cloudsImage != null)
            {
                var cloudsSprite = new Sprite(new Texture(cloudsImage));
                target.Draw(cloudsSprite, states);
            }
        }

        private static SFML.Graphics.Image ToSFMLImage(Image image)
        {
            if (image == null) return null;

            var sfmlimage = new SFML.Graphics.Image(image.Width, image.Height);

            for (uint j = 0; j < image.Height; j++)
            {
                for (uint i = 0; i < image.Width; i++)
                {
                    sfmlimage.SetPixel(i, j, ToSFMLColor(image[i, j]));
                }
            }

            return sfmlimage;
        }

        private static SFML.Graphics.Color ToSFMLColor(Color color)
        {
            return new SFML.Graphics.Color(color.R, color.G, color.B);
        }
    }
}
