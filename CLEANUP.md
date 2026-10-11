# Cleanup Checklist - Before Starting Frontend

Ordered by importance. Last verified against the code on 2026-10-04.

## Done

- [x] **TodoItemsController DI crash** - now injects `ITodoService` / `IAuthService`
- [x] **DB provider switched to SQL Server** - csproj has `SqlServer` + `EntityFrameworkCore.Design`, Program.cs uses `UseSqlServer`
- [x] **EF migration generated** - `Migrations/20260927231615_InitialCreate` (SQL Server). Generated, but not yet applied or committed (see #3)
- [x] **CORS** - `AllowAngular` policy added and `app.UseCors` called in Program.cs
- [x] **Case-duplicate files removed from the index** - old `ToDoItemsController.cs` / `ToDoListApp.csproj` are staged as deleted
- [x] **Naming unified in code** - everything is now `Todo` + `List` (`TodoList`, `TodoListApp`, `TodoDbContext`, `IdTodoList`, ...); namespaces merged into `TodoListApp.*` (`Repository` -> `Repositories`); files renamed to match; EF snapshot verified to still match the models
- [x] **Folder renamed `ToDoList/` -> `TodoList/`** - `TodoList.sln`, the test project reference and `.vscode/launch.json` updated; git index tracks the new spelling (renames staged, not committed). Tip for future case-only renames on Windows: use `git -c core.ignorecase=false ...`

## 1. Clone-and-run blockers (your stated goal: works on any notebook)

- [ ] **2. Config and secrets for a fresh machine**
  - `appsettings.json` has `"*"` placeholders for `ConnectionStrings:DefaultConnection` and `JwtSettings:SecretKey`, and `dotnet user-secrets list` shows nothing configured here, so the app cannot start on a new machine
  - `docker-compose.yml` has the SA password in plaintext and uses `SA_PASSWORD`; the 2022 image expects `MSSQL_SA_PASSWORD`. Move the password into a gitignored `.env` plus a committed `.env.example` (`.gitignore` currently has no `.env` rule)
  - The connection string password must match the compose password
  - The JWT secret must be long enough for HMAC-SHA256 (32+ characters), or token creation fails
  - Decide where each value lives (user-secrets for the API, `.env` for compose) and document it in #4

- [ ] **3. Run it end to end once**
  - `docker compose up -d`, then `dotnet ef database update`, then `dotnet run`, then register + login + create a list through Swagger
  - None of the items above have been exercised yet, only compiled
  - Then commit the pending work: `Migrations/`, `docker-compose.yml`, `.env.example`, `CLEANUP.md`

- [ ] **4. ReadMe.md with the run steps you just verified**
  - Current file only holds 3 business rules, with garbled encoding. Re-save as UTF-8 and add setup steps (docker, secrets, migrate, run)

## 2. API contract fixes (cheap now, expensive after Angular is built against them)

- [ ] **5. `PATCH /me` leaks the password hash**
  - `ClientsController.cs` returns the raw `Client` entity, and `Client.Password` returns the stored hash
  - Fix: return a response DTO (e.g. id, name, email). This is the first step of "stop returning entities directly"; do the same for the other endpoints afterwards

- [ ] **6. Login takes credentials from the query string**
  - `ClientsController.cs:37` `Login(string email, string password)` -> create `LoginDTO` and bind from the body
  - Lower severity than #5 (an SPA does not show API URLs to the user; the exposure is server/proxy logs), but it is a contract change, so do it before the Angular login form exists

## 3. Robustness (no contract change, can be done any time before release)

- [ ] **7. NRE in `AuthService.CanUserDeleteClientTodolist`** (`AuthService.cs:32-35`) - `GetByIdAsync` can return null; return false/404 instead of throwing a 500
- [ ] **8. Orphaned rows on delete** - deleting a `TodoList` or `Client` leaves `ClientTodoList` / `TodoItem` rows behind (no FKs, no cascade). Decide: FKs + cascade (adds a second migration, or regenerate InitialCreate before it is applied/committed) or manual cleanup in the service
- [ ] **9. Dead AutoMapper entry** - delete `CreateMap<PostClientTodoListDTO, PostTodoListDTO>()` (`Mapping.cs:14`)

- [ ] **Test project does not compile** - `TodoServiceTests.cs:239` reads `result.Name` on an unconstrained generic `TEntity` (error CS1061). It was already broken in HEAD before the rename. Fix by asserting on the entity through a per-case selector (e.g. add a `Func<object, string?>` to the `PatchCases` data) instead of `.Name`

## 4. Cosmetic - alongside frontend work

- [ ] Fix mojibake Portuguese strings still present in some files (e.g. `ClientsController.cs`)
- [ ] Remove or gate the verbose JWT claim `Console.WriteLine` logging in Program.cs
- [ ] Replace exceptions-for-flow-control in `ValidateLogin` with a result/null return
- [ ] Review the generic CRUD pattern (`Post<TEntity,TDto>`, `Patch<...>`, `DeleteById<...>`)
