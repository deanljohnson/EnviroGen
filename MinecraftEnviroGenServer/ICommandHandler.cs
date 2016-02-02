namespace MinecraftEnviroGenServer
{
    public interface ICommandHandler
    {
        byte[] HandleRequest(byte[] request);
    }
}
