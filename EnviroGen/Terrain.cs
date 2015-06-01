using System;
using SFML.Graphics;

namespace EnviroGen
{
    /// <summary>
    /// Includes land masses and oceans
    /// </summary>
    public class Terrain : Transformable, Drawable
    {
        private int m_seaLevel = 100; //How high the sea goes up to
        private int m_sandHeight = 10; //Distance from water where a height is considered sand
        private int m_forestHeight = 70; //Distance from water where a height is considered forest
        private int m_mountainHeight = 135; //Distance from water where a height is considered mountain
        private HeightMap m_heightMap;
        private Sprite m_heightSprite { get; set; }

        /// <summary>
        /// Sets the Terrains height map. Setting this will cause a regeneration of the sprite.
        /// </summary>
        public HeightMap HeightMap
        {
            private get { return m_heightMap; }
            set
            {
                m_heightMap = value;
                GenerateSprite();
            }
        }

        public Terrain(HeightMap heightMap)
        {
            HeightMap = heightMap;
            GenerateSprite();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform.Combine(Transform);
            target.Draw(m_heightSprite, states);
        }

        /// <summary>
        /// Sets the heights for color mapping. Call ColorMap to actually update the sprite.
        /// </summary>
        /// <param name="seaLevel"></param>
        /// <param name="sandHeight"></param>
        /// <param name="forestHeight"></param>
        /// <param name="mountainHeight"></param>
        public void SetColorMapping(int seaLevel, int sandHeight, int forestHeight, int mountainHeight)
        {
            m_seaLevel = seaLevel;
            m_sandHeight = sandHeight;
            m_forestHeight = forestHeight;
            m_mountainHeight = mountainHeight;
        }

        /// <summary>
        /// Color maps the terrain according to its height map.
        /// </summary>
        public void ColorMap()
        {
            GenerateSprite();
        }

        /// <summary>
        /// Generate the height sprite based on the height map
        /// </summary>
        /// <returns></returns>
        private void GenerateSprite()
        {
            var heightImage = new Image(HeightMap.Size.X, HeightMap.Size.Y);
            SetHeightPixels(heightImage);
            m_heightSprite = new Sprite(new Texture(heightImage));
        }

        /// <summary>
        /// Set img pixel colors according to height properties
        /// </summary>
        /// <param name="img"></param>
        private void SetHeightPixels(Image img)
        {
            var water = new Color(0, 0, 255);
            var sand = new Color(255, 255, 204);
            var forest = new Color(0, 255, 0);
            var mountains = new Color(102, 102, 102, 120);

            for (uint j = 0; j < img.Size.Y; j++)
            {
                for (uint i = 0; i < img.Size.X; i++)
                {
                    //Scale the float value of the height map into a byte value
                    var height = (byte)(HeightMap[i, j] * Byte.MaxValue);
                    if (IsWaterHeight(height))
                    {
                        water.A = height; //deep water is darker
                        img.SetPixel(i, j, water);
                    }
                    else if (IsSandHeight(height))
                    {
                        var fromWater = (byte)(height - m_seaLevel);
                        fromWater *= 10; //scale the distance so that we see a greater range of colors
                        sand.A = (byte)(240 - fromWater);
                        img.SetPixel(i, j, sand);
                    }
                    else if (IsForestHeight(height))
                    {
                        var fromWater = (byte)(height - m_seaLevel);
                        forest.G = (byte)(height - (2 * fromWater)); //higher forest is darker colored

                        img.SetPixel(i, j, forest);
                    }
                    else if (IsMountainHeight(height))
                    {
                        mountains.A = height; //higher mountains are brighter
                        img.SetPixel(i, j, mountains);
                    }
                }
            }
        }

        /// <summary>
        /// Checks if height is in water zone
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        private bool IsWaterHeight(byte height)
        {
            return height < m_seaLevel;
        }

        /// <summary>
        /// Checks if height is in sand zone
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        private bool IsSandHeight(byte height)
        {
            return height >= m_seaLevel && height < m_seaLevel + m_sandHeight;
        }

        /// <summary>
        /// Checks if height is in forest zone
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        private bool IsForestHeight(byte height)
        {
            return height >= m_seaLevel + m_sandHeight && height < m_seaLevel + m_forestHeight;
        }

        /// <summary>
        /// Checks if height is in mountain zone
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        private bool IsMountainHeight(byte height)
        {
            return height >= m_seaLevel + m_forestHeight /*&& height < SeaLevel + MountainDistance*/;
        }
    }
}
