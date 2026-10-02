using Microsoft.AspNetCore.Mvc.Testing;
using Platform.Auth.Login.Func.Configuration;

namespace Platform.Auth.Login.Func.Tests;

public sealed class ConfigurationValidationTests
{
    [Fact]
    public void Host_fails_to_start_when_api_key_is_missing()
    {
        var exception = Assert.ThrowsAny<Exception>(() =>
            new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                    builder.UseSetting($"{FunctionInvocationOptions.SectionName}:ApiKey", string.Empty))
                .CreateClient());

        Assert.Contains("ApiKey", exception.Message, StringComparison.OrdinalIgnoreCase);
    }
}
