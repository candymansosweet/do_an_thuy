

using Web.API.Middlewares;
using Infrastructure;
using Application;
namespace Web.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
            // Cấu hình CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services
                .AddInfrastructure()
                .AddApplication()
                .AddWebAPI(builder.Logging);
            var app = builder.Build();
            // Thêm dòng này để bật CORS
            app.UseCors("AllowAll");



            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}