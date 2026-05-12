using System.Text;
using Bun3.Core.UnifiedToggle;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Bun3.Core.Editor.UnifiedToggle
{
    /// <summary>
    /// Custom inspector applied to all classes deriving from <see cref="BaseUnifiedToggle"/>.
    /// </summary>
    [CustomEditor(typeof(BaseUnifiedToggle), true)]
    public class BaseUnifiedToggleEditor : UnityEditor.Editor
    {
        protected SerializedProperty _script;
        protected SerializedProperty _authorGroup;
        protected SerializedProperty _options;

        protected BaseUnifiedToggle m_Toggle;
        
        private string _prevSignature;

        private void OnEnable()
        {
            _script = serializedObject.FindProperty("m_Script");
            _authorGroup = serializedObject.FindProperty(BaseUnifiedToggle.PropertyNameAuthorGroup);
            _options = serializedObject.FindProperty("_options");

            m_Toggle = target as BaseUnifiedToggle;

            _prevSignature = ComputeOptionsSignature(_options);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.PropertyField(_script);
            }

            var prevGroup = _authorGroup.objectReferenceValue;
            EditorGUILayout.PropertyField(_authorGroup);

            if (prevGroup != _authorGroup.objectReferenceValue)
            {
                if (prevGroup is UnifiedToggleGroup oldGroup)
                {
                    oldGroup.Unregister((BaseUnifiedToggle)target);
                }

                if (_authorGroup.objectReferenceValue is UnifiedToggleGroup newGroup)
                {
                    newGroup.Register((BaseUnifiedToggle)target);
                }
            }
            
            EditorGUI.BeginChangeCheck();
            
            EditorGUILayout.PropertyField(_options, true);

            var newSignature = ComputeOptionsSignature(_options);
            var optionsStructureChanged = _prevSignature != newSignature;

            if (EditorGUI.EndChangeCheck() || optionsStructureChanged)
            {
                _prevSignature = newSignature;
                
                OnOptionChanged();

                serializedObject.ApplyModifiedProperties();

                if (_authorGroup.objectReferenceValue is UnifiedToggleGroup group)
                {
                    Undo.RecordObject(target, "Sync Options");
                    group.UpdateValues();

                    serializedObject.Update();
                }

                if (!Application.isPlaying)
                    EditorSceneManager.MarkSceneDirty(m_Toggle.gameObject.scene);

                Repaint();
            }

            serializedObject.ApplyModifiedProperties();

            DrawPropertiesExcluding(serializedObject, BaseUnifiedToggle.PropertyNameAuthorGroup, "_options", "m_Script");
        }

        protected virtual void OnOptionChanged()
        {
        }

        /// <summary>
        /// 옵션 리스트의 구조적 변화(요소 추가/삭제/타입 변경)를 안정적으로 감지하기 위한 지문.
        /// BeginChangeCheck가 놓치는 SubclassSelector의 managedReferenceValue 할당까지 잡는다.
        /// </summary>
        private static string ComputeOptionsSignature(SerializedProperty optionsProperty)
        {
            if (optionsProperty == null || !optionsProperty.isArray)
                return string.Empty;

            var sb = new StringBuilder();
            sb.Append(optionsProperty.arraySize);
            for (var i = 0; i < optionsProperty.arraySize; i++)
            {
                var elem = optionsProperty.GetArrayElementAtIndex(i);
                sb.Append('|').Append(elem.managedReferenceFullTypename);
            }
            return sb.ToString();
        }
    }
}
