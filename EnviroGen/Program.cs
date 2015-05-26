using System;
using SFML.Graphics;
using SFML.Window;

namespace EnviroGen
{
    static class Program
    {
        private static bool DisplayingOptions;
        private static EnvironmentGenerator Generator;
        private static Environment Environment;
        private static OptionsMenu OptionsMenu;

        public static RenderWindow DisplayWindow;
        public static readonly Random Random = new Random();

        public static void Main(string[] args)
        {
            InitializeDisplayWindow();

            Generator = new EnvironmentGenerator(DisplayWindow.Size, 7, 6);
            Generator.Generate(); //Generate the first environment
            Environment = Generator.Environment;

            while (DisplayWindow.IsOpen())
            {
                DisplayWindow.DispatchEvents();

                Environment.Update();
                DisplayWindow.Clear(Color.Black);
                DisplayWindow.Draw(Environment);

                if (DisplayingOptions)
                {
                    OptionsMenu.Update(); 
                    DisplayWindow.Draw(OptionsMenu);
                }

                DisplayWindow.Display();
            }
        }

        /// <summary>
        /// Setup general window data
        /// </summary>
        private static void InitializeDisplayWindow()
        {
            DisplayWindow = new RenderWindow(new VideoMode(1400, 800, 32), "EnviroGen Display", Styles.Default);
            DisplayWindow.SetVerticalSyncEnabled(true);
            DisplayWindow.SetActive(false);
            DisplayWindow.SetVisible(true);

            DisplayWindow.Closed += WindowClosedEvent;
            DisplayWindow.KeyReleased += KeyPressedEvent;
        }

        private static void KeyPressedEvent(object sender, KeyEventArgs e)
        {
            if (e.Code.Equals(Keyboard.Key.Escape))
            {
                DisplayingOptions = !DisplayingOptions;
                if (DisplayingOptions)
                {
                    OptionsMenu = new OptionsMenu(Generator);
                }
                if (!DisplayingOptions)
                {
                    Generator.Generate(); //Options may have changed, regenerate the environment
                    Environment = Generator.Environment;
                }
            }
        }

        private static void WindowClosedEvent(object sender, EventArgs e)
        {
            DisplayWindow.Close();
        }
    }
}
