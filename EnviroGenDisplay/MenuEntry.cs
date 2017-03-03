namespace EnviroGenDisplay
{
    public class MenuEntry
    {
        public string Header { get; }

        public MenuEntry(string header)
        {
            Header = header;
        }
    }

    public class MenuEntry<TOnClickType> : MenuEntry
    {
        public TOnClickType OnClick { get; protected set; }

        public MenuEntry(string header, TOnClickType onClick)
            : base(header)
        {
            OnClick = onClick;
        }
    }
}
