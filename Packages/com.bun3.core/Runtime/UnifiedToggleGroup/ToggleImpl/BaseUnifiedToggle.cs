using System.Collections.Generic;
using UnityEngine;

namespace Bun3.Core.UnifiedToggle
{
    [ExecuteAlways]
    public abstract partial class BaseUnifiedToggle : MonoBehaviour, IUnifiedToggle
    {
        [SerializeField] protected UnifiedToggleGroup _authorGroup;

        public const string PropertyNameAuthorGroup = nameof(_authorGroup);

        public abstract void SetValue(string value);

        partial void OnDestroyEditor();

        protected virtual void OnDestroy()
        {
#if UNITY_EDITOR
            OnDestroyEditor();
#endif
            if (_authorGroup)
                _authorGroup.EnsureValidToggles();
        }

        public abstract void SetOptionValues(string[] values);

        protected virtual void OnEnable()
        {
            if (_authorGroup)
                _authorGroup.Register(this);
        }

        protected virtual void OnDisable()
        {
            if (_authorGroup)
                _authorGroup.Unregister(this);
        }
    }

    [DisallowMultipleComponent]
    public abstract partial class BaseUnifiedToggle<TComponent> : BaseUnifiedToggle where TComponent : Component
    {
        public abstract TComponent Component { get; }

        [SerializeReference, SubclassSelector] protected List<IUnifiedOption<TComponent>> _options = new();
        
        public const string PropertyNameOptions = nameof(_options);
        
        public IReadOnlyList<IUnifiedOption<TComponent>> Options => _options;

        // 자식 토글이 보유한 직렬화된 컴포넌트 참조를 채우는 hook.
        // GetComponent 호출은 자식이 자신이 보유한 필드명을 알고 있으므로 자식에서 수행.
        // component => this 형태(예: UnifiedToggleGameObject)라면 override 불필요.
        protected virtual void EnsureComponent() { }

        protected virtual void Reset()
        {
            EnsureComponent();
        }

        protected virtual void OnValidate()
        {
            EnsureComponent();
        }

        public sealed override void SetOptionValues(string[] values)
        {
            for (var i = 0; i < _options.Count; i++)
            {
                var option = _options[i];
                option?.SetOptionValues(values);
            }
        }

        public sealed override void SetValue(string value)
        {
            if (_options.Count == 0)
                return;

            foreach (var option in _options)
            {
                if (option == null) continue;
                option.SetValue(Component, value);
            }
        }
    }
}
