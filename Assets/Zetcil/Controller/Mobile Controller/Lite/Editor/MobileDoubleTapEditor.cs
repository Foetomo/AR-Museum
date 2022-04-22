using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(MobileDoubleTap)), CanEditMultipleObjects]
    public class MobileDoubleTapEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           DoubleTapStatus,
           MaxDoubleTapTime,
           VariancePosition,
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
            DoubleTapStatus = serializedObject.FindProperty("DoubleTapStatus");
            MaxDoubleTapTime = serializedObject.FindProperty("MaxDoubleTapTime");
            VariancePosition = serializedObject.FindProperty("VariancePosition");
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
                EditorGUILayout.PropertyField(DoubleTapStatus, true);
                if (DoubleTapStatus.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(MaxDoubleTapTime, true);
                EditorGUILayout.PropertyField(VariancePosition, true);
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