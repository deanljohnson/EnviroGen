using EnviroGen;
using EnviroGen.Coloring;

namespace EnviroGenDisplay
{
    public interface IEnvironment
    {
        Environment Environment { get; }

        void Update();

        Colorizer GetColorizer();
        void AddColor(ColorRange c);
        void RemoveColor(ColorRange c);
        void ApplyColorizer();
    }
}
