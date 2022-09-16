using Intus.Business.Interfaces.Repositories;
using Intus.Business.Interfaces.Services;
using Intus.Business.Repositories;
using Intus.Business.Services;

namespace Intus.Api.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IFileRepository, FileRepository>();
        services.AddScoped<IDimensionsService, DimensionsService>();
        return services;
    }
}