using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace MinecraftEnviroGenServer
{
    public class EnviroGenPipeServer
    {
        private string m_OutputPipeName { get; }
        private string m_InputPipeName { get; }
        private int m_NumThreads { get; }
        private bool m_Running { get; set; }
        private Thread m_OutputThread { get; set; }
        private Thread m_InputThread { get; set; }

        public EnviroGenServerCommander Commander { get; set; }

        public EnviroGenPipeServer(string outputPipeName, string inputPipeName, int numThreads)
        {
            m_OutputPipeName = outputPipeName;
            m_InputPipeName = inputPipeName;
            m_NumThreads = numThreads;
        }

        public void Run()
        {
            m_Running = true;

            m_OutputThread = new Thread(ServerOutputLoop);
            m_OutputThread.Start();

            //m_InputThread = new Thread(ServerInputLoop);
            //m_InputThread.Start();
        }

        private void ServerOutputLoop(object data)
        {
            while (m_Running)
            {
                ProcessNextOutput();
            }
        }

        private void ServerInputLoop(object data)
        {
            while (m_Running)
            {
                ProcessNextInput();
            }
        }

        private void ProcessNextOutput()
        {
            try
            {
                var pipeStream = new NamedPipeServerStream(m_OutputPipeName, PipeDirection.InOut, m_NumThreads, PipeTransmissionMode.Message, PipeOptions.None);
                //Wait till the Java side connects to the pipe
                pipeStream.WaitForConnection();

                //Spawn a new thread and keep waiting
                var t = new Thread(ProcessClientRequestOutput);
                t.Start(pipeStream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ProcessNextInput()
        {
            try
            {
                var pipeStream = new NamedPipeServerStream(m_InputPipeName, PipeDirection.InOut, m_NumThreads, PipeTransmissionMode.Message, PipeOptions.None);
                var reader = new StreamReader(pipeStream);

                //Read till stream is empty
                while (true)
                {
                    var line = reader.ReadLine();

                    if (line != null)
                    {
                        //Spawn a new thread and keep waiting
                        var t = new Thread(ProcessClientInputCommand);
                        t.Start(line);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ProcessClientRequestOutput(object o)
        {
            var pipeStream = (NamedPipeServerStream)o;

            try
            {
                var writer = new StreamWriter(pipeStream);
                writer.Write(Commander.NextCommand + "\n");
                writer.Flush();
                pipeStream.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                pipeStream.Close();
                pipeStream.Dispose();
            }

            pipeStream.Close();
            pipeStream.Dispose();
        }

        private void ProcessClientInputCommand(object line)
        {
            Debug.Assert(line is string);
        }
    }
}
