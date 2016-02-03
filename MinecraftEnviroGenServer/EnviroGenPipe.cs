using System;
using System.IO.Pipes;

namespace MinecraftEnviroGenServer
{
    public class EnviroGenPipeStream
    {
        private NamedPipeServerStream m_Pipe { get; }

        public EnviroGenPipeStream(string pipeName, int numThreads)
        {
            m_Pipe = new NamedPipeServerStream(pipeName, PipeDirection.InOut, numThreads, PipeTransmissionMode.Byte, PipeOptions.None);
        }

        public void Close()
        {
            m_Pipe.Close();
        }

        public void Dispose()
        {
            m_Pipe.Dispose();
        }

        public void WaitForConnection()
        {
            m_Pipe.WaitForConnection();
        }

        public void Flush()
        {
            m_Pipe.Flush();
        }

        public void Write(byte[] b, int offet, int len)
        {
            m_Pipe.Write(b, offet, len);
        }

        public byte[] ReadNextRequest()
        {
            //We read into this buffer because this will block until we get a byte,
            //which is what we want in this case.
            var commandRead = new byte[1];
            m_Pipe.Read(commandRead, 0, 1);

            Console.WriteLine($"Command read: {ServerCommands.CommandNames[commandRead[0]]}");

            var commandLength = ServerCommands.CommandLengths[commandRead[0]];
            var input = new byte[1 + commandLength];

            input[0] = commandRead[0];

            if (commandLength > 0)
            {
                //TODO: handle improper number of bytes sent, currently this just blocks if the amount is too low
                //Read all the command args into the input array
                m_Pipe.Read(input, 1, commandLength);
            }

            return input;
        }
    }
}
