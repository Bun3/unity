using System;
using System.Linq;
using UnityEngine;

namespace Bun3.Core.UnifiedToggle
{
    [DisallowMultipleComponent]
    public sealed partial class UnifiedToggleGroup : MonoBehaviour, IUnifiedToggle
    {
        [SerializeField] private string[] _presets = { "Off", "On" };
        [SerializeField] private BaseUnifiedToggle[] _toggles = { };

        public string CurrentPreset { get; private set; } = string.Empty;

        public bool isOn => _presets.Length > 0 && CurrentPreset == _presets[^1];

        public void SetOn(bool isOn)
        {
            if (_presets.Length == 0)
                return;
            SetValue(isOn ? _presets[^1] : _presets[0]);
        }

        public void SetValue(string value)
        {
            if (CurrentPreset == value)
                return;

            CurrentPreset = value;

            foreach (var toggle in _toggles)
            {
                if (toggle == null || !toggle.enabled) continue;
                toggle.SetValue(value);
            }
        }

        public string[] GetPresets()
        {
            return _presets;
        }

        public void Register(BaseUnifiedToggle toggle)
        {
            if (toggle == null)
                return;
            if (_toggles.Contains(toggle))
                return;
            Array.Resize(ref _toggles, _toggles.Length + 1);
            _toggles[^1] = toggle;
            UpdateValues();
        }

        public void Unregister(BaseUnifiedToggle toggle)
        {
            if (toggle == null)
                return;
            if (!_toggles.Contains(toggle))
                return;
            _toggles = _toggles.Where(t => t != toggle).ToArray();
            UpdateValues();
        }

        public void EnsureValidToggles()
        {
            _toggles = _toggles.Where(x => x != null).ToArray();
        }

        [ContextMenu(nameof(UpdateValues))]
        public void UpdateValues()
        {
            SetOptionValues(_presets);
        }

        public void SetOptionValues(string[] values)
        {
            foreach (var toggle in _toggles)
                toggle.SetOptionValues(values);
        }
    }
}
