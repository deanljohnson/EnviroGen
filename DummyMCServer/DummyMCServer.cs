using System;
using System.IO.Pipes;

namespace DummyMCServer
{
    class DummyMCServer
    {
        private static string OutputPipeName;

        static void Main(string[] args)
        {
            OutputPipeName = args[0];

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
                    {
                        var response = SendCommandToEnviroGen(bytes);
                    }
                    
                }
            }
        }

        private static byte[] SendCommandToEnviroGen(byte[] cmd)
        {
            if (ServerCommands.CommandLengths[cmd[0]] != cmd.Length - 1)
            {
                Console.WriteLine($"Command {ServerCommands.CommandNames[cmd[0]]} was not input with the correct number of arguments, dropping command");
                return null;
            }

            var pipe = new NamedPipeClientStream(".", OutputPipeName, PipeDirection.InOut);
            Console.WriteLine("Connecting to server");
            pipe.Connect();

            Console.WriteLine($"Sending {ServerCommands.CommandNames[cmd[0]]} to server.");
            pipe.Write(cmd, 0, cmd.Length);

            return ReadCommandFromEnviroGen(pipe);
        }

        private static byte[] ReadCommandFromEnviroGen(NamedPipeClientStream pipe)
        {
            //We read into this buffer because this will block until we get a byte,
            //which is what we want in this case.
            var commandRead = new byte[1];
            pipe.Read(commandRead, 0, 1);

            Console.WriteLine($"Raw command read: {commandRead[0]}");

            var commandLength = MinecraftEnviroGenServer.ServerCommands.CommandLengths[commandRead[0]];
            var input = new byte[1 + commandLength];

            input[0] = commandRead[0];

            if (commandLength > 0)
            {
                //TODO: handle improper number of bytes sent, currently this just blocks if the amount is too low
                //Read all the command args into the input array
                pipe.Read(input, 1, commandLength);
            }

            return input;
        }
    }
}
