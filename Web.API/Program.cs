using Web.API.Middlewares;
using Infrastructure;
using Application;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Application.Common.Models;

namespace Web.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            // Đăng ký FileSettings từ appsettings.json
            builder.Services.Configure<FileSettings>(builder.Configuration.GetSection("FileSettings"));


            // Register HttpContextAccessor first
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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

            // Register IUserService

            var app = builder.Build();
            #region config Static Files
            FileSettings fileSettings = builder.Configuration.GetSection("FileSettings").Get<FileSettings>();
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), fileSettings.UploadPath);
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadFolder),
                RequestPath = "/" + fileSettings.UploadPath
            });
            #endregion




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
            app.UseMiddleware<JwtMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}