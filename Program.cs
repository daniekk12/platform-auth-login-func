using Platform.Auth.Login.Func.Contracts;
using Platform.Auth.Login.Func.Functions;
using Platform.Auth.Login.Func.Http;
using Platform.Auth.Login.Func.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<LoginFunction>();
builder.Services.AddSingleton<IFunction<LoginRequest, LoginResponse>>(sp =>
    sp.GetRequiredService<LoginFunction>());
builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapLoginHttpAdapter();
app.MapHealthChecks("/health");

app.Run();

public partial class Program;
