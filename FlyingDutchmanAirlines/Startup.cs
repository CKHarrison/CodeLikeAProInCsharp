using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace FlyingDutchmanAirlines
{
    class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }
    }
    
}
