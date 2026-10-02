using Platform.Auth.Login.Func.Contracts;
using Platform.Auth.Login.Func.Extensions;
using Platform.Auth.Login.Func.Functions;
using Platform.Auth.Login.Func.Http;
using Platform.Auth.Login.Func.Middleware;
using Platform.Auth.Login.Func.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLoginConfiguration(builder.Configuration);
builder.Services.AddSingleton<LoginFunction>();
builder.Services.AddSingleton<IFunction<LoginRequest, LoginResponse>>(sp =>
    sp.GetRequiredService<LoginFunction>());
builder.Services.AddHealthChecks();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler();
    app.UseHsts();
}

app.UseMiddleware<SecurityHeadersMiddleware>();
app.UseMiddleware<FunctionOperationCacheMiddleware>();
app.UseMiddleware<InternalInvocationMiddleware>();

app.MapLoginHttpAdapter();
app.MapHealthChecks("/health");

app.Run();

public partial class Program;
