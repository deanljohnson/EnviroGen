using System;
using System.Diagnostics;

namespace MinecraftEnviroGenServer
{
    class MinecraftEnviroGenServer
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(
                    "ERROR: Expected to find the path to the server executable as a command line argument.");
                Console.Write("Press ENTER to exit, or enter DUMMY to launch the dummy server:\n");
                var line = Console.ReadLine();

                if (line?.ToLower() == "dummy")
                {
                    StartDummyMCServer();
                }
                else
                {
                    return;
                }
            }
            else
            {
                StartMCServer(args[0]);
            }

            StartPipeServer("EnviroGenServerOutput", "EnviroGenServerInput", 20);
        }

        private static void StartDummyMCServer()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo("DummyMCServer.exe", "EnviroGenServerOutput EnviroGenServerInput")
            };

            Console.WriteLine("Starting Dummy MC Server");
            process.Start();
        }

        private static void StartMCServer(string path)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo("java.exe", $"-jar {path}")
            };

            Console.WriteLine($"Starting MC Server at {path}");
            process.Start();
        }

        private static void StartPipeServer(string outputPipeName, string inputPipeName, int numThreads)
        {
            var server = new EnviroGenPipeServer(outputPipeName, inputPipeName, numThreads)
            {
                Commander = new EnviroGenServerCommander()
            };

            Console.WriteLine("Starting the EnviroGen Pipe Server");
            server.Run();
        }
    }
}
