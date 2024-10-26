using FluentValidation;
using TechStackStudies.DTOs;
using TechStackStudies.Infrastructure.Validators;

namespace TechStackStudies.Configuration;

public static class ValidatorsCollection
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<TechnologyRequest>, TechnologyValidator>();
        return services;
    }
}
