using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>();
            return services;
        }
    }
}
