using System;
using System.Text.RegularExpressions;
using SFML.Graphics;
using SFML.Window;
using SFMLUI;

namespace EnviroGen
{
    class OptionsMenu : Drawable
    {
        private readonly EnvironmentGenerator m_generator;
        private readonly UserInterface m_ui;
        private readonly UIContainer m_container;

        public OptionsMenu(EnvironmentGenerator generator)
        {
            m_generator = generator;
            m_ui = new UserInterface(Program.DisplayWindow);
            m_container = new UIContainer(new Vector2f(100, 100));

            //Used to validate text entry's in the UI
            var numbersOnly = new Regex(@"^\d$");

            //All the fields that will be editable in the UI
            var heightField = new UILabelWithField(new Vector2f(0, 0), SetHeightOctave, "Height Map Octave Count: ", m_generator.HeightMapGenerator.OctaveCount.ToString(), numbersOnly);
            var seaLevelField = new UILabelWithField(new Vector2f(0, 0), SetSeaLevel, "Sea Level: ", Terrain.SeaLevel.ToString(), numbersOnly);
            var sandDistanceField = new UILabelWithField(new Vector2f(0, 0), SetSandDistance, "Sand Distance: ",
                Terrain.SandDistance.ToString(), numbersOnly);
            var forestDistanceField = new UILabelWithField(new Vector2f(0, 0), SetForestDistance, "Forest Distance: ",
                Terrain.ForestDistance.ToString(), numbersOnly);
            var mountainDistanceField = new UILabelWithField(new Vector2f(0, 0), SetMountainDistance,
                "Mountain Distance: ", Terrain.MountainDistance.ToString(), numbersOnly);
            var cloudOctaveField = new UILabelWithField(new Vector2f(0, 0), SetCloudOctave, "Cloud Map Octave Count: ",
                m_generator.CloudGenerator.OctaveCount.ToString(), numbersOnly);

            //Conveniently organizes fields into a single column
            var grid = new UIGridContainer(new Vector2f(5, 0), new Vector2u(1, 6), new Vector2f(10, 20))
            {
                heightField,
                seaLevelField,
                sandDistanceField,
                forestDistanceField,
                mountainDistanceField,
                cloudOctaveField
            };

            var background = new UIRectangle(new Vector2f(0, 0), new Vector2f(grid.Size.X + 30, grid.Size.Y), Color.White, 2, Color.Black);

            m_container.Add(background);
            m_container.Add(grid);

            m_ui.AddContainer(m_container);
        }

        public void Update()
        {
            m_ui.Update();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(m_ui, states);
        }

        public void PushValues()
        {
            foreach (var elem in m_container.OfType<ITextualOutputElement>(true))
            {
                elem.OutputValue();
            }
        }

        private void SetHeightOctave(string text, UITextField field)
        {
            m_generator.HeightMapGenerator.OctaveCount = Int16.Parse(text);
        }

        private static void SetMountainDistance(string text, UITextField field)
        {
            Terrain.MountainDistance = Int16.Parse(text);
        }

        private static void SetForestDistance(string text, UITextField field)
        {
            Terrain.ForestDistance = Int16.Parse(text);
        }

        private static void SetSandDistance(string text, UITextField field)
        {
            Terrain.SandDistance = Int16.Parse(text);
        }

        private static void SetSeaLevel(string text, UITextField field)
        {
            Terrain.SeaLevel = Int16.Parse(text);
        }

        private void SetCloudOctave(string text, UITextField field)
        {
            m_generator.CloudGenerator.OctaveCount = Int16.Parse(text);
        }
    }
}
