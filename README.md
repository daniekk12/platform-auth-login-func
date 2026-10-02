# platform-auth-login-func

Independent **Login Function** for the Platform Auth learning project. Business logic lives in `LoginFunction`, which implements `IFunction<LoginRequest, LoginResponse>`. HTTP (`POST /login`) is a **local invocation adapter** for the gateway (or a future Function Host)—**not** a public client API.

## Client access

**End users and frontends must not call this service directly.** Use the gateway at `https://localhost:5000/auth/login`. Direct `POST /login` without the internal invocation header returns **403 Forbidden**.

## What it does

- Validates login input (email format, required fields).
- Executes login function logic without database, JWT, sessions, or password verification.
- Returns a non-sensitive success payload demonstrating the function ran.

## Architecture

```text
HTTP Request (POST /login)
        |
        v
  HTTP Adapter (Http/LoginHttpAdapter)
        |
        v
  LoginFunction (Functions/LoginFunction)
        |
        v
  LoginResponse
```

Signup and Login do not reference each other. A future Function Host will invoke `LoginFunction` without HTTP-specific business logic.

## Install

```bash
dotnet restore
dotnet build
```

## Run locally

```bash
dotnet dev-certs https --trust
dotnet run --launch-profile https
```

Listens on **https://localhost:5002** (see `Properties/launchSettings.json`). HTTP is not enabled in the default launch profile.

## Endpoints

| Method | Path | Description |
|--------|------|-------------|
| POST | `/login` | Gateway-internal invoke (requires `X-Platform-Auth-Internal-Key`) |
| GET | `/health` | Process liveness |

Configure `FunctionInvocation:ApiKey` (same shared secret as the gateway). Development default is in `appsettings.Development.json`.

### Request (`POST /login`, gateway only)

```json
{
  "email": "test@example.com",
  "password": "Password123!"
}
```

### Success response (`200 OK`)

```json
{
  "message": "Login function executed",
  "email": "test@example.com"
}
```

### Validation error (`400 Bad Request`)

Validation problem details; passwords and secrets are never returned.

## Tests

```bash
dotnet test tests/Platform.Auth.Login.Func.Tests/Platform.Auth.Login.Func.Tests.csproj
```

Tests target `LoginFunction` behavior (validation, cancellation), not only HTTP.

Standalone repository: no `.sln`, no project references to other Platform Auth components.
