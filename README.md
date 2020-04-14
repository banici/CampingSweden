# CampingSweden
Web API with CRUD operations

## Short intro
This is a ASP.NET Core 3.1 Web API connected to local database made with code first method and migrations. The User interface is a
MVC Project with Razor code as MVC standards.

This project is a website that shows information and pictures about camping parks around the country for regular users. Admin users with login accounts can use the CRUD operations such as: Create / Read / Update / Delete the information on the camping parks in the API.

## Installed Packages
### Swashbuckle:
Swagger UI tool makes it easy to test your API functionality as it includes built-in test harness for the public methods. And with SwaggerDocument gives a nice structure in the SwaggerUI from the API information about the objects from the routes, controllers and methods.

### Automapper:
Since this project is using DTO's (Data transfer Objects) to prevent the API from exposing the database entities to the client. Automapper makes it easy and with less code to write when converting domain model objects to dto's.

### JwtBearer:
JwtBearer is a standard web authentication token attached to HTTP request that validates the calls to authenticate them. 
