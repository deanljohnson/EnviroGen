using System;
using SFML.Graphics;
using SFML.Window;

namespace EnviroGen
{
    class Clouds : Transformable, Drawable
    {
        private readonly Sprite m_cloudSprite;
        private readonly float[,] m_cloudMap;
        private Vector2f m_size;

        public Clouds(float[,] cloudMap)
        {
            m_cloudMap = cloudMap;
            m_cloudSprite = GenerateCloudSprite();
            m_size = new Vector2f(m_cloudSprite.Texture.Size.X, m_cloudSprite.Texture.Size.Y);
        }

        /// <summary>
        /// Scrolls the clouds
        /// </summary>
        public void Update()
        {
            m_cloudSprite.Position += new Vector2f(.5f, 0);
            if (m_cloudSprite.Position.X >= m_size.X)
            {
                m_cloudSprite.Position = new Vector2f(0, 0);
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform.Combine(Transform);

            target.Draw(m_cloudSprite, states);

            //Flip texture rect and set position for scrolling
            //Allows never-ending scrolling, but not a very interesting cloud sequence
            m_cloudSprite.Position -= new Vector2f(m_cloudSprite.Texture.Size.X, 0);
            m_cloudSprite.TextureRect = new IntRect((int)m_cloudSprite.Texture.Size.X, 0, ((int)m_cloudSprite.Texture.Size.X) * -1, (int)m_cloudSprite.Texture.Size.Y);
            target.Draw(m_cloudSprite);

            //reset textureRect and position
            m_cloudSprite.Position += new Vector2f(m_cloudSprite.Texture.Size.X, 0);
            m_cloudSprite.TextureRect = new IntRect(0, 0, (int)m_cloudSprite.Texture.Size.X, (int)m_cloudSprite.Texture.Size.Y);
        }

        /// <summary>
        /// Generates a cloud sprite basd on the values from the cloud map
        /// </summary>
        /// <returns></returns>
        private Sprite GenerateCloudSprite()
        {
            var cloudImage = new Image((uint)m_cloudMap.GetLength(0), (uint)m_cloudMap.GetLength(1));
            SetCloudPixels(cloudImage);
            return new Sprite(new Texture(cloudImage));
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
