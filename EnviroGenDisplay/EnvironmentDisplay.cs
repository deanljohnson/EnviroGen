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
using SFML.Window;
using Environment = EnviroGen.Environment;

namespace EnviroGenDisplay
{
    static class EnvironmentDisplay
    {
        private static Environment Environment { get; set; }
        private static RenderWindow Window { get; set; }

        public static EnvironmentData EnvironmentData { get; set; }

        static EnvironmentDisplay()
        {
            Window = new RenderWindow(new VideoMode(1400, 800, 32), "EnviroGen Display", Styles.Default);
            Window.SetVerticalSyncEnabled(true);
            Window.SetActive(false);
            Window.SetVisible(true);
            Window.SetKeyRepeatEnabled(true);

            Window.Closed += WindowClosedEvent;
            Window.MouseWheelMoved += MouseWheelEvent;
            Window.KeyPressed += KeyPressedEvent;

            Environment = new Environment(null, null);
            EnvironmentData = new EnvironmentData();
        }

        public static void Update(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            lock (Window)
            {
                while (Window.IsOpen())
                {
                    UpdateDisplay();
                }
            }
        }

        private static void UpdateDisplay()
        {
            Window.DispatchEvents();

            Window.Clear(Color.Black);

            lock (Environment)
            {
                Window.Draw(Environment);
            }

            Window.Display();
        }

        private static void WindowClosedEvent(object sender, EventArgs e)
        {
            Window.Close();
        }

        private static void MouseWheelEvent(object sender, MouseWheelEventArgs e)
        {
            var view = Window.GetView();

            if (e.Delta > 0)
            {
                view.Zoom(.9f);
            }
            else
            {
                view.Zoom(1.1f);
            }

            Window.SetView(view);
        }

        private static void KeyPressedEvent(object sender, KeyEventArgs e)
        {
            var view = Window.GetView();
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

            Window.SetView(view);
        }

        public static void GenerateHeightMap()
        {
            HeightMap terrainHeightMap;
            bool combine;

            lock (EnvironmentData)
            {
                combine = EnvironmentData.Combining;
                var random = new Random();
                var seed = EnvironmentData.Seed;
                seed = seed == -1 ? random.Next(5000) : seed;

                terrainHeightMap = HeightMapGenerator.GenerateHeightMap(EnvironmentData.SizeX, EnvironmentData.SizeY, EnvironmentData.OctaveCount, EnvironmentData.Gain, EnvironmentData.Frequency, seed, EnvironmentData.Modifiers.ToList());
            }

            if (terrainHeightMap == null)
            {
                throw new NullReferenceException("Error in terrain height map generation, EnvironmentDisplay.GenerateHeightMap");
            }
            //terrainHeightMap.Normalize();

            lock (Environment)
            {
                if (combine && Environment.Terrain != null && Environment.Terrain.HeightMap != null)
                {
                    Environment.Terrain.HeightMap.CombineWith(terrainHeightMap);
                    Environment.Terrain.Colorize();
                }
                else
                {
                    Environment.Terrain = new Terrain(terrainHeightMap);
                }
            }
        }

        public static void SetColorMapping(List<ColorRange> colorRanges)
        {
            lock (Environment)
            {
                if (Environment.Terrain == null) return;

                Environment.Terrain.Colorizer = new Colorizer(colorRanges);
                Environment.Terrain.Colorize();
            }
        }

        public static void BuildContinents(ContinentGenerationData data)
        {
            lock (Environment)
            {
                if (Environment.Terrain == null) return;
                ContinentGenerator.BuildContinents(Environment.Terrain.HeightMap, data);
                Environment.Terrain.HeightMap.Normalize();
                Environment.Terrain.Colorize();
            }
        }

        public static void ErodeHeightMap(ErosionData data, bool improvedThermal = false)
        {
            lock (Environment)
            {
                if (Environment.Terrain == null) return;

                var erosionData = data as ThermalErosionData;
                if (erosionData != null)
                {
                    if (improvedThermal) ImprovedThermalErosion.Erode(Environment.Terrain.HeightMap, erosionData);
                    else ThermalErosion.Erode(Environment.Terrain.HeightMap, erosionData);
                    
                }
                else
                {
                    var hydraulicErosionData = data as HydraulicErosionData;
                    if (hydraulicErosionData != null)
                    {
                        HydraulicErosion.Erode(Environment.Terrain.HeightMap, hydraulicErosionData);
                    }
                }

                Environment.Terrain.Colorize();
            }
        }
    }
}
