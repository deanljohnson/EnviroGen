using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;

namespace EnviroGen
{
    /// <summary>
    /// Includes land masses and oceans
    /// </summary>
    public class Terrain : Transformable, Drawable
    {
        public static Color SeaColor = new Color(0, 0, 255);
        public static Color SandColor = new Color(255, 255, 204);
        public static Color ForestColor = new Color(0, 255, 0);
        public static Color MountainColor = new Color(102, 102, 102, 120);

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

        public List<Vector2i> RiverTiles { get; set; }

        public Terrain(HeightMap heightMap)
            : this(heightMap, new List<Vector2i>())
        {
        }

        public Terrain(HeightMap heightMap, List<Vector2i> riverTiles)
        {
            m_heightMap = heightMap;
            RiverTiles = riverTiles;
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
        public void SetColorMappingHeights(int seaLevel, int sandHeight, int forestHeight, int mountainHeight)
        {
            m_seaLevel = seaLevel;
            m_sandHeight = sandHeight;
            m_forestHeight = forestHeight;
            m_mountainHeight = mountainHeight;
        }

        /// <summary>
        /// Sets the colors for color mapping. Call ColorMap to actually update the sprite.
        /// </summary>
        public void SetColorMappingColors(Color seaColor, Color sandColor, Color forestColor, Color mountainColor)
        {
            SeaColor = seaColor;
            SandColor = sandColor;
            ForestColor = forestColor;
            MountainColor = mountainColor;
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
            for (uint j = 0; j < img.Size.Y; j++)
            {
                for (uint i = 0; i < img.Size.X; i++)
                {
                    //Scale the float value of the height map into a byte value
                    var height = (byte)(HeightMap[i, j] * Byte.MaxValue);
                    if (IsWaterHeight(height))
                    {
                        SeaColor.A = height; //deep water is darker
                        img.SetPixel(i, j, SeaColor);
                    }
                    else if (IsSandHeight(height))
                    {
                        var fromWater = (byte)(height - m_seaLevel);
                        fromWater *= 10; //scale the distance so that we see a greater range of colors
                        SandColor.A = (byte)(240 - fromWater);
                        img.SetPixel(i, j, SandColor);
                    }
                    else if (IsForestHeight(height))
                    {
                        var fromWater = (byte)(height - m_seaLevel);
                        ForestColor.G = (byte)(height - (2 * fromWater)); //higher forest is darker colored

                        img.SetPixel(i, j, ForestColor);
                    }
                    else if (IsMountainHeight(height))
                    {
                        MountainColor.A = height; //higher mountains are brighter
                        img.SetPixel(i, j, MountainColor);
                    }
                }
            }

            foreach (var riverTile in RiverTiles)
            {
                img.SetPixel((uint)riverTile.X, (uint)riverTile.Y, SeaColor);
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
