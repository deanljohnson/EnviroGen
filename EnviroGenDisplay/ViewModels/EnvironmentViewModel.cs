using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EnviroGen;
using EnviroGenDisplay.Properties;
using Environment = EnviroGen.Environment;

namespace EnviroGenDisplay.ViewModels
{
    internal class EnvironmentViewModel : Environment
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SaveCommand { get; set; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private WriteableBitmap m_HeightBitmap;

        public WriteableBitmap HeightMapBitmap {
            get { return m_HeightBitmap; }
            private set
            {
                if (!ReferenceEquals(m_HeightBitmap, value))
                {
                    m_HeightBitmap = value;
                    OnPropertyChanged();
                }
            }
        }

        public EnvironmentViewModel(int w = 1000, int h = 780)
        {
            //why 96? idk, it works
            HeightMapBitmap = new WriteableBitmap(w, h, 96, 96, PixelFormats.Bgra32, null);

            SaveCommand = new RelayCommand(SaveToImage);
        }

        private void SaveToImage(object obj = null)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "PNF file (*png)|*.png"
            };

            dialog.FileOk += OnSaveImage;

            dialog.ShowDialog();
        }

        private void OnSaveImage(object sender, CancelEventArgs e)
        {
            var dialog = sender as SaveFileDialog;
            if (dialog == null)
                return;

            string fileName = dialog.FileName;

            if (fileName != string.Empty)
            {
                using (FileStream stream5 = new FileStream(fileName, FileMode.Create))
                {
                    PngBitmapEncoder encoder5 = new PngBitmapEncoder();
                    encoder5.Frames.Add(BitmapFrame.Create(m_HeightBitmap.Clone()));
                    encoder5.Save(stream5);
                }
            }
        }

        public override void Update()
        {
            if (Terrain == null) return;

            lock (this)
            {
                //We have to execute this after any background workers are finished
                //because writeablebitmap's can only lock on the owning thread
                UpdateWholeBitmap();
            }
        }

        private void UpdateWholeBitmap()
        {
            Debug.Assert(Terrain is Terrain);
            var terrain = (Terrain) Terrain;

            //Get pixels to place in the Bitmap
            var pixels = new Color[terrain.Image.Width * terrain.Image.Height];
            for (uint j = 0; j < terrain.Image.Height; j++)
            {
                //The number of pixels up to the j'th row
                var countSoFar = (j * terrain.Image.Width);

                for (uint i = 0; i < terrain.Image.Width; i++)
                {
                    pixels[i + countSoFar] = terrain.Image[i, j];
                }
            }

            //Resize the bitmap if needed
            if (terrain.Image.Width != HeightMapBitmap.PixelWidth ||
                terrain.Image.Height != HeightMapBitmap.PixelHeight)
            {
                HeightMapBitmap = new WriteableBitmap((int)terrain.Image.Width, 
                                                        (int)terrain.Image.Height, 
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
