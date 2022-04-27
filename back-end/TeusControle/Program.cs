using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace TeusControle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                // webBuilder.UseKestrel();
                webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                // webBuilder.UseUrls("http://localhost:44338", "http://10.0.0.199:44338");
                webBuilder.UseUrls("http://localhost:44338", "http://172.21.102.251:44338");
                webBuilder.UseIISIntegration();
                webBuilder.UseStartup<Startup>();
            });
    }
}
