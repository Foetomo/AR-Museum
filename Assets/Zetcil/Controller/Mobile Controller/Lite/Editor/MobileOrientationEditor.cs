using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(MobileOrientation)), CanEditMultipleObjects]
    public class MobileOrientationEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           InvokeType,
           Orientation
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            InvokeType = serializedObject.FindProperty("InvokeType");
            Orientation = serializedObject.FindProperty("Orientation");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(InvokeType, true);
                EditorGUILayout.PropertyField(Orientation, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}