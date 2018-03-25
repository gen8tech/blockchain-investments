using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Blockchain.Investments.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string contentRoot = GetCurrentAppPath();
            
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseConfiguration(config)
                .UseContentRoot(contentRoot)
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

        public static string GetCurrentAppPath() 
        {
            string currentPath = "/";
            string assemblyRoot = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string[] directories = assemblyRoot.Split(Path.DirectorySeparatorChar);

            foreach (string folder in directories) 
            {
                if (folder == "bin")
                    break;
                currentPath = Path.Combine(currentPath, folder);

                if (currentPath.Substring(currentPath.Length - 1, 1) == ":")
                    currentPath += Path.DirectorySeparatorChar;
            }
            
            if (currentPath == "/")
                currentPath = Directory.GetCurrentDirectory();

            return currentPath;
        }
    }
}
