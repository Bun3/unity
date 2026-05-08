# Changelog

All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.1.0] - 2026-05-08

### Added

- Initial release.
- `ButtonInteractableScope` (`ref struct`) — combines multiple `Require` checks into a single `Button.interactable` state, applied on dispose.
- `IButtonDisabledHandler` — receives `DisabledReason` events while the scope is open, and a final `Handle()` callback when the scope completes.
- Default no-op handler so consumers do not have to register one before use.
- `Button Interactable Scope` sample demonstrating typical usage with a toast-style handler.
