using Platform.Auth.Login.Func.Contracts;
using Platform.Auth.Login.Func.Models;
using Platform.Auth.Login.Func.Validation;

namespace Platform.Auth.Login.Func.Http;

public static class LoginHttpAdapter
{
    public static IEndpointRouteBuilder MapLoginHttpAdapter(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/login", async (
            LoginRequest? request,
            IFunction<LoginRequest, LoginResponse> loginFunction,
            CancellationToken cancellationToken) =>
        {
            if (request is null)
            {
                return Results.BadRequest(new { message = "Request body is required." });
            }

            try
            {
                var response = await loginFunction.ExecuteAsync(request, cancellationToken);
                return Results.Ok(response);
            }
            catch (FunctionValidationException ex)
            {
                return Results.ValidationProblem(ex.Errors);
            }
        });

        return endpoints;
    }
}
