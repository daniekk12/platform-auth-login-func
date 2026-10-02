using Microsoft.Extensions.Logging.Abstractions;
using Platform.Auth.Login.Func.Functions;
using Platform.Auth.Login.Func.Models;
using Platform.Auth.Login.Func.Validation;

namespace Platform.Auth.Login.Func.Tests;

public sealed class LoginFunctionTests
{
    private readonly LoginFunction _function = new(NullLogger<LoginFunction>.Instance);

    [Fact]
    public async Task ExecuteAsync_valid_request_returns_success_response()
    {
        var request = new LoginRequest
        {
            Email = "test@example.com",
            Password = "Password123!"
        };

        var response = await _function.ExecuteAsync(request, CancellationToken.None);

        Assert.Equal("Login function executed", response.Message);
        Assert.Equal("test@example.com", response.Email);
    }

    [Fact]
    public async Task ExecuteAsync_missing_email_throws_validation_exception()
    {
        var request = new LoginRequest { Email = string.Empty, Password = "Password123!" };

        var ex = await Assert.ThrowsAsync<FunctionValidationException>(
            () => _function.ExecuteAsync(request, CancellationToken.None));

        Assert.Contains(ex.Errors, pair => pair.Key == nameof(LoginRequest.Email));
    }

    [Fact]
    public async Task ExecuteAsync_invalid_email_throws_validation_exception()
    {
        var request = new LoginRequest { Email = "not-an-email", Password = "Password123!" };

        var ex = await Assert.ThrowsAsync<FunctionValidationException>(
            () => _function.ExecuteAsync(request, CancellationToken.None));

        Assert.Contains(ex.Errors, pair => pair.Key == nameof(LoginRequest.Email));
    }

    [Fact]
    public async Task ExecuteAsync_missing_password_throws_validation_exception()
    {
        var request = new LoginRequest { Email = "test@example.com", Password = string.Empty };

        var ex = await Assert.ThrowsAsync<FunctionValidationException>(
            () => _function.ExecuteAsync(request, CancellationToken.None));

        Assert.Contains(ex.Errors, pair => pair.Key == nameof(LoginRequest.Password));
    }

    [Fact]
    public async Task ExecuteAsync_cancelled_token_throws_operation_canceled()
    {
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        var request = new LoginRequest
        {
            Email = "test@example.com",
            Password = "Password123!"
        };

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => _function.ExecuteAsync(request, cts.Token));
    }
}
