using System;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using System.Diagnostics;

namespace MinecraftEnviroGenServer
{
    static class DummyMCServer
    {
        private static string m_OutputPipeName;
        private static Stopwatch m_Watch { get; set; }

        public static void Start(string pipeName)
        {
            m_OutputPipeName = pipeName;

            m_Watch = new Stopwatch();

            new Thread(ServerLoop).Start();
        }

        private static void ServerLoop(object data)
        {
            while (true)
            {
                Console.Write("Enter command to send to EnviroGen: ");
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
                        m_Watch.Start();
                        var response = SendCommandToEnviroGen(bytes);
                        m_Watch.Stop();
                        Console.WriteLine($"The server took {m_Watch.ElapsedMilliseconds}ms to respond to the command.");
                        m_Watch.Reset();

                        var responseString = response.Aggregate(string.Empty, (last, current) => last + " " + current);
                        Console.WriteLine(responseString);
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

            var pipe = new NamedPipeClientStream(".", m_OutputPipeName, PipeDirection.InOut);
            Console.WriteLine("Connecting to server");
            pipe.Connect();

            Console.WriteLine($"Sending {ServerCommands.CommandNames[cmd[0]]} to EnviroGen.");
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

            var commandLength = ServerCommands.CommandLengths[commandRead[0]];
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
