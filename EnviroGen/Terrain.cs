using System;
using SFML.Graphics;
using SFML.Window;

namespace EnviroGen
{
    /// <summary>
    /// Includes land masses and oceans
    /// </summary>
    class Terrain : Transformable, Drawable
    {
        private Sprite m_heightSprite { get; set; }
        private float[,] m_heightMap { get; set; }

        public static int SeaLevel = 100; //How high the sea goes up to
        public static int SandDistance = 10; //Distance from water where a height is considered sand
        public static int ForestDistance = 70; //Distance from water where a height is considered forest
        public static int MountainDistance = 135; //Distance from water where a height is considered mountain

        public Terrain(float[,] heightMap, Vector2i desiredSize)
        {
            m_heightMap = heightMap;
            m_heightSprite = GenerateHeightSprite();
            Scale = new Vector2f(desiredSize.X / (float)(m_heightSprite.Texture.Size.X), desiredSize.Y / (float)(m_heightSprite.Texture.Size.Y));
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform.Combine(Transform);
            target.Draw(m_heightSprite, states);
        }

        /// <summary>
        /// Generate the height sprite based on the height map
        /// </summary>
        /// <returns></returns>
        private Sprite GenerateHeightSprite()
        {
            var heightImage = new Image((uint)m_heightMap.GetLength(0), (uint)m_heightMap.GetLength(1));
            SetHeightPixels(heightImage);
            return new Sprite(new Texture(heightImage));
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
                    var height = (byte)(m_heightMap[i, j] * Byte.MaxValue);
                    if (IsWaterHeight(height))
                    {
                        water.A = height; //deep water is darker
                        img.SetPixel(i, j, water);
                    }
                    else if (IsSandHeight(height))
                    {
                        var fromWater = (byte)(height - SeaLevel);
                        fromWater *= 10; //scale the distance so that we see a greater range of colors
                        sand.A = (byte)(240 - fromWater);
                        img.SetPixel(i, j, sand);
                    }
                    else if (IsForestHeight(height))
                    {
                        var fromWater = (byte)(height - SeaLevel);
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
        private static bool IsWaterHeight(byte height)
        {
            return height < SeaLevel;
        }

        /// <summary>
        /// Checks if height is in sand zone
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        private static bool IsSandHeight(byte height)
        {
            return height >= SeaLevel && height < SeaLevel + SandDistance;
        }

        /// <summary>
        /// Checks if height is in forest zone
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        private static bool IsForestHeight(byte height)
        {
            return height >= SeaLevel + SandDistance && height < SeaLevel + ForestDistance;
        }

        /// <summary>
        /// Checks if height is in mountain zone
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        private static bool IsMountainHeight(byte height)
        {
            return height >= SeaLevel + ForestDistance /*&& height < SeaLevel + MountainDistance*/;
        }
    }
}
