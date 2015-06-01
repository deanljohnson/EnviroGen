using System;
using System.ComponentModel;
using System.Diagnostics;
using EnviroGen;
using SFML.Graphics;
using SFML.Window;
using Environment = EnviroGen.Environment;

namespace EnviroGenDisplay
{
    static class EnvironmentDisplay
    {
        private static Environment Environment { get; set; }
        private static EnvironmentData EnvironmentData { get; set; }
        private static RenderWindow Window { get; set; }

        static EnvironmentDisplay()
        {
            Window = new RenderWindow(new VideoMode(1400, 800, 32), "EnviroGen Display", Styles.Default);
            Window.SetVerticalSyncEnabled(true);
            Window.SetActive(false);
            Window.SetVisible(true);

            Window.Closed += WindowClosedEvent;

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

        public static void GenerateFromData(EnvironmentData data)
        {
            EnvironmentGenerator enviroGen;

            lock (EnvironmentData)
            {
                EnvironmentData = data;

                var random = new Random();
                var heightSeed = Int32.Parse(EnvironmentData.HeightMapSeed);
                heightSeed = heightSeed == -1 ? random.Next(5000) : heightSeed;
                var cloudSeed = Int32.Parse(EnvironmentData.CloudMapSeed);
                cloudSeed = cloudSeed == -1 ? random.Next(5000) : cloudSeed;

                enviroGen = new EnvironmentGenerator
                {
                    SizeX = Int32.Parse(EnvironmentData.SizeX),
                    SizeY = Int32.Parse(EnvironmentData.SizeY),
                    HeightMapOctaveCount = Int32.Parse(EnvironmentData.HeightMapOctaveCount),
                    CloudMapOctaveCount = Int32.Parse(EnvironmentData.CloudMapOctaveCount),
                    NumContinents = Int32.Parse(EnvironmentData.NumberOfContinents),
                    MinimumContinentSize = Int32.Parse(EnvironmentData.MinimumContinentSize),
                    MaximumContinentSize = Int32.Parse(EnvironmentData.MaximumContinentSize),
                    SeaLevel = Int32.Parse(EnvironmentData.SeaLevel),
                    SandDistance = Int32.Parse(EnvironmentData.SandDistance),
                    ForestDistance = Int32.Parse(EnvironmentData.MountainDistance),
                    HeightMapSeed = heightSeed,
                    CloudMapSeed = cloudSeed,
                    NoiseRoughness = float.Parse(EnvironmentData.NoiseRoughness),
                    NoiseScale = float.Parse(EnvironmentData.NoiseScale)
                };
            }

            lock (Environment)
            {
                Environment = enviroGen.Generate();
            }
        }

        public static void RefreshColorMapping(EnvironmentData data)
        {
            lock (EnvironmentData)
            {
                EnvironmentData = data;
            }

            lock (Environment)
            {
                Environment.Terrain.SetColorMapping(Int32.Parse(data.SeaLevel), Int32.Parse(data.SandDistance), Int32.Parse(data.ForestDistance), Int32.Parse(data.MountainDistance));
                Environment.Terrain.ColorMap();
            }
        }
    }
}
