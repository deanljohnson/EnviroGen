using EnviroGen;

namespace EnviroGenDisplay
{
    public interface IDisplayedEnvironment
    {
        Environment Environment { get; }

        void Update();
    }
}
