using Platform.Auth.Login.Func.Contracts;
using Platform.Auth.Login.Func.Models;
using Platform.Auth.Login.Func.Validation;

namespace Platform.Auth.Login.Func.Functions;

public sealed class LoginFunction : IFunction<LoginRequest, LoginResponse>
{
    private readonly ILogger<LoginFunction> _logger;

    public LoginFunction(ILogger<LoginFunction> logger)
    {
        _logger = logger;
    }

    public Task<LoginResponse> ExecuteAsync(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogInformation("Login function invoked");

        ArgumentNullException.ThrowIfNull(request);

        var errors = RequestValidation.Validate(request);
        if (errors.Count > 0)
        {
            throw new FunctionValidationException(errors);
        }

        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(
            new LoginResponse("Login function executed", request.Email.Trim()));
    }
}
