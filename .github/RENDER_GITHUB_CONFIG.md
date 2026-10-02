# GitHub → Render configuration (login function)

Workflow: `.github/workflows/render-deploy.yml` + `.github/scripts/render_sync_deploy.py`.

API: `PUT /v1/services/{serviceId}/env-vars`, then `POST /v1/services/{serviceId}/deploys`.

## One-time Render setup

Create Docker Web Service from **`platform-auth-login-func`**, copy **`srv-…`**. Deploy **second** (after signup, before gateway).

## GitHub Environment: `production`

Create environment **`production`**. Restrict deployment to `main` (and optional reviewers).

Deploy runs only when CI on **`main`** succeeds (`workflow_run`) or **`workflow_dispatch`** is run from **`main`**. Pull request CI does not sync production secrets to Render.

## Secrets

| Name | Purpose |
|------|---------|
| `RENDER_API_KEY` | Render API bearer token |
| `FunctionInvocation__ApiKey` | Same shared secret as gateway and signup |

## Variables

| Name | Purpose |
|------|---------|
| `RENDER_SERVICE_ID` | This login service’s `srv-…` ID |
| `PUBLIC_HEALTH_URL` | `https://<login-host>/health` |
| `ASPNETCORE_ENVIRONMENT` | `Production` |
| `AllowedHosts__0` | `<login-host>.onrender.com` |
