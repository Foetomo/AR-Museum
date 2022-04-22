using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(MobileTap)), CanEditMultipleObjects]
    public class MobileTapEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           usingMouseSimulation,
           ShowDebug,
           BeginTouch,
           MovedTouch,
           EndedTouch,
           usingEventSettings,
           EventSettings
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            usingMouseSimulation = serializedObject.FindProperty("usingMouseSimulation");
            ShowDebug = serializedObject.FindProperty("ShowDebug");
            BeginTouch = serializedObject.FindProperty("BeginTouch");
            MovedTouch = serializedObject.FindProperty("MovedTouch");
            EndedTouch = serializedObject.FindProperty("EndedTouch");
            usingEventSettings = serializedObject.FindProperty("usingEventSettings");
            EventSettings = serializedObject.FindProperty("EventSettings");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(usingMouseSimulation, true);
                EditorGUILayout.PropertyField(ShowDebug, true);
                EditorGUILayout.PropertyField(BeginTouch, true);
                EditorGUILayout.PropertyField(MovedTouch, true);
                EditorGUILayout.PropertyField(EndedTouch, true);

                EditorGUILayout.PropertyField(usingEventSettings);
                if (usingEventSettings.boolValue)
                {
                    EditorGUILayout.PropertyField(EventSettings);
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