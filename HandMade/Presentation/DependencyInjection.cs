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
        //services.Sca
        return services;
    }
}