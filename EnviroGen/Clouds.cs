using System;
using SFML.Graphics;

namespace EnviroGen
{
    public class Clouds : Transformable, Drawable
    {
        private Sprite m_cloudSprite { get; set; }
        private HeightMap m_cloudMap;

        /// <summary>
        /// Sets the Clouds height map. Setting this will cause a regeneration of the sprite.
        /// </summary>
        public HeightMap CloudMap
        {
            get { return m_cloudMap; }
            set
            {
                m_cloudMap = value;
                GenerateCloudSprite();
            }
        }

        public Clouds(HeightMap cloudMap)
        {
            m_cloudMap = cloudMap;
            GenerateCloudSprite();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform.Combine(Transform);
            target.Draw(m_cloudSprite, states);
        }

        /// <summary>
        /// Generates a cloud sprite basd on the values from the cloud map
        /// </summary>
        /// <returns></returns>
        private void GenerateCloudSprite()
        {
            var cloudImage = new Image(m_cloudMap.Size.X, m_cloudMap.Size.Y);
            SetCloudPixels(cloudImage);
            m_cloudSprite = new Sprite(new Texture(cloudImage));
        }

        /// <summary>
        /// Sets image pixels to varying levels of white
        /// </summary>
        /// <param name="img"></param>
        private void SetCloudPixels(Image img)
        {
            var cloudColor = new Color(255, 255, 255);

            for (uint j = 0; j < img.Size.Y; j++)
            {
                for (uint i = 0; i < img.Size.X; i++)
                {
                    var height = (byte)((m_cloudMap[i, j] * m_cloudMap[i, j]) * Byte.MaxValue);
                    cloudColor.A = (byte)(height / 3.5);
                    img.SetPixel(i, j, cloudColor);
                }
            }
        }
    }
}
