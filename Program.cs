using Platform.Auth.Login.Func.Abstractions;
using Platform.Auth.Login.Func.Contracts;
using Platform.Auth.Login.Func.Functions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<LoginFunction>();
builder.Services.AddSingleton<IFunction<LoginRequest, LoginResponse>>(sp =>
    sp.GetRequiredService<LoginFunction>());

builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapPost("/login", async (
    LoginRequest request,
    IFunction<LoginRequest, LoginResponse> loginFunction,
    CancellationToken cancellationToken) =>
{
    var response = await loginFunction.ExecuteAsync(request, cancellationToken);
    return Results.Ok(response);
});

app.MapHealthChecks("/health");

app.Run();
