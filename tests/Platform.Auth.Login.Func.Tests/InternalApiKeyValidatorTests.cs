using Platform.Auth.Login.Func.Configuration;
using Platform.Auth.Login.Func.Security;

namespace Platform.Auth.Login.Func.Tests;

public sealed class InternalApiKeyValidatorTests
{
    [Fact]
    public void IsValid_returns_true_for_matching_key()
    {
        Assert.True(InternalApiKeyValidator.IsValid("same-key", "same-key"));
    }

    [Fact]
    public void IsValid_returns_false_for_missing_key()
    {
        Assert.False(InternalApiKeyValidator.IsValid("expected-key", null));
    }

    [Fact]
    public void IsValid_returns_false_for_incorrect_key()
    {
        Assert.False(InternalApiKeyValidator.IsValid("expected-key", "wrong-key"));
    }
}
