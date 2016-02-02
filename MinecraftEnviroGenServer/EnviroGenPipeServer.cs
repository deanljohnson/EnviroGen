using System;
using System.Threading;

namespace MinecraftEnviroGenServer
{
    public class EnviroGenPipeServer
    {
        private string m_PipeName { get; }
        private int m_NumThreads { get; }
        private bool m_Running { get; set; }
        private Thread m_OutputThread { get; set; }

        public ICommandHandler Handler { get; set; }

        public EnviroGenPipeServer(string pipeName, int numThreads)
        {
            m_PipeName = pipeName;
            m_NumThreads = numThreads;
        }

        public void Run()
        {
            m_Running = true;

            m_OutputThread = new Thread(ServerOutputLoop);
            m_OutputThread.Start();
        }

        private void ServerOutputLoop(object data)
        {
            while (m_Running)
            {
                ProcessNextOutput();
            }
        }

        private void ProcessNextOutput()
        {
            try
            {
                var pipeStream = new EnviroGenPipeStream(m_PipeName, m_NumThreads);

                //Wait till the Java side connects to the pipe
                pipeStream.WaitForConnection();

                Console.WriteLine("New Connection on pipe");

                //Spawn a new thread and keep waiting
                var t = new Thread(ProcessClientRequest);
                t.Start(pipeStream);

                //ProcessClientRequest(pipeStream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ProcessClientRequest(object o)
        {
            var pipeStream = (EnviroGenPipeStream)o;

            try
            {
                var request = pipeStream.ReadNextRequest();
                var response = HandleRequest(request);

                Console.WriteLine($"Sending {ServerCommands.CommandNames[response[0]]} to MC Server");
                pipeStream.Write(response, 0, response.Length);
                pipeStream.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            pipeStream.Dispose();
        }

        private byte[] HandleRequest(byte[] request)
        {
            return Handler.HandleRequest(request);
        }
    }
}
