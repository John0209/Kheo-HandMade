using System.Reflection;
using System.Text.Json.Serialization;
using ClassLibrary1.Interface;
using ClassLibrary1.Interface.IRepositories;
using ClassLibrary1.Repositories;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace HandMade;

public static class DependencyInjection
{
    public static IServiceCollection AddDependency(this IServiceCollection services, string dbConnection = "")
    {
        //Db context
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(dbConnection));

        //Add repo
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(IBaseRepository<>), typeof(BaseRepository<>))
            .AddClasses(classes => classes.AssignableTo(typeof(BaseRepository<>)), publicOnly: true)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        //Add service
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(IBaseRepository<>), typeof(BaseRepository<>))
            .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Service")), publicOnly: true)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddControllers()
            //allow enum string value in swagger and front-end instead of int value
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services.AddSwaggerGen(ops =>
        {
            ops.MapType<DateTime>(() => new OpenApiSchema()
            {
                Type = "string",
                Format = "date",
                Example = new OpenApiString(DateTime.Now.ToString("yyyy-MM-dd"))
            });
            
            ops.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "HandMade", Version = "v1", Description = "ASP NET core API for HandMade project."
                });
            
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            ops.IncludeXmlComments(xmlPath);
            
        });
        return services;
    }
}