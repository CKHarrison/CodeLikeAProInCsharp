using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace FlyingDutchmanAirlines
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeWebHost(args);
        }
        private static void InitializeWebHost(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().UseUrls("http://0.0.0.0:8080").Build().Run();
    }
}