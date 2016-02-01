namespace MinecraftEnviroGenServer
{
    public class EnviroGenServerCommander : ICommandSupplier
    {
        private byte[] m_NextCommand { get; set; }

        public EnviroGenServerCommander()
        {
            m_NextCommand = new byte[0];
        }

        public byte[] GetCopyOfCommand(bool flush)
        {
            var copy = new byte[m_NextCommand.Length];

            lock (m_NextCommand)
            {
                m_NextCommand.CopyTo(copy, 0);

                if (flush) m_NextCommand = new byte[0];
            }

            return copy;
        }

        public void SetCommand(byte[] cmd)
        {
            lock (m_NextCommand)
            {
                m_NextCommand = new byte[cmd.Length];
                cmd.CopyTo(m_NextCommand, 0);
            }
        }
    }
}