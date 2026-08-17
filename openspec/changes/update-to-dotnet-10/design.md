## Context

See proposal.md - Why. `id7` is a single-project dotnet CLI tool currently targeting `net5.0` (EOL). SDK 10.0.x is installed. The tool is a packaged global tool (`PackAsTool`) with version 1.0.3. There is no source control history in this folder (no `.git`), so changes must be verified carefully before replacing anything.

## Goals / Non-Goals

**Goals:**
- Move the project to a supported, long-term-support runtime (`net10.0`) with minimal source churn.
- Keep the packaged tool's observable behavior byte-for-byte equivalent (output text, file format, commands).
- Preserve the existing publish workflow (`dotnet pack` → `nupkg/`).

**Non-Goals:**
- Modernizing code style (implicit usings, file-scoped namespaces, records, etc.).
- Bumping the package version or re-publishing to NuGet.
- Adding a build matrix (multi-targeting `net8.0`/`net10.0`) - there are no consumers requiring an older TFM.

## Decisions

**1. Target `net10.0` directly, no multi-targeting.**
Rationale: .NET 10 is the current LTS and the only installed SDK family. The tool is personal, with no consumers pinning to an older framework. Multi-targeting adds restore/build overhead with zero benefit. Alternative considered: staying on net5.0 - rejected, it is EOL and unbuildable with the installed SDK.

**2. Keep source changes minimal; fix only what the compiler/analyzers flag.**
Rationale: The used API surface (`Path`, `File`, `Console`, `string`, top-level statements, `async void` helper) is stable across net5 -> net10. Rewriting to new idioms is churn with no behavior value. Alternative: modernize the file - rejected per Non-Goals.

**3. Do not enable `TreatWarningsAsErrors`.**
Rationale: net10 SDK analyzers may surface new console-app warnings (e.g. `CA`/`IDE` rules). A personal tool should not be blocked by warnings that were silent before. Fix real errors; review warnings case-by-case. Trade-off accepted: some warnings may linger.

**4. Keep package version 1.0.3.**
Rationale: `Version` is independent of the target framework. A version bump belongs to a release decision, not a framework upgrade. Alternative: bumping to 1.1.0 - rejected, out of scope unless republishing.

**5. Clean `bin/` and `obj/` before the first build.**
Rationale: net5-era restore artifacts can confuse the new SDK. A clean restore eliminates stale-state build errors. Alternative: incremental build - riskier due to old assets.

## Risks / Trade-offs

- [Compile or analyzer errors surface under the net10 SDK] -> Fix minimally in `Program.cs`; the used APIs are stable, so this is unlikely.
- [Packed tool now requires a .NET 10 runtime on the consuming machine] -> Acceptance criterion: document that the tool targets .NET 10; this is the intent of the upgrade.
- [No git history, so mistakes are not readily revertible] -> Verify by building and smoke-testing before packing; keep a copy of the original files.
- [`async void SaveTasks()` is a pre-existing anti-pattern] -> Out of scope; behavior is unchanged and it compiles fine.
- [New analyzer warnings appear] -> Warnings are non-fatal (Decision 3); only errors block the build.