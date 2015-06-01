using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
