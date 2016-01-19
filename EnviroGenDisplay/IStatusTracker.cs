namespace EnviroGenDisplay
{
    public interface IStatusTracker
    {
        string CurrentMessage { get; }

        void PushMessage(string msg);
        void PopMessage();
    }
}