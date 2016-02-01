using System;
using System.Diagnostics;

namespace MinecraftEnviroGenServer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("ERROR: Expected to find the path to the server executable as a command line argument.");
                Console.Write("Press ENTER to exit...");
                Console.ReadLine();
                return;
            }

            StartMCServer(args[0]);

            StartPipeServer("EnviroGenServerOutput", "EnviroGenServerInput", 20);
        }

        private static void StartMCServer(string path)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo("java.exe", $"-jar {path}")
            };

            process.Start();
        }

        private static void StartPipeServer(string outputPipeName, string inputPipeName, int numThreads)
        {
            var server = new EnviroGenPipeServer(outputPipeName, inputPipeName, numThreads)
            {
                Commander = new EnviroGenServerCommander()
            };
            server.Run();
        }
    }
}
