using System;
using Bun3.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Bun3.UI.Samples
{
    /// <summary>
    /// Demonstrates <see cref="ButtonInteractableScope"/>.
    /// Aggregates multiple conditions to determine the button's
    /// <c>interactable</c> state, and forwards disabled reasons
    /// to an <see cref="IButtonDisabledHandler"/>.
    /// </summary>
    public class ButtonInteractableScopeSample : MonoBehaviour
    {
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private int _gold;
        [SerializeField] private int _itemCount;

        private const int Price = 100;
        private const int RequiredItems = 1;

        private void Awake()
        {
            ButtonInteractableScope.DefaultHandler = new ToastDisabledHandler();
        }

        private void Update()
        {
            using var scope = new ButtonInteractableScope(_purchaseButton);
            scope.Require(_gold >= Price, "Not enough gold.");
            scope.Require(_itemCount >= RequiredItems, "Not enough materials.");
            scope.Require(IsShopOpen(), OpenShopHoursPopup);
        }

        private bool IsShopOpen() => true;
        private void OpenShopHoursPopup() { }

        private sealed class ToastDisabledHandler : IButtonDisabledHandler
        {
            private ButtonInteractableScope.DisabledReason _disabledReason;

            public void OnDisabled(ButtonInteractableScope.DisabledReason reason)
            {
                _disabledReason = reason;
            }

            public void Handle()
            {
                if (_disabledReason.DisabledAction != null)
                    _disabledReason.DisabledAction.Invoke();
                else if (_disabledReason.DisabledMessage != null)
                    Debug.Log($"[Disabled] {_disabledReason.DisabledMessage}");
            }
        }
    }
}
