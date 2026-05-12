# Changelog

All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.2.0] - 2026-05-09

### Added

- `Bun3.Core.Attributes.ReadOnlyAttribute` and matching `ReadOnlyDrawer` — disables a serialized field's inspector editing.
- `UnifiedToggleGroup` — preset-based unified toggle. Editor-time and runtime produce identical results via `[ExecuteAlways]` and a shared invocation path.
- Built-in toggle implementations: `UnifiedToggleCanvasGroup`, `UnifiedToggleGameObject`, `UnifiedToggleImage`, `UnifiedToggleLayoutElement`, `UnifiedToggleToggleGroup` (cascading to another group).
- Extensible options via `UnifiedOption<TComponent, TOption>` with `[SubclassSelector]` inspector UX.
- `Unified Toggle Group` sample.

### Changed

- Package depends on `com.mackysoft.serializereference-extensions` (git URL).
- Migrated from the prototype `com.bun3.unity-ui.unified-toggle-group` repo with UniTask and ZLinq dependencies dropped: `IUnifiedToggle.SetValueAsync(string): UniTask` → `IUnifiedToggle.SetValue(string): void`. All option implementations are synchronous.

## [0.1.0] - 2026-05-08

### Added

- Initial package scaffold.
