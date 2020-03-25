using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampingParkAPI.Data;
using CampingParkAPI.Repository;
using CampingParkAPI.Repository.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using CampingParkAPI.MappingProfiles;
using System.Reflection;
using System.IO;
using TrailAPI.Repository.IRepository;
using TrailAPI.Repository;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CampingParkAPI
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
            services.AddDbContext<CampingParkDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ICampingParkRepository, CampingParkRepository>();
            services.AddScoped<ITrailRepository, TrailRepository>();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            // These three services enables dependency injection for swaggerUI for each controller instead of adding each AddSwaggerGen() for every controller.
            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

            //services.AddSwaggerGen(options => {
            //    options.SwaggerDoc("CampingParkOpenAPI",
            //        new Microsoft.OpenApi.Models.OpenApiInfo()
            //        {
            //            Title = "CampingParkOpenAPI",
            //            Version = "1",
            //            Contact = new Microsoft.OpenApi.Models.OpenApiContact()
            //        {
            //            Name = "ilija",
            //            Email = "banic89a@hotmail.com",
            //            Url = new Uri("https://banici.github.io/myPortfolio")
            //        }
            //        });

            //    //options.SwaggerDoc("CampingParkOpenAPITrails",
            //    //    new Microsoft.OpenApi.Models.OpenApiInfo()
            //    //    {
            //    //        Title = "CampingParkOpenAPI (Trails)",
            //    //        Version = "1",
            //    //        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
            //    //    {
            //    //        Name = "ilija",
            //    //        Email = "banic89a@hotmail.com",
            //    //        Url = new Uri("https://banici.github.io/myPortfolio")
            //    //    }
            //    //    });

            //var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //var xmlCommentFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
            //    options.IncludeXmlComments(xmlCommentFullPath);

            //});

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach(var desc in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",
                        desc.GroupName.ToUpperInvariant());
                }
                options.RoutePrefix = "";
            });

            //app.UseSwaggerUI(options => {
            //    options.SwaggerEndpoint("/swagger/CampingParkOpenAPI/swagger.json", "CampingPark API");
            //    //options.SwaggerEndpoint("/swagger/CampingParkOpenAPITrails/swagger.json", "CampingPark API (Trails)");
            //    options.RoutePrefix = ""; // This empty prefix will be default startup when launching API by removing LaunchUrl in launchSettings.json 
            //});

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
