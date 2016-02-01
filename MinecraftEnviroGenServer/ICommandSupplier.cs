namespace MinecraftEnviroGenServer
{
    public interface ICommandSupplier
    {
        /// <summary>
        /// Copies and returns the byte array representing the current command.
        /// </summary>
        /// <param name="flush">Whether or not to wipe the current command immediately after copying.</param>
        /// <returns>the copied command</returns>
        byte[] GetCopyOfCommand(bool flush);

        /// <summary>
        /// Set's the current command to the given byte array.
        /// </summary>
        /// <param name="cmd"></param>
        void SetCommand(byte[] cmd);
    }
}
