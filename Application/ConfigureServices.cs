using Application.Accounts;
using Application.Authenticates;
using Application.FileStorages;
using Application.PathServices;
using Application.Projects;
using Application.Staffs;
using Application.Tasks;
using Common.Services.JwtTokenService;
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
            services.AddTransient<IStaffRespository, StaffRespository>();
            services.AddTransient<IProjectRespository, ProjectRespository>();
            services.AddTransient<ITaskRespository, TaskRespository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAuthenticateRespository, AuthenticateRespository>();
            services.AddTransient<IFileStorageService, FileStorageService>();
            services.AddTransient<IPathService, PathService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
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
