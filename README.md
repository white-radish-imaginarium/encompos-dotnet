# EncomposApi.Common

Shared .NET contracts for the EncomPos Point-of-Sale REST API — models, query and result types, enums, and a C# client.

Targets `net9.0`. MIT licensed.

## What's in here

| Area | Contents |
| --- | --- |
| *(root)* | Query objects (`*Query.cs`), result envelopes (`*Result.cs`), page and search models, plus the `ApiError` / `ApiResult` response types |
| `Models/` | JSON-friendly models — inventory, customers, suppliers, purchase orders |
| `Enums/` | Domain enums — deposit types, inventory types, account types |
| `Client/` | `EncomposApiClient` and its DI registration extensions |
| `Types/` | `Optional<T>`, which distinguishes "absent" from "null" in partial-update payloads, plus `TypedJsonResult<T>` |
| `Json/` | Serializer settings shared by the API and its consumers |
| `Sync/` | Sync request contracts (`InventorySyncRequest`, `SyncTarget`) |

Query and model types carry FluentValidation validators alongside them (17 files).

## Consuming it

There is no NuGet package yet. Add the repository as a submodule and reference the project:

```bash
git submodule add https://github.com/white-radish-imaginarium/encompos-dotnet.git
```

```xml
<ProjectReference Include="..\encompos-dotnet\EncomposApi.Common\EncomposApi.Common.csproj" />
```

The root namespace is `EncomposApi`, not `EncomposApi.Common`.

### Current requirement: ASP.NET Core

This library declares `<FrameworkReference Include="Microsoft.AspNetCore.App" />`, so it can presently only be referenced from an ASP.NET Core project. Some result types convert to ASP.NET Core MVC types, and the client registration extensions depend on `Microsoft.Extensions.*`.

Work to remove that requirement is in progress. Until it lands, a plain console app, worker service, or class library cannot consume this library.

## Versioning

**This repository is not yet versioned.** There are no tags, and `main` has carried breaking changes to the public surface — the WooCommerce sync implementation and Swagger schema filter were removed, as were the MVC members on `ApiError` and `ApiResult`.

Until a tagged release exists, pin by commit SHA:

```bash
cd encompos-dotnet && git checkout <sha>
```

## Building and testing

```bash
dotnet build EncomposApi.Common.Tests/EncomposApi.Common.Tests.csproj
dotnet test EncomposApi.Common.Tests/EncomposApi.Common.Tests.csproj
```

There is no solution file; the test project references the library, so building it covers both.

## Contributing

This library is developed alongside a private consumer that embeds it as a submodule, so changes usually originate there. When they do, **push this repository before committing the parent's gitlink** — otherwise a fresh recursive clone of the parent will reference a commit that is not on the remote.

## License

MIT — see [LICENSE](LICENSE).
