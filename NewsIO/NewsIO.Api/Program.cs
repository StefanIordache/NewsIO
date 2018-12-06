using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace NewsIO.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                    .UseUrls("http://localhost:5030")
                    .Build()
                    .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
