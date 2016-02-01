namespace MinecraftEnviroGenServer
{
    //TODO: enforce locked access to NextCommand
    public class EnviroGenServerCommander : ICommandSupplier
    {
        public byte[] NextCommand { get; set; }

        public EnviroGenServerCommander()
        {
            NextCommand = new byte[0];
        }

        public void FlushCommand()
        {
            NextCommand = new byte[0];
        }
    }
}