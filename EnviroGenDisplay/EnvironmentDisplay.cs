using System;
using System.ComponentModel;
using EnviroGen;
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

        public static void GenerateFromData()
        {
            EnvironmentGenerator enviroGen;

            lock (EnvironmentData)
            {
                var random = new Random();
                var heightSeed = Int32.Parse(EnvironmentData.HeightMapSeed);
                heightSeed = heightSeed == -1 ? random.Next(5000) : heightSeed;
                var cloudSeed = Int32.Parse(EnvironmentData.CloudMapSeed);
                cloudSeed = cloudSeed == -1 ? random.Next(5000) : cloudSeed;

                EnvironmentData.HeightMapSeed = heightSeed.ToString();
                EnvironmentData.CloudMapSeed = cloudSeed.ToString();

                enviroGen = EnvironmentData.BuildEnvironmentGenerator();
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
                Environment.Terrain.Colorize(data.BuildTerrainColorizer());
            }
        }
    }
}
