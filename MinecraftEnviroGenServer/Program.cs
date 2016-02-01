using System.Diagnostics;

namespace MinecraftEnviroGenServer
{
    class Program
    {
        static void Main(string[] args)
        {
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
