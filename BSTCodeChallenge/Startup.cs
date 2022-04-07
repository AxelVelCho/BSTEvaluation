using Microsoft.OpenApi.Models;
using MediatR;
using BSTCodeChallenge.DataAccess;
using System.Reflection;
using BSTCodeChallenge.DataAccess.Data;
using BSTCodeChallenge.Controllers;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using BSTCodeChallenge.ExceptionControllers;

namespace BSTCodeChallenge
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddLogging();
            services.AddMemoryCache();
            services.AddMvc();
            services.AddControllers();
            services.AddControllers(options =>
            {
                options.Filters.Add<HttpResponseExceptionFilter>();
            });
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                    new BadRequestObjectResult(context.ModelState)
                    {
                        ContentTypes =
                        {
                            Application.Json,
                            Application.Xml
                        }
                    };
            })
            .AddXmlSerializerFormatters();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "BST Test API",
                    Description = "An ASP.NET Core Web API for managing products CRUD"
                });
            });
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/error-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseSwagger(options =>
                {
                    options.SerializeAsV2 = true;
                });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            new CacheController();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
