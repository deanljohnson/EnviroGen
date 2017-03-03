namespace EnviroGenDisplay
{
    public class MenuEntry<TOnClickType>
    {
        public string Header { get; }
        public TOnClickType OnClick { get; protected set; }

        public MenuEntry(string header, TOnClickType onClick)
        {
            OnClick = onClick;
            Header = header;
        }
    }
}
