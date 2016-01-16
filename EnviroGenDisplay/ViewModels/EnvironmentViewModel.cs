using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EnviroGen;
using EnviroGen.Coloring;
using EnviroGen.Continents;
using EnviroGen.Erosion;
using EnviroGen.HeightMaps;
using Environment = EnviroGen.Environment;

namespace EnviroGenDisplay.ViewModels
{
    internal class EnvironmentViewModel : ViewModelBase, IEnvironment
    {
        private static readonly Random Random = new Random();
        private Environment m_Environment { get; }

        public WriteableBitmap HeightMapBitmap { get; set; }

        public EnvironmentViewModel(int w = 1000, int h = 780)
        {
            m_Environment = new Environment(null, null);
            //why 96? idk, it works
            HeightMapBitmap = new WriteableBitmap(w, h, 96, 96, PixelFormats.Bgra32, null);
        }

        public void GenerateHeightMap(EnvironmentData data)
        {
            var options = data.ToGenerationOptions();
            //pick a random seed if the seed is -1
            options.Seed = (options.Seed == -1) ? Random.Next(10000) : options.Seed;

            var terrainHeightMap = HeightMapGenerator.GenerateHeightMap(options);

            if (terrainHeightMap == null)
            {
                throw new NullReferenceException("Error in terrain height map generation, EnvironmentDisplay.GenerateHeightMap");
            }

            lock (m_Environment)
            {
                if (data.Combining && m_Environment.Terrain?.HeightMap != null)
                {
                    m_Environment.Terrain.HeightMap.CombineWith(terrainHeightMap);
                    m_Environment.Terrain.UpdateImage();
                }
                else
                {
                    m_Environment.Terrain = new Terrain(terrainHeightMap);
                }

                UpdateBitmap();
            }
        }

        public void SetColorMapping(Colorizer colorizer)
        {
            lock (m_Environment)
            {
                if (m_Environment.Terrain == null) return;

                m_Environment.Terrain.Colorizer = colorizer;
                m_Environment.Terrain.Colorize();

                UpdateBitmap();
            }
        }

        public void ErodeHeightMap(IEroder eroder)
        {
            eroder.Erode(m_Environment.Terrain.HeightMap);
            m_Environment.Terrain.UpdateImage();
            UpdateBitmap();
        }

        private void UpdateBitmap()
        {
            //Get pixels to place in the Bitmap
            var pixels = new Color[m_Environment.Terrain.Image.Width * m_Environment.Terrain.Image.Height];
            for (uint j = 0; j < m_Environment.Terrain.Image.Height; j++)
            {
                //The number of pixels up to the j'th row
                var countSoFar = (j * m_Environment.Terrain.Image.Width);

                for (uint i = 0; i < m_Environment.Terrain.Image.Width; i++)
                {
                    pixels[i + countSoFar] = m_Environment.Terrain.Image[i, j];
                }
            }

            HeightMapBitmap.Lock();
            IntPtr buffer = HeightMapBitmap.BackBuffer;

            unsafe
            {
                byte* bytes = (byte*) buffer.ToPointer();

                for (var i = 0; i < HeightMapBitmap.PixelWidth * HeightMapBitmap.PixelHeight * 4; i += 4)
                {
                    //Indexing auto-dereferences pointers
                    //The order of color channels is dependent on HeightBitMap's construction,
                    //be careful if editing
                    bytes[i] = pixels[i / 4].B;
                    bytes[i + 1] = pixels[i / 4].G;
                    bytes[i + 2] = pixels[i / 4].R;
                    bytes[i + 3] = pixels[i / 4].A;
                }
            }

            HeightMapBitmap.AddDirtyRect(new Int32Rect(0, 0, HeightMapBitmap.PixelWidth, HeightMapBitmap.PixelHeight));
            HeightMapBitmap.Unlock();
        }

        public void GenerateContinents(IContinentGenerator generator)
        {
            generator.GenerateContinents(m_Environment.Terrain.HeightMap);
            m_Environment.Terrain.UpdateImage();
            UpdateBitmap();

        }
    }
}
