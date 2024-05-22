using ClassLibrary1.Interface;
using ClassLibrary1.Interface.IRepositories;
using ClassLibrary1.Repositories;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

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
        return services;
    }
}