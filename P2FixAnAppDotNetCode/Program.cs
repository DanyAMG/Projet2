using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using P2FixAnAppDotNetCode.Models.Repositories;

namespace P2FixAnAppDotNetCode
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var productRepository = ProductRepository.Instance;

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
