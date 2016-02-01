using System;
using System.IO.Pipes;
using System.Linq;
using System.Threading;

namespace DummyMCServer
{
    class DummyMCServer
    {
        private static bool InputMode = true;
        private static string OutputPipeName;
        private static string InputPipeName;

        static void Main(string[] args)
        {
            OutputPipeName = args[0];
            InputPipeName = args[1];

            var outputListener = new Thread(OutputListenerThread);
            outputListener.Start();

            while (true)
            {
                var line = Console.ReadLine();

                if (line?.ToLower() == "exit")
                {
                    break;
                }
                else if (line != null)
                {
                    var byteStrings = line.Split(' ');
                    var bytes = new byte[byteStrings.Length];

                    var successfulParse = true;
                    for (var i = 0; i < byteStrings.Length; i++)
                    {
                        try
                        {
                            bytes[i] = byte.Parse(byteStrings[i]);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine($"{byteStrings[i]} cannot be parsed as a byte, dropping command");
                            successfulParse = false;
                        }
                    }

                    if (successfulParse)
                        SendCommandToServer(bytes);
                }
            }
        }

        private static void OutputListenerThread(object data)
        {
            while (true)
            {
                var outputPipe = new NamedPipeClientStream(".", OutputPipeName, PipeDirection.InOut);

                outputPipe.Connect();

                var command = (byte)outputPipe.ReadByte();
                var commandLength = ServerCommands.CommandLengths[command];

                var commandBytes = new byte[commandLength + 1];
                commandBytes[0] = command;

                if (commandLength > 0)
                {
                    outputPipe.Read(commandBytes, 1, commandLength);
                }

                var commandString = commandBytes.Aggregate(string.Empty, 
                                    (current, commandByte) => current + (commandByte + " "));

                Console.WriteLine($"Received from EnviroGen: {commandString}");
            }
        }

        private static void SendCommandToServer(byte[] cmd)
        {
            if (ServerCommands.CommandLengths[cmd[0]] != cmd.Length - 1)
            {
                Console.WriteLine($"Command {ServerCommands.CommandNames[cmd[0]]} was not input with the correct number of arguments, dropping command");
                return;
            }

            var inputPipe = new NamedPipeClientStream(".", InputPipeName, PipeDirection.InOut);
            Console.WriteLine("Connecting to server");
            inputPipe.Connect();

            Console.WriteLine($"Sending {ServerCommands.CommandNames[cmd[0]]} to server.");
            inputPipe.Write(cmd, 0, cmd.Length);
        }
    }
}
