using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FlyingDutchmanAirlines
{
    class Program
    {
        static void Main(string[] args)
        {
            InitalizeHost();
        }

        private static void InitalizeHost() => 
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder =>
            {
                builder.UseStartup<Startup>();
                builder.UseUrls("http://0.0.0.0:8080");
            }).Build().Run();
    }
}
