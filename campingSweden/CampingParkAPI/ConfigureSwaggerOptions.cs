using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampingParkAPI
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this._provider = provider; // this line works just like block inside curlybraces
        //{
        //    _provider = provider
        //}
        public void Configure(SwaggerGenOptions options)
        {
            foreach(var desc in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    desc.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = $"Camping Park {desc.ApiVersion}",
                        Version = desc.ApiVersion.ToString()
                    });
            }
        }

    }
}
