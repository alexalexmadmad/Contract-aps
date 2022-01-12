using System.Text.Json;
using AutoMapper;
using ElmålingsSystem.API.Infrastructure;
using ElmålingsSystem.API.Services;
using ElmålingsSystem.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ElmålingsSystem.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MålingContext>(options =>
                options.UseInMemoryDatabase("ElMåling"));

            services.AddScoped<IEjerKundeService, EjerKundeService>();
            services.AddScoped<ILejerKundeService, LejerKundeService>();
            services.AddScoped<IInstallationService, InstallationService>();
            services.AddScoped<IMålerService, MålerService>();
            services.AddScoped<IMåleVærdierService, MåleVærdierService>();
            services.AddScoped<JsonSerializerOptions>();
            services.AddAutoMapper(typeof(MappingProfile));

            //configure IApiVersionDescriptionProvider as a service
            services.AddApiVersioningAndExplorer();

            services.AddSwaggerGeneration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //Swagger middleware
                app.UseSwagger();

                #region ApiVersioning middleware
                app.UseSwaggerUIAndAddApiVersionEndPointBuilder(provider);

                //SwaggerUI middleware, with JSON endpoint.
                //app.UseSwaggerUI(c =>
                //{
                //    c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "ContractIt aps API");
                //    c.RoutePrefix = string.Empty;   //set swagger UI root at the app's root
                //});
                #endregion
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class SwaggerGenerationSetupExtensionMethods
    {
        public static void AddSwaggerGeneration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                using (var serviceProvider = services.BuildServiceProvider())
                {
                    var provider = serviceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerDoc(description.GroupName, description.CreateInfoForApiVersion());
                    }
                }
            });
        }

        public static OpenApiInfo CreateInfoForApiVersion(this ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = $"ContractIt aps API",
                Version = description.ApiVersion.ToString(),
                Description = "OpenAPI for ContractIt aps's elmålings system"
            };

            if (description.IsDeprecated)
            {
                info.Description += " (Denne API version er forældet.)";
            }

            return info;
        }

        public static void UseSwaggerUIAndAddApiVersionEndPointBuilder(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwaggerUI(configure =>
            {
                //Build a swagger endpoint for each API version
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    configure.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    configure.RoutePrefix = string.Empty;
                }
            });
        }
    }

    public static class ApiVersionSetupExtensionMethods
    {
        public static void AddApiVersioningAndExplorer(this IServiceCollection services)
        {
            // Add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            // Note: the specified format code will format the version as "'v'major[.minor][-status]"
            services.AddVersionedApiExplorer(options => { options.GroupNameFormat = "'v'VVV"; });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                // Use this if you would like to use the MediaType version header.
                // Eg: Header 'Accept: application/json; v=1.0'
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                // This is set to true so that we can set what version to select (default version)
                // when no version has been selected.
                options.AssumeDefaultVersionWhenUnspecified = true;
                // And this is where we set how to select the default version.
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });
        }
    }
}
