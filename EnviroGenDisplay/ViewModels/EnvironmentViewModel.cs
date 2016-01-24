using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EnviroGen;
using EnviroGen.Coloring;
using EnviroGen.Continents;
using EnviroGen.Erosion;
using EnviroGen.Noise.Modifiers;
using Environment = EnviroGen.Environment;

namespace EnviroGenDisplay.ViewModels
{
    internal class EnvironmentViewModel : ViewModelBase, IEnvironment
    {
        private static readonly Random Random = new Random();

        private WriteableBitmap m_HeightBitmap;
        private BackgroundWorker m_TerrainWorker { get; } = new BackgroundWorker();
        private BackgroundWorker m_ErosionWorker { get; } = new BackgroundWorker();

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

            m_TerrainWorker.DoWork += GenerateTerrain;
            m_TerrainWorker.RunWorkerCompleted += OnGenerationProcedureComplete;

            m_ErosionWorker.DoWork += ErodeTerrain;
            m_ErosionWorker.RunWorkerCompleted += OnGenerationProcedureComplete;
        }

        public void Update()
        {
            OnGenerationProcedureComplete(null, null);
        }

        public void GenerateTerrain(GenerationOptions data)
        {
            if (!m_TerrainWorker.IsBusy)
                m_TerrainWorker.RunWorkerAsync(data);
        }

        public void ErodeTerrain(IEroder eroder)
        {
            if (!m_ErosionWorker.IsBusy)
                m_ErosionWorker.RunWorkerAsync(eroder);
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

        public void GenerateContinents(IContinentGenerator generator)
        {
            if (Environment?.Terrain == null) return;

            lock (Environment)
            {
                Environment.GenerateContinents(generator);
            }

            UpdateWholeBitmap();
        }

        public void ApplyTerrainModifier(IModifier modifier)
        {
            if (Environment?.Terrain == null) return;

            lock (Environment)
            {
                Environment.ApplyTerrainModifier(modifier);
            }
            UpdateWholeBitmap();
        }

        public void ApplyTerrainModifierInverted(IInvertableModifier modifier)
        {
            if (Environment?.Terrain == null) return;

            lock (Environment)
            {
                Environment.ApplyTerrainModifierInverted(modifier);
            }
            UpdateWholeBitmap();
        }

        private void OnGenerationProcedureComplete(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            if (Environment?.Terrain == null) return;

            lock (Environment)
            {
                //We have to execute this after any background workers are finished
                //because writeablebitmap's can only lock on the owning thread
                UpdateWholeBitmap();
            }
        }

        private void GenerateTerrain(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            StatusTracker.PushMessage("Generating Terrain...");

            var options = (GenerationOptions)doWorkEventArgs.Argument;

            Debug.Assert(options != null, $"The passed argument was not of the expected type {typeof(GenerationOptions)}");

            //pick a random seed if the seed is -1
            options.Seed = (options.Seed == -1) ? Random.Next(10000) : options.Seed;

            lock (Environment)
            {
                Environment.GenerateTerrain(options);
            }

            StatusTracker.PopMessage();
        }

        private void ErodeTerrain(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            if (Environment?.Terrain == null) return;

            StatusTracker.PushMessage("Eroding Terrain...");

            var eroder = (IEroder)doWorkEventArgs.Argument;

            Debug.Assert(eroder != null, $"The passed argument was not of the expected type {typeof(IEroder)}");

            lock (Environment)
            {
                Environment.ErodeTerrain(eroder);
            }

            StatusTracker.PopMessage();
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
