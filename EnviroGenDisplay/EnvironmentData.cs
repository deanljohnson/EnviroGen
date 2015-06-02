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
        private int m_sizeX;
        private int m_sizeY;
        private int m_heightMapOctaveCount;
        private int m_cloudMapOctaveCount;
        private float m_seaLevel;
        private float m_sandDistance;
        private float m_forestDistance;
        private float m_mountainDistance;
        private int m_numberOfContinents;
        private int m_minimumContinentSize;
        private int m_maximumContinentSize;
        private int m_heightMapSeed;
        private int m_cloudMapSeed;
        private float m_noiseRoughness;
        private float m_noiseScale;
        private float m_erosionAngle;
        private int m_erosionIterations;
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
            get { return m_sizeX.ToString(); }
            set
            {
                if (m_sizeX.ToString() != value)
                {
                    m_sizeX = Int32.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string SizeY
        {
            get { return m_sizeY.ToString(); }
            set
            {
                if (m_sizeY.ToString() != value)
                {
                    m_sizeY = Int32.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string HeightMapOctaveCount
        {
            get { return m_heightMapOctaveCount.ToString(); }
            set
            {
                if (m_heightMapOctaveCount.ToString() != value)
                {
                    m_heightMapOctaveCount = Int32.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string CloudMapOctaveCount
        {
            get { return m_cloudMapOctaveCount.ToString(); }
            set
            {
                if (m_cloudMapOctaveCount.ToString() != value)
                {
                    m_cloudMapOctaveCount = Int32.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string SeaLevel
        {
            get { return m_seaLevel.ToString(CultureInfo.CurrentCulture); }
            set
            {
                if (m_seaLevel.ToString(CultureInfo.InvariantCulture) != value)
                {
                    m_seaLevel = float.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string SandDistance
        {
            get { return m_sandDistance.ToString(CultureInfo.CurrentCulture); }
            set
            {
                if (m_sandDistance.ToString(CultureInfo.CurrentCulture) != value)
                {
                    m_sandDistance = float.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string ForestDistance
        {
            get { return m_forestDistance.ToString(CultureInfo.CurrentCulture); }
            set
            {
                if (m_forestDistance.ToString(CultureInfo.CurrentCulture) != value)
                {
                    m_forestDistance = float.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string MountainDistance
        {
            get { return m_mountainDistance.ToString(CultureInfo.CurrentCulture); }
            set
            {
                if (m_mountainDistance.ToString(CultureInfo.CurrentCulture) != value)
                {
                    m_mountainDistance = float.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string NumberOfContinents
        {
            get { return m_numberOfContinents.ToString(); }
            set
            {
                if (m_numberOfContinents.ToString() != value)
                {
                    m_numberOfContinents = Int32.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string MinimumContinentSize
        {
            get { return m_minimumContinentSize.ToString(); }
            set
            {
                if (m_minimumContinentSize.ToString() != value)
                {
                    m_minimumContinentSize = Int32.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string MaximumContinentSize
        {
            get { return m_maximumContinentSize.ToString(); }
            set
            {
                if (m_maximumContinentSize.ToString() != value)
                {
                    m_maximumContinentSize = Int32.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string HeightMapSeed
        {
            get { return m_heightMapSeed.ToString(); }
            set
            {
                if (m_heightMapSeed.ToString() != value)
                {
                    m_heightMapSeed = Int32.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string CloudMapSeed
        {
            get { return m_cloudMapSeed.ToString(); }
            set
            {
                if (m_cloudMapSeed.ToString() != value)
                {
                    m_cloudMapSeed = Int32.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string NoiseRoughness
        {
            get { return m_noiseRoughness.ToString(CultureInfo.CurrentCulture); }
            set
            {
                if (m_noiseRoughness.ToString(CultureInfo.CurrentCulture) != value)
                {
                    m_noiseRoughness = float.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string NoiseScale
        {
            get { return m_noiseScale.ToString(CultureInfo.CurrentCulture); }
            set
            {
                if (m_noiseScale.ToString(CultureInfo.CurrentCulture) != value)
                {
                    m_noiseScale = float.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string ErosionAngle
        {
            get { return m_erosionAngle.ToString(CultureInfo.CurrentCulture); }
            set
            {
                if (m_erosionAngle.ToString(CultureInfo.CurrentCulture) != value)
                {
                    m_erosionAngle = float.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string ErosionIterations
        {
            get { return m_erosionIterations.ToString(); }
            set
            {
                if (m_erosionIterations.ToString() != value)
                {
                    m_erosionIterations = Int32.Parse(value);
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
            m_sizeX = 1400;
            m_sizeY = 800;
            m_heightMapOctaveCount = 8;
            m_cloudMapOctaveCount = 8;
            m_seaLevel = .4f;
            m_sandDistance = .45f;
            m_forestDistance = .75f;
            m_mountainDistance = 1f;
            m_numberOfContinents = 1;
            m_minimumContinentSize = 400;
            m_maximumContinentSize = 450;
            m_heightMapSeed = -1;
            m_cloudMapSeed = -1;
            m_noiseRoughness = .55f;
            m_noiseScale = .005f;
            m_erosionAngle = .022f;
            m_erosionIterations = 40;

            m_seaColorLow = Color.FromArgb(255, 0, 0, 116);
            m_sandColorLow = Color.FromArgb(255, 170, 166, 27);
            m_forestColorLow = Color.FromArgb(255, 0, 159, 21);
            m_mountainColorLow = Color.FromArgb(255, 197, 197, 202);

            m_seaColorHigh = Color.FromArgb(255, 0, 0, 255);
            m_sandColorHigh = Color.FromArgb(255, 206, 202, 49);
            m_forestColorHigh = Color.FromArgb(255, 22, 88, 31);
            m_mountainColorHigh = Color.FromArgb(255, 248, 248, 248);
        }

        public EnvironmentGenerator BuildEnvironmentGenerator()
        {
            var terrainColorizer = BuildTerrainColorizer();

            return new EnvironmentGenerator
            {
                SizeX = m_sizeX,
                SizeY = m_sizeY,
                HeightMapOctaveCount = m_heightMapOctaveCount,
                CloudMapOctaveCount = m_cloudMapOctaveCount,
                NumContinents = m_numberOfContinents,
                MinimumContinentSize = m_minimumContinentSize,
                MaximumContinentSize = m_maximumContinentSize,
                SeaLevel = m_seaLevel,
                SandDistance = m_sandDistance,
                ForestDistance = m_forestDistance,
                HeightMapSeed = m_heightMapSeed,
                CloudMapSeed = m_cloudMapSeed,
                NoiseRoughness = m_noiseRoughness,
                NoiseScale = m_noiseScale,
                ErosionAngle = m_erosionAngle,
                ErosionIterations = m_erosionIterations,
                TerrainColorizer = terrainColorizer
            };
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
            terrainColorizer.AddColorRange(seaColorLow, seaColorHigh, 0f, m_seaLevel);
            terrainColorizer.AddColorRange(sandColorLow, sandColorHigh, m_seaLevel, m_sandDistance);
            terrainColorizer.AddColorRange(forestColorLow, forestColorHigh, m_sandDistance, m_forestDistance);
            terrainColorizer.AddColorRange(mountainColorLow, mountainColorHigh, m_forestDistance, m_mountainDistance);

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
    }
}
