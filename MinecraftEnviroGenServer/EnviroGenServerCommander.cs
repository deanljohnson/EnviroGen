namespace MinecraftEnviroGenServer
{
    public class EnviroGenServerCommander : ICommandSupplier
    {
        private string m_NextCommand;
        public string NextCommand
        {
            get { return m_NextCommand.Length > 0 ? m_NextCommand : "0"; }
            set { m_NextCommand = value; }
        }

        public EnviroGenServerCommander()
        {
            m_NextCommand = "";
        }
    }
}