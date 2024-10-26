using TechStackStudies.Infrastructure.Repositories;
using TechStackStudies.Interfaces;

namespace TechStackStudies.Configuration;

public static class RepositoriesCollection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITechnologyRepository, TechnologyRepository>();
        return services;
    }
}
