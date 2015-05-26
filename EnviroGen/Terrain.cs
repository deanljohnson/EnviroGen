using System;
using SFML.Graphics;

namespace EnviroGen
{
    class Terrain : Transformable, Drawable
    {
        private readonly Sprite m_heightSprite;
        private readonly float[,] m_heightMap;

        public static int SeaLevel = 120;
        public static int SandDistance = 10; //Distance from water where a height is considered sand
        public static int ForestDistance = 70; //Distance from water where a height is considered forest
        public static int MountainDistance = 135; //Distance from water where a height is considered mountain

        public Terrain(float[,] heightMap)
        {
            m_heightMap = heightMap;
            m_heightSprite = GenerateHeightSprite();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform.Combine(Transform);
            target.Draw(m_heightSprite, states);
        }

        private Sprite GenerateHeightSprite()
        {
            var heightImage = new Image((uint)m_heightMap.GetLength(0), (uint)m_heightMap.GetLength(1));
            SetHeightPixels(heightImage);
            return new Sprite(new Texture(heightImage));
        }

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

        private static bool IsWaterHeight(byte height)
        {
            return height < SeaLevel;
        }

        private static bool IsSandHeight(byte height)
        {
            return height >= SeaLevel && height < SeaLevel + SandDistance;
        }

        private static bool IsForestHeight(byte height)
        {
            return height >= SeaLevel + SandDistance && height < SeaLevel + ForestDistance;
        }

        private static bool IsMountainHeight(byte height)
        {
            return height >= SeaLevel + ForestDistance && height < SeaLevel + MountainDistance;
        }
    }
}
