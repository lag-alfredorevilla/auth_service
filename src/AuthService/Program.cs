using Microsoft.AspNetCore.Hosting;

namespace AuthService.Hosting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Hosting.Startup>()
                .Build();

            host.Run();
        }
    }
}