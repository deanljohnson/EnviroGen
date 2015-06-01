using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using EnviroGen;
using EnviroGenDisplay.Annotations;

namespace EnviroGenDisplay
{
    public class EnvironmentData : INotifyPropertyChanged
    {
        private int m_sizeX;
        private int m_sizeY;
        private int m_heightMapOctaveCount;
        private int m_cloudMapOctaveCount;
        private int m_seaLevel;
        private int m_sandDistance;
        private int m_forestDistance;
        private int m_mountainDistance;
        private int m_numberOfContinents;
        private int m_minimumContinentSize;
        private int m_maximumContinentSize;
        private int m_heightMapSeed;
        private int m_cloudMapSeed;
        private float m_noiseRoughness;
        private float m_noiseScale;
        private float m_erosionAngle;
        private int m_erosionIterations;
        private Color m_seaColor;
        private Color m_sandColor;
        private Color m_forestColor;
        private Color m_mountainColor;

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
            get { return m_seaLevel.ToString(); }
            set
            {
                if (m_seaLevel.ToString() != value)
                {
                    m_seaLevel = Byte.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string SandDistance
        {
            get { return m_sandDistance.ToString(); }
            set
            {
                if (m_sandDistance.ToString() != value)
                {
                    m_sandDistance = Byte.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string ForestDistance
        {
            get { return m_forestDistance.ToString(); }
            set
            {
                if (m_forestDistance.ToString() != value)
                {
                    m_forestDistance = Byte.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string MountainDistance
        {
            get { return m_mountainDistance.ToString(); }
            set
            {
                if (m_mountainDistance.ToString() != value)
                {
                    m_mountainDistance = Byte.Parse(value);
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

        public Color SeaColor
        {
            get { return m_seaColor; }
            set
            {
                if (m_seaColor != value)
                {
                    m_seaColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color SandColor
        {
            get { return m_sandColor; }
            set
            {
                if (m_sandColor != value)
                {
                    m_sandColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color ForestColor
        {
            get { return m_forestColor; }
            set
            {
                if (m_forestColor != value)
                {
                    m_forestColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color MountainColor
        {
            get { return m_mountainColor; }
            set
            {
                if (m_mountainColor != value)
                {
                    m_mountainColor = value;
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
            m_seaLevel = 120;
            m_sandDistance = 10;
            m_forestDistance = 70;
            m_mountainDistance = 135;
            m_numberOfContinents = 1;
            m_minimumContinentSize = 400;
            m_maximumContinentSize = 450;
            m_heightMapSeed = -1;
            m_cloudMapSeed = -1;
            m_noiseRoughness = .55f;
            m_noiseScale = .005f;
            m_erosionAngle = .022f;
            m_erosionIterations = 40;
        }

        public EnvironmentGenerator BuildEnvironmentGenerator()
        {
            var seaColor = new SFML.Graphics.Color(m_seaColor.R, m_seaColor.G, m_seaColor.B, m_seaColor.A);
            var sandColor = new SFML.Graphics.Color(m_sandColor.R, m_sandColor.G, m_sandColor.B, m_sandColor.A);
            var forestColor = new SFML.Graphics.Color(m_forestColor.R, m_forestColor.G, m_forestColor.B, m_forestColor.A);
            var mountainColor = new SFML.Graphics.Color(m_mountainColor.R, m_mountainColor.G, m_mountainColor.B, m_mountainColor.A);

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
                SeaColor = seaColor,
                SandColor = sandColor,
                ForestColor = forestColor,
                MountainColor = mountainColor
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
