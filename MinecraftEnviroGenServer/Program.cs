﻿using System;
using System.Diagnostics;

namespace MinecraftEnviroGenServer
{
    class Program
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

            StartPipeServer("EnviroGenOutput", 20);
        }

        private static void StartDummyMCServer()
        {
            DummyMCServer.Start("EnviroGenOutput");
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

        private static void StartPipeServer(string pipeName, int numThreads)
        {
            var serverHandler = new EGServerHandler
            {
                EnvironmentUpdater = new MCEnvironmentUpdater()
            };

            var server = new EGPipeServer(pipeName, numThreads)
            {
                Handler = serverHandler
            };

            Console.WriteLine("Starting the EnviroGen Pipe Server");
            server.Run();
        }
    }
}
