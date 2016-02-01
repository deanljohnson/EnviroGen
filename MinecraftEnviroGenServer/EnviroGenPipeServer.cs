using System;
using System.Diagnostics;
using System.IO.Pipes;
using System.Threading;

namespace MinecraftEnviroGenServer
{
    //TODO: Fix the InputThread operations
    public class EnviroGenPipeServer
    {
        private string m_OutputPipeName { get; }
        private string m_InputPipeName { get; }
        private int m_NumThreads { get; }
        private bool m_Running { get; set; }
        private Thread m_OutputThread { get; set; }
        private Thread m_InputThread { get; set; }

        public ICommandSupplier Commander { get; set; }

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
            //m_OutputThread.Start();

            m_InputThread = new Thread(ServerInputLoop);
            m_InputThread.Start();
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
                var pipeStream = new NamedPipeServerStream(m_OutputPipeName, PipeDirection.Out, m_NumThreads, PipeTransmissionMode.Byte, PipeOptions.None);
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
                var pipeStream = new NamedPipeServerStream(m_InputPipeName, PipeDirection.InOut, m_NumThreads, PipeTransmissionMode.Byte, PipeOptions.None);

                pipeStream.WaitForConnection();

                Console.WriteLine("New Connection on Input Pipe");

                while (true)
                {
                    var commandInt = pipeStream.ReadByte();

                    if (commandInt >= 0)
                    {
                        var command = (byte)commandInt;
                        Console.WriteLine($"Raw command read: {command}");

                        var commandLength = InputCommands.CommandLengths[command];
                        var input = new byte[1 + commandLength];

                        input[0] = command;

                        if (commandLength > 0)
                        {
                            //TODO: handle improper number of bytes sent, currently this just blocks if the amount is too low
                            //Read all the command args into the input array
                            pipeStream.Read(input, 1, commandLength);
                        }

                        //Spawn a new thread and keep waiting
                        var t = new Thread(ProcessClientInputCommand);
                        t.Start(input);

                        break;
                    }
                }

                pipeStream.Close();
                pipeStream.Dispose();
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
                var cmd = Commander.GetCopyOfCommand(true);
                pipeStream.Write(cmd, 0, cmd.Length);
                pipeStream.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            pipeStream.Close();
            pipeStream.Dispose();
        }

        private void ProcessClientInputCommand(object inputObject)
        {
            Debug.Assert(inputObject is byte[]);
            var input = (byte[]) inputObject;

            switch (input[0])
            {
                case InputCommands.NULL:
                    Console.WriteLine($"Received Input: {nameof(InputCommands.NULL)}");
                    break;
                case InputCommands.START_WORLD_GEN:
                    Console.WriteLine($"Received Input: {nameof(InputCommands.START_WORLD_GEN)}");
                    break;
                case InputCommands.UPDATE_REQUEST:
                    Console.WriteLine($"Received Input: {nameof(InputCommands.UPDATE_REQUEST)}");
                    break;
                case InputCommands.START_SIMULATING:
                    Console.WriteLine($"Received Input: {nameof(InputCommands.START_SIMULATING)}");
                    break;
            }
        }
    }
}
