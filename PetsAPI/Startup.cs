using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PetsAPI.Common.HttpClients;
using PetsAPI.Common.Interface;
using PetsAPI.Common.Model;
using PetsAPI.Common.Security;
using PetsAPI.Common.Settings;
using PetsAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowedClients",
                    builder => builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        // This reduces the need for OPTIONS/Preflight requests made by a browser. browser will cache response.
                        .SetPreflightMaxAge(TimeSpan.FromSeconds(2520))
                );

            });

            services.AddControllers();
            services.AddMemoryCache();

            //DI for custom settings configuration.
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            //Add Http Client for DI, new in core 2.1
            services.AddHttpClient<IDogHttpClient, DogHttpClient>(client => client.BaseAddress = new Uri(appSettings.dogApiURL));
            services.AddHttpClient<ICatHttpClient, CatHttpClient>(client => client.BaseAddress = new Uri(appSettings.catApiURL));
            services.AddScoped<IDogService, DogService>();
            services.AddScoped<ICatService, CatService>();
            services.AddScoped<IPetService, PetService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Pets API",
                    Description = "An ASP.NET Core Web API that combines the two API (Dogs API and cats API).",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Ralph Pagulayan",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    }
                });
            });


            //Add the Custom Mapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                //api
                cfg.AddProfile<ProfileConfiguration>();
            });

            services.AddSingleton(sp => mapperConfig.CreateMapper());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseMiddleware<ApiKeyMiddleware>();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
