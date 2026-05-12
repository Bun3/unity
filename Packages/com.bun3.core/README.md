# Unity Core

Bun3 shared toolkit for Unity. General-purpose utilities used across Bun3 packages.

## Features

- **`Bun3.Core.Attributes.ReadOnlyAttribute`** — make a serialized field non-editable in the inspector.
- **`UnifiedToggleGroup`** — preset-based unified toggle that produces identical results in editor and runtime, with custom-extensible options and cascading control of nested groups. Built-in implementations cover `CanvasGroup`, `Image`, `LayoutElement`, `GameObject` activation, and another `UnifiedToggleGroup` (for cascading).

## Requirements

- Unity 6000.3 (6.0) or later
- `com.mackysoft.serializereference-extensions` (declared as git dependency)

## Installation

Install via the Unity Package Manager:

- *Window → Package Manager → Add package from git URL...*

Or add to `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.bun3.core": "0.2.0"
  }
}
```

## Quick Start — UnifiedToggleGroup

```csharp
using Bun3.Core.UnifiedToggle;

// In the inspector, configure UnifiedToggleGroup with presets like ["Off", "On"]
// and add UnifiedToggle* children with options per preset.

// Apply a preset from code:
group.SetValue("On");

// Or toggle the last/first preset:
group.SetOn(true);
```

When a preset is applied, every registered toggle runs its options for that preset. The same code path is invoked when the group's preset buttons are clicked in the inspector, so edit-time and runtime yield identical results.

A complete example is included as the `Unified Toggle Group` sample.

## Links

- [Documentation](Documentation/unity.core.md)
- [Changelog](CHANGELOG.md)
- [Third Party Notices](Third%20Party%20Notices.md)
