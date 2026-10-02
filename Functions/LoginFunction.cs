using Platform.Auth.Login.Func.Abstractions;
using Platform.Auth.Login.Func.Contracts;

namespace Platform.Auth.Login.Func.Functions;

public sealed class LoginFunction : IFunction<LoginRequest, LoginResponse>
{
    public Task<LoginResponse> ExecuteAsync(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _ = request;

        return Task.FromResult(new LoginResponse("Login function executed"));
    }
}
