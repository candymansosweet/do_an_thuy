using Application.Staffs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "Infrastructure"))
            //    .AddJsonFile("appsettings.json")
            //    .Build(); ;
            services.AddScoped<IStaffRespository, StaffRespository>();
            var assembly = typeof(ConfigureServices).Assembly;
            //services.AddScoped<IJwtUtils, JwtUtils>();

            services.AddAutoMapper(assembly);
            //services.AddMediatR(configuration =>
            //{
            //    configuration.RegisterServicesFromAssembly(assembly);
            //});
            return services;
        }
    }
}
