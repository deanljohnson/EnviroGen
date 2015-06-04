using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using EnviroGen;
using EnviroGen.Coloring;
using EnviroGenDisplay.Annotations;

namespace EnviroGenDisplay
{
    public sealed class EnvironmentData : INotifyPropertyChanged
    {
        public GenerationOptions GenOptions { get; private set; }
        private Color m_seaColorLow;
        private Color m_sandColorLow;
        private Color m_forestColorLow;
        private Color m_mountainColorLow;
        private Color m_seaColorHigh;
        private Color m_sandColorHigh;
        private Color m_forestColorHigh;
        private Color m_mountainColorHigh;

        public string SizeX
        {
            get { return GenOptions.SizeX.ToString(); }
            set
            {
                if (GenOptions.SizeX.ToString() != value)
                {
                    GenOptions.SizeX = Int32.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string SizeY
        {
            get { return GenOptions.SizeY.ToString(); }
            set
            {
                if (GenOptions.SizeY.ToString() != value)
                {
                    GenOptions.SizeY = Int32.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string HeightMapOctaveCount
        {
            get { return GenOptions.HeightMapOctaveCount.ToString(); }
            set
            {
                if (GenOptions.HeightMapOctaveCount.ToString() != value)
                {
                    GenOptions.HeightMapOctaveCount = Int32.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string CloudMapOctaveCount
        {
            get { return GenOptions.CloudMapOctaveCount.ToString(); }
            set
            {
                if (GenOptions.CloudMapOctaveCount.ToString() != value)
                {
                    GenOptions.CloudMapOctaveCount = Int32.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string SeaLevel
        {
            get { return GenOptions.SeaLevel.ToString(CultureInfo.CurrentCulture); }
            set
            {
                if (GenOptions.SeaLevel.ToString(CultureInfo.InvariantCulture) != value)
                {
                    GenOptions.SeaLevel = float.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string SandDistance
        {
            get { return GenOptions.SandDistance.ToString(CultureInfo.CurrentCulture); }
            set
            {
                if (GenOptions.SandDistance.ToString(CultureInfo.CurrentCulture) != value)
                {
                    GenOptions.SandDistance = float.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string ForestDistance
        {
            get { return GenOptions.ForestDistance.ToString(CultureInfo.CurrentCulture); }
            set
            {
                if (GenOptions.ForestDistance.ToString(CultureInfo.CurrentCulture) != value)
                {
                    GenOptions.ForestDistance = float.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string MountainDistance
        {
            get { return GenOptions.MountainDistance.ToString(CultureInfo.CurrentCulture); }
            set
            {
                if (GenOptions.MountainDistance.ToString(CultureInfo.CurrentCulture) != value)
                {
                    GenOptions.MountainDistance = float.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string HeightMapSeed
        {
            get { return GenOptions.HeightMapSeed.ToString(); }
            set
            {
                if (GenOptions.HeightMapSeed.ToString() != value)
                {
                    GenOptions.HeightMapSeed = Int32.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string CloudMapSeed
        {
            get { return GenOptions.CloudMapSeed.ToString(); }
            set
            {
                if (GenOptions.CloudMapSeed.ToString() != value)
                {
                    GenOptions.CloudMapSeed = Int32.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string NoiseRoughness
        {
            get { return GenOptions.NoiseRoughness.ToString(CultureInfo.CurrentCulture); }
            set
            {
                if (GenOptions.NoiseRoughness.ToString(CultureInfo.CurrentCulture) != value)
                {
                    GenOptions.NoiseRoughness = float.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string NoiseFrequency
        {
            get { return GenOptions.NoiseFrequency.ToString(CultureInfo.CurrentCulture); }
            set
            {
                if (GenOptions.NoiseFrequency.ToString(CultureInfo.CurrentCulture) != value)
                {
                    GenOptions.NoiseFrequency = float.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public Color SeaColorLow
        {
            get { return m_seaColorLow; }
            set
            {
                if (m_seaColorLow != value)
                {
                    m_seaColorLow = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color SandColorLow
        {
            get { return m_sandColorLow; }
            set
            {
                if (m_sandColorLow != value)
                {
                    m_sandColorLow = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color ForestColorLow
        {
            get { return m_forestColorLow; }
            set
            {
                if (m_forestColorLow != value)
                {
                    m_forestColorLow = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color MountainColorLow
        {
            get { return m_mountainColorLow; }
            set
            {
                if (m_mountainColorLow != value)
                {
                    m_mountainColorLow = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color SeaColorHigh
        {
            get { return m_seaColorHigh; }
            set
            {
                if (m_seaColorHigh != value)
                {
                    m_seaColorHigh = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color SandColorHigh
        {
            get { return m_sandColorHigh; }
            set
            {
                if (m_sandColorHigh != value)
                {
                    m_sandColorHigh = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color ForestColorHigh
        {
            get { return m_forestColorHigh; }
            set
            {
                if (m_forestColorHigh != value)
                {
                    m_forestColorHigh = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color MountainColorHigh
        {
            get { return m_mountainColorHigh; }
            set
            {
                if (m_mountainColorHigh != value)
                {
                    m_mountainColorHigh = value;
                    OnPropertyChanged();
                }
            }
        }

        public EnvironmentData()
        {
            GenOptions = new GenerationOptions();

            m_seaColorLow = Color.FromArgb(255, 0, 0, 116);
            m_sandColorLow = Color.FromArgb(255, 170, 166, 27);
            m_forestColorLow = Color.FromArgb(255, 0, 159, 21);
            m_mountainColorLow = Color.FromArgb(255, 197, 197, 202);

            m_seaColorHigh = Color.FromArgb(255, 0, 0, 255);
            m_sandColorHigh = Color.FromArgb(255, 206, 202, 49);
            m_forestColorHigh = Color.FromArgb(255, 22, 88, 31);
            m_mountainColorHigh = Color.FromArgb(255, 248, 248, 248);
        }

        public Colorizer BuildTerrainColorizer()
        {
            var seaColorLow = FromSystemColor(m_seaColorLow);
            var sandColorLow = FromSystemColor(m_sandColorLow);
            var forestColorLow = FromSystemColor(m_forestColorLow);
            var mountainColorLow = FromSystemColor(m_mountainColorLow);

            var seaColorHigh = FromSystemColor(m_seaColorHigh);
            var sandColorHigh = FromSystemColor(m_sandColorHigh);
            var forestColorHigh = FromSystemColor(m_forestColorHigh);
            var mountainColorHigh = FromSystemColor(m_mountainColorHigh);

            var terrainColorizer = new Colorizer();
            terrainColorizer.AddColorRange(seaColorLow, seaColorHigh, 0f, GenOptions.SeaLevel);
            terrainColorizer.AddColorRange(sandColorLow, sandColorHigh, GenOptions.SeaLevel, GenOptions.SandDistance);
            terrainColorizer.AddColorRange(forestColorLow, forestColorHigh, GenOptions.SandDistance, GenOptions.ForestDistance);
            terrainColorizer.AddColorRange(mountainColorLow, mountainColorHigh, GenOptions.ForestDistance, GenOptions.MountainDistance);

            return terrainColorizer;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private static SFML.Graphics.Color FromSystemColor(Color sysColor)
        {
            return new SFML.Graphics.Color(sysColor.R, sysColor.G, sysColor.B, sysColor.A);
        }

        private static Color FromSFMLColor(SFML.Graphics.Color sfmlColor)
        {
            return Color.FromArgb(sfmlColor.A, sfmlColor.R, sfmlColor.G, sfmlColor.B);
        }
    }
}
