# platform-auth-login-func

Independent **Login Function** for the Platform Auth learning project. Business logic lives in `LoginFunction` (`IFunction<LoginRequest, LoginResponse>`). HTTP is a **local invocation adapter**, not the definition of the function.

## Architecture role

```text
Gateway API  --->  (future Function Host)  --->  Login Function
```

Signup and Login do not reference each other.

## Function contract

**Input**

```json
{
  "email": "test@example.com",
  "password": "Password123!"
}
```

**Output**

```json
{
  "message": "Login function executed"
}
```

C# types: `LoginRequest`, `LoginResponse` in `Contracts/`.

## Run locally

```bash
dotnet run --launch-profile http
```

**Local HTTP adapter:** `POST http://localhost:5002/`

```bash
curl -X POST http://localhost:5002/ -H "Content-Type: application/json" -d "{\"email\":\"test@example.com\",\"password\":\"Password123!\"}"
```

## Build

```bash
dotnet build
```

Standalone repository: no `.sln`, no references to other Platform Auth projects.
