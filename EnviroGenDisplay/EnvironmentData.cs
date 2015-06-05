using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SFML.Graphics;
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

        public Color SeaColorLow
        {
            get { return m_seaColorLow; }
            set 
            {
                m_seaColorLow = value;
                OnPropertyChanged();
            }
        }

        public Color SandColorLow
        {
            get { return m_sandColorLow; }
            set
            {
                m_sandColorLow = value;
                OnPropertyChanged();
            }
        }

        public Color ForestColorLow
        {
            get { return m_forestColorLow; }
            set
            {

                m_forestColorLow = value;
                OnPropertyChanged();
            }
        }

        public Color MountainColorLow
        {
            get { return m_mountainColorLow; }
            set
            {
                m_mountainColorLow = value;
                OnPropertyChanged();
            }
        }

        public Color SeaColorHigh
        {
            get { return m_seaColorHigh; }
            set
            {
                m_seaColorHigh = value;
                OnPropertyChanged();
            }
        }

        public Color SandColorHigh
        {
            get { return m_sandColorHigh; }
            set
            {
                m_sandColorHigh = value;
                OnPropertyChanged();
            }
        }

        public Color ForestColorHigh
        {
            get { return m_forestColorHigh; }
            set
            {
                m_forestColorHigh = value;
                OnPropertyChanged();
            }
        }

        public Color MountainColorHigh
        {
            get { return m_mountainColorHigh; }
            set
            {
                m_mountainColorHigh = value;
                OnPropertyChanged();
            }
        }

        public EnvironmentData()
        {
            GenOptions = new GenerationOptions();

            m_seaColorLow = new Color(0, 0, 116, 255);
            m_sandColorLow = new Color(170, 166, 27, 255);
            m_forestColorLow = new Color(0, 159, 21, 255);
            m_mountainColorLow = new Color(197, 197, 202, 255);

            m_seaColorHigh = new Color(0, 0, 255, 255);
            m_sandColorHigh = new Color(206, 202, 49, 255);
            m_forestColorHigh = new Color(22, 88, 31, 255);
            m_mountainColorHigh = new Color(248, 248, 248, 255);
        }

        public Colorizer BuildTerrainColorizer()
        {
            var terrainColorizer = new Colorizer();
            terrainColorizer.AddColorRange(m_seaColorLow, m_seaColorHigh, 0f, GenOptions.SeaLevel);
            terrainColorizer.AddColorRange(m_sandColorLow, m_sandColorHigh, GenOptions.SeaLevel, GenOptions.SandDistance);
            terrainColorizer.AddColorRange(m_forestColorLow, m_forestColorHigh, GenOptions.SandDistance, GenOptions.ForestDistance);
            terrainColorizer.AddColorRange(m_mountainColorLow, m_mountainColorHigh, GenOptions.ForestDistance, GenOptions.MountainDistance);

            return terrainColorizer;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
