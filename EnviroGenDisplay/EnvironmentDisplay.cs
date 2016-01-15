using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using EnviroGen;
using EnviroGen.Coloring;
using EnviroGen.Continents;
using EnviroGen.Erosion;
using EnviroGen.HeightMaps;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace EnviroGenDisplay
{
    static class EnvironmentDisplay
    {
        private static EnvironmentDrawable m_Environment { get; }
        private static RenderWindow m_Window { get; }

        static EnvironmentDisplay()
        {
            m_Window = new RenderWindow(new VideoMode(1400, 800, 32), "EnviroGen Display", Styles.Default);
            m_Window.SetVerticalSyncEnabled(true);
            m_Window.SetActive(false);
            m_Window.SetVisible(true);
            m_Window.SetKeyRepeatEnabled(true);

            m_Window.Closed += WindowClosedEvent;
            m_Window.MouseWheelMoved += MouseWheelEvent;
            m_Window.KeyPressed += KeyPressedEvent;

            m_Environment = new EnvironmentDrawable(null, null);
        }

        public static void Update(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            lock (m_Window)
            {
                while (m_Window.IsOpen)
                {
                    UpdateDisplay();
                }
            }
        }

        private static void UpdateDisplay()
        {
            m_Window.DispatchEvents();

            lock (m_Environment)
            {
                if (m_Environment.Dirty)
                {
                    m_Environment.Dirty = false;
                    m_Window.Clear(Color.Black);
                    m_Window.Draw(m_Environment);
                    m_Window.Display();
                }
            }
        }

        private static void WindowClosedEvent(object sender, EventArgs e)
        {
            m_Window.Close();
        }

        private static void MouseWheelEvent(object sender, MouseWheelEventArgs e)
        {
            var view = m_Window.GetView();

            view.Zoom(e.Delta > 0 ? .9f : 1.1f);

            m_Window.SetView(view);
        }

        private static void KeyPressedEvent(object sender, KeyEventArgs e)
        {
            var view = m_Window.GetView();
            const float speed = 10f;

            switch (e.Code)
            {
                case Keyboard.Key.Up:
                    view.Move(new Vector2f(0f, -speed));
                    break;
                case Keyboard.Key.Down:
                    view.Move(new Vector2f(0f, speed));
                    break;
                case Keyboard.Key.Left:
                    view.Move(new Vector2f(-speed, 0f));
                    break;
                case Keyboard.Key.Right:
                    view.Move(new Vector2f(speed, 0f));
                    break;
            }

            m_Window.SetView(view);
        }

        public static void GenerateHeightMap(object sender, DoWorkEventArgs e)
        {
            var arg = e.Argument as EnvironmentData;

            if (arg == null)
            {
                var argList = e.Argument as IEnumerable<EnvironmentData>;

                if (argList == null) throw new ArgumentNullException();

                GenerateHeightMap(argList.ToList());
            }
            else
            {
                GenerateHeightMap(arg);
            }        
        }

        public static void GenerateHeightMap(List<EnvironmentData> dataList)
        {
            throw new NotImplementedException();
        }

        public static void GenerateHeightMap(EnvironmentData data)
        {
            var options = data.ToGenerationOptions();
            var random = new Random();
            options.Seed = (options.Seed == -1) ? random.Next(10000) : options.Seed;

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
                    m_Environment.Terrain.Colorize();
                }
                else
                {
                    m_Environment.Terrain = new Terrain(terrainHeightMap);
                }

                m_Environment.Dirty = true;
            }
        }

        public static void SetColorMapping(Colorizer colorizer)
        {
            lock (m_Environment)
            {
                if (m_Environment.Terrain == null) return;

                m_Environment.Terrain.Colorizer = colorizer;
                m_Environment.Terrain.Colorize();

                m_Environment.Dirty = true;
            }
        }

        public static void SetColorMapping(List<ColorRange> colorRanges)
        {
            lock (m_Environment)
            {
                if (m_Environment.Terrain == null) return;

                m_Environment.Terrain.Colorizer = new Colorizer(colorRanges);
                m_Environment.Dirty = true;
            }
        }

        public static void BuildContinents(ContinentGenerationData data)
        {
            lock (m_Environment)
            {
                if (m_Environment.Terrain == null) return;
                ContinentGenerator.BuildContinents(m_Environment.Terrain.HeightMap, data);
                m_Environment.Terrain.HeightMap.Normalize();
                m_Environment.Terrain.Colorize();
                m_Environment.Dirty = true;
            }
        }

        public static void ErodeHeightMap(ErosionData data, bool improvedThermal = false)
        {
            lock (m_Environment)
            {
                if (m_Environment.Terrain == null) return;

                var erosionData = data as ThermalErosionData;
                if (erosionData != null)
                {
                    if (improvedThermal) ImprovedThermalErosion.Erode(m_Environment.Terrain.HeightMap, erosionData);
                    else ThermalErosion.Erode(m_Environment.Terrain.HeightMap, erosionData);

                }
                else
                {
                    var hydraulicErosionData = data as HydraulicErosionData;
                    if (hydraulicErosionData != null)
                    {
                        HydraulicErosion.Erode(m_Environment.Terrain.HeightMap, hydraulicErosionData);
                    }
                }

                m_Environment.Terrain.Colorize();
                m_Environment.Dirty = true;
            }
        }
    }
}
