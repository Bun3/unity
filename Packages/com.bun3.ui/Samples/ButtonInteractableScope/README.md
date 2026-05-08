# Button Interactable Scope

Demonstrates how to combine multiple conditions into a single `Button.interactable` state with `ButtonInteractableScope`, and how to route disabled reasons through `IButtonDisabledHandler`.

## How it works

1. Register a global handler once during application bootstrap:
   ```csharp
   ButtonInteractableScope.DefaultHandler = new MyDisabledHandler();
   ```
2. Wherever the button state needs to be re-evaluated, open a scope and call `Require` for each condition:
   ```csharp
   using var scope = new ButtonInteractableScope(button);
   scope.Require(hasItem, "You don't have the required item.");
   scope.Require(hasGold, () => OpenShopPopup());
   ```
3. When the scope is disposed, `button.interactable` is set to the AND of all `Require` results. If any check failed, the handler's `Handle()` is invoked so it can present the disabled state.

## Files

- `ButtonInteractableScopeSample.cs` — example MonoBehaviour and a toast-style `IButtonDisabledHandler` implementation.
- `Bun3.UI.Samples.ButtonInteractableScope.asmdef` — assembly definition for this sample.

## Try it

1. Add the `ButtonInteractableScopeSample` component to a GameObject in a scene.
2. Drag a `UnityEngine.UI.Button` into the `Purchase Button` field.
3. Adjust `Gold` and `Item Count` in the Inspector during Play mode and watch the button toggle along with the disabled-reason logs.
