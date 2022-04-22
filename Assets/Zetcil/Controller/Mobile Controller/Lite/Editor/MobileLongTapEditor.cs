using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(MobileLongTap)), CanEditMultipleObjects]
    public class MobileLongTapEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           LongTapStatus,
           StartTime,
           MaxLongTapTime,
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
            LongTapStatus = serializedObject.FindProperty("LongTapStatus");
            StartTime = serializedObject.FindProperty("StartTime");
            MaxLongTapTime = serializedObject.FindProperty("MaxLongTapTime");
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
                EditorGUILayout.PropertyField(LongTapStatus, true);
                if (LongTapStatus.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(StartTime, true);
                EditorGUILayout.PropertyField(MaxLongTapTime, true);
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