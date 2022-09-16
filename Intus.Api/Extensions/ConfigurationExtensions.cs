using intus_test_backend.Configurations;

namespace intus_test_backend.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddPathConfigurationInstance(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(new PathConfiguration(
            configuration.GetSection("PathConfiguration")["UnixPath"],
            configuration.GetSection("PathConfiguration")["DosPath"]
        ));
        return services;
    }
}