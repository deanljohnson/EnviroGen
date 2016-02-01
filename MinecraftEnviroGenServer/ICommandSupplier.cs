namespace MinecraftEnviroGenServer
{
    interface ICommandSupplier
    {
        byte[] NextCommand { get; }
    }
}
