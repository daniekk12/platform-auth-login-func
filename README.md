# platform-auth-login-func

Independent **Login Function** for the Platform Auth learning project. Business logic lives in `LoginFunction`, which implements `IFunction<LoginRequest, LoginResponse>`. HTTP (`POST /login`) is a **local invocation adapter** only.

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
dotnet run --launch-profile http
```

Listens on **http://localhost:5002** (see `Properties/launchSettings.json`).

## Endpoints

| Method | Path | Description |
|--------|------|-------------|
| POST | `/login` | Invoke login function |
| GET | `/health` | Process liveness |

### Request (`POST /login`)

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

Example:

```bash
curl -X POST http://localhost:5002/login \
  -H "Content-Type: application/json" \
  -d "{\"email\":\"test@example.com\",\"password\":\"Password123!\"}"
```

## Tests

```bash
dotnet test tests/Platform.Auth.Login.Func.Tests/Platform.Auth.Login.Func.Tests.csproj
```

Tests target `LoginFunction` behavior (validation, cancellation), not only HTTP.

Standalone repository: no `.sln`, no project references to other Platform Auth components.
