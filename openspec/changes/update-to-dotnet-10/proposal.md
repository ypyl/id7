## Why

The `id7` CLI tool targets `net5.0`, which reached end-of-support in May 2022 and is long past end-of-life. No compatible .NET 5 runtime is guaranteed on modern systems, and the project cannot build with the installed .NET 10 SDK without a target upgrade. Updating to `net10.0` (current LTS, released November 2025) restores a supported, buildable, securable runtime.

## What Changes

- Update `TargetFramework` in `id7.csproj` from `net5.0` to `net10.0`.
- Verify the existing source (`Program.cs`) compiles cleanly under the .NET 10 SDK (C# 14 default), fixing any breaking API or analyzer issues that surface.
- Confirm the dotnet tool packaging (`PackAsTool`, `nupkg` output) still works and produces a packable `.nupkg`.
- Clean stale build artifacts (`bin/`, `obj/`) to force a fresh restore and rebuild.
- No change to CLI behavior, commands, output format, or tool version.

## Capabilities

### New Capabilities

None. This change introduces no new user-facing behavior.

### Modified Capabilities

None. The tool's external behavior is unchanged; this is a framework/tooling upgrade. The change sets `skip_specs: true` in `.openspec.yaml` because no spec-level behavior changes.

## Impact

- `id7.csproj` — target framework and any related metadata.
- `Program.cs` — only if compiler/analyzer errors require source adjustments.
- Build/pack pipeline — `dotnet build` and `dotnet pack` against the .NET 10 SDK.
- Requires a .NET 10 SDK (10.0.x installed locally) to build and pack.