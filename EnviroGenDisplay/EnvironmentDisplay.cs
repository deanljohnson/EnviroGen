using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
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

            view.Zoom(e.Delta > 0 ? .9f : 1.1f);

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

            lock (Environment)
            {
                if (data.Combining && Environment.Terrain != null && Environment.Terrain.HeightMap != null)
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

        public static void SetColorMapping(Colorizer colorizer)
        {
            lock (Environment)
            {
                if (Environment.Terrain == null) return;

                Environment.Terrain.Colorizer = colorizer;
                Environment.Terrain.Colorize();
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
