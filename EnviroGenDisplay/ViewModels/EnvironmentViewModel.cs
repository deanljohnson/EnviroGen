using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EnviroGen;
using EnviroGen.Coloring;
using Environment = EnviroGen.Environment;

namespace EnviroGenDisplay.ViewModels
{
    internal class EnvironmentViewModel : ViewModelBase, IEnvironment
    {
        private WriteableBitmap m_HeightBitmap;

        public Environment Environment { get; }

        public WriteableBitmap HeightMapBitmap {
            get { return m_HeightBitmap; }
            private set
            {
                if (!ReferenceEquals(m_HeightBitmap, value))
                {
                    m_HeightBitmap = value;
                    OnPropertyChanged(nameof(HeightMapBitmap));
                }
            }
        }

        public IStatusTracker StatusTracker { get; set; }

        public EnvironmentViewModel(int w = 1000, int h = 780)
        {
            Environment = new Environment(null);
            //why 96? idk, it works
            HeightMapBitmap = new WriteableBitmap(w, h, 96, 96, PixelFormats.Bgra32, null);
        }

        public void Update()
        {
            if (Environment?.Terrain == null) return;

            lock (Environment)
            {
                //We have to execute this after any background workers are finished
                //because writeablebitmap's can only lock on the owning thread
                UpdateWholeBitmap();
            }
        }

        public void ApplyColorizer()
        {
            if (Environment?.Terrain == null) return;

            lock (Environment)
            {
                Environment.Terrain.Colorize();

                UpdateWholeBitmap();
            }
        }

        public Colorizer GetColorizer()
        {
            return Environment.Terrain?.Colorizer ?? Terrain.DefaultColorizer;
        }

        public void AddColor(ColorRange c)
        {
            Environment.Terrain?.Colorizer.AddColorRange(c);
        }

        public void RemoveColor(ColorRange c)
        {
            Environment.Terrain?.Colorizer.RemoveColorRange(c);
        }

        private void UpdateWholeBitmap()
        {
            //Get pixels to place in the Bitmap
            var pixels = new Color[Environment.Terrain.Image.Width * Environment.Terrain.Image.Height];
            for (uint j = 0; j < Environment.Terrain.Image.Height; j++)
            {
                //The number of pixels up to the j'th row
                var countSoFar = (j * Environment.Terrain.Image.Width);

                for (uint i = 0; i < Environment.Terrain.Image.Width; i++)
                {
                    pixels[i + countSoFar] = Environment.Terrain.Image[i, j];
                }
            }

            //Resize the bitmap if needed
            if (Environment.Terrain.Image.Width != HeightMapBitmap.PixelWidth ||
                Environment.Terrain.Image.Height != HeightMapBitmap.PixelHeight)
            {
                HeightMapBitmap = new WriteableBitmap((int) Environment.Terrain.Image.Width, 
                                                        (int) Environment.Terrain.Image.Height, 
                                                        96, 96, PixelFormats.Bgra32, null);
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
    }
}
