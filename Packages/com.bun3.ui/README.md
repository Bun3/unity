# Unity UI

Dev kits for Unity UI. A small collection of utilities that simplify common patterns in Unity uGUI.

## Features

- **`ButtonInteractableScope`** — aggregate multiple conditions into a single `Button.interactable` state, and route disabled reasons (messages or actions) through `IButtonDisabledHandler`.

## Requirements

- Unity 6000.3 (6.0) or later
- `UnityEngine.UI` (built-in)

## Installation

Install via the Unity Package Manager:

- *Window → Package Manager → Add package from git URL...*

Or add it to `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.bun3.ui": "0.1.0"
  }
}
```

## Quick Start

```csharp
using Bun3.UI.Buttons;

using var scope = new ButtonInteractableScope(myButton);
scope.Require(player.HasItem(itemId), "You don't have the required item.");
scope.Require(player.Gold >= price,    "Not enough gold.");
scope.Require(IsShopOpen(),            () => OpenShopHoursPopup());
```

When the `using` block exits, `myButton.interactable` is updated to the AND of all `Require` results. If any check failed, the registered `IButtonDisabledHandler` is invoked so you can present the disabled reason.

A complete example is included as a Package Manager sample (`Button Interactable Scope`). See `Samples/ButtonInteractableScope/` after import.

## Links

- [Documentation](Documentation/Unity%20UI.md)
- [Changelog](CHANGELOG.md)
- [Third Party Notices](Third%20Party%20Notices.md)
