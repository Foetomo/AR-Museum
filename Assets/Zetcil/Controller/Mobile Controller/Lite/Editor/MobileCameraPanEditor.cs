using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(MobileCameraPan)), CanEditMultipleObjects]
    public class MobileCameraPanEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           MainCamera,
           usingMouseSimulation,
           PanSpeed,
           BeginTouch
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            MainCamera = serializedObject.FindProperty("MainCamera");
            usingMouseSimulation = serializedObject.FindProperty("usingMouseSimulation");
            PanSpeed = serializedObject.FindProperty("PanSpeed");
            BeginTouch = serializedObject.FindProperty("BeginTouch");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(MainCamera, true);
                if (MainCamera.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(usingMouseSimulation, true);
                if (usingMouseSimulation.boolValue)
                {
                    EditorGUILayout.PropertyField(PanSpeed, true);
                }
                EditorGUILayout.PropertyField(BeginTouch, true);
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}