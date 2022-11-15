using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace ConcurrenceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .ConfigureAppConfiguration((_, configuration) =>
                {
                    string linuxPath = @"/root/.microsoft/usersecrets/secrets.json";
                    configuration.AddKeyPerFile(directoryPath: Path.GetFullPath(linuxPath), optional: true);
                    configuration.AddJsonFile( path: linuxPath, optional: true);
                    configuration.AddEnvironmentVariables();
                })
                .Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
