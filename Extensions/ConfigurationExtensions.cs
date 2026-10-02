using Platform.Auth.Login.Func.Configuration;

namespace Platform.Auth.Login.Func.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddLoginConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<FunctionInvocationOptions>()
            .Bind(configuration.GetSection(FunctionInvocationOptions.SectionName))
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.ApiKey),
                $"{FunctionInvocationOptions.SectionName}.{nameof(FunctionInvocationOptions.ApiKey)} is required.")
            .ValidateOnStart();

        return services;
    }
}
