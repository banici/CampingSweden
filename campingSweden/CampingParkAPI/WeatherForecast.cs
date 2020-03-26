using System;

namespace CampingParkAPI
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}


// Automapper for easier converting from model to DTO.

// SwashbuckleAspCore containing Swagger / SwaggerUI for an alternative API call than Postman?

// Due to enabling xml comments, warnings are shown through out the whole project pointing to where comments are missing.
// Project => Properties > Build => Surpress warnings and type the error code to get rid of warnings.

// MVC project installed Microsoft.AspNetCore.Mvc.Razor.runtimecompilation. This package makes it possible to make changes while project is running
// and see the new changes while refreshing the website.

