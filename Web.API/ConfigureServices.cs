using Microsoft.OpenApi.Models;

namespace Web.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebAPI(this IServiceCollection services, ILoggingBuilder loggingBuilder)
        {
            var assembly = typeof(ConfigureServices);

            loggingBuilder.ClearProviders();
            //loggingBuilder.AddLog4Net();
            loggingBuilder.AddConsole().AddSimpleConsole(
                options =>
                {
                    options.IncludeScopes = true;
                    options.SingleLine = true;
                    options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
                }
            );

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
                config.CustomSchemaIds(type => type.ToString());
            });
            return services;
        }
    }
}
