using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(MobileVibrate)), CanEditMultipleObjects]
    public class MobileVibrateEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           VibrateTrigger,
           usingVibrateEvent,
           VibrateEvent
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            VibrateTrigger = serializedObject.FindProperty("VibrateTrigger");
            usingVibrateEvent = serializedObject.FindProperty("usingVibrateEvent");
            VibrateEvent = serializedObject.FindProperty("VibrateEvent");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(VibrateTrigger, true);
                EditorGUILayout.PropertyField(usingVibrateEvent, true);
                if (usingVibrateEvent.boolValue)
                {
                    EditorGUILayout.PropertyField(VibrateEvent, true);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}