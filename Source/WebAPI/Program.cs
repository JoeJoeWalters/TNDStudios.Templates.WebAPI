using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using WebAPI.DependencyChecker;
using WebAPI.DependencyChecker.Infrastructure;

[assembly: InternalsVisibleTo("Component.Tests")]

namespace WebAPI
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        internal const string _dependencyCheckId = "DependencyCheck";

        // Called when a HTTP Dependency changes between available and not available
        static void HttpDependencyChange(IDependencyCheckResult result)
        {
            switch (result.Origin.Id)
            {
                case _dependencyCheckId:

                    // Dependent Web API has changed state


                    break;
            }
        }

        static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add in the hosted dependency checker
            builder.Services.AddDependencyChecker(o => 
                {
                    o.Checks.Add(new HttpCheck() { Id = _dependencyCheckId, Path = "https://localhost:7049/health/healthcheck", OnChange = Program.HttpDependencyChange });
                    o.Frequency = 10000;
                });

            // API Versioning
            builder.Services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                o.ReportApiVersions = true;
                o.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("ver"));
            });

            builder.Services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Contacts API",
                        Description = "An example API for contacts",
                        TermsOfService = new Uri("https://example.com/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Example Contact",
                            Url = new Uri("https://example.com/contact")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Example License",
                            Url = new Uri("https://example.com/license")
                        }
                    });

                    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}