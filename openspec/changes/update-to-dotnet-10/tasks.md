## 1. Update target framework

- [ ] 1.1 Change `<TargetFramework>` in `id7.csproj` from `net5.0` to `net10.0`

## 2. Build and fix

- [ ] 2.1 Delete stale `bin/` and `obj/` directories for a clean restore
- [ ] 2.2 Run `dotnet restore` and `dotnet build -c Release`; fix any compile errors in `Program.cs` (expect none given the stable API surface used)
- [ ] 2.3 Review new analyzer warnings from the .NET 10 SDK; fix only trivial ones, leave the rest non-fatal

## 3. Verify behavior is unchanged

- [ ] 3.1 Smoke test the built tool: `--help` output matches the pre-upgrade text
- [ ] 3.2 Smoke test add/delete/list: adding tasks, deleting by index 0-6, and the max-7 guard behavior are unchanged
- [ ] 3.3 Confirm the `id7` data file is read/written in the same format as before

## 4. Pack and verify

- [ ] 4.1 Run `dotnet pack -c Release` and verify a fresh `.nupkg` appears in `nupkg/`
- [ ] 4.2 Install the packed tool locally (or run it directly) and confirm it executes on the .NET 10 runtime