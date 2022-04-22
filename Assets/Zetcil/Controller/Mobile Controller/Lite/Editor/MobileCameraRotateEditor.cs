using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(MobileCameraRotate)), CanEditMultipleObjects]
    public class MobileCameraRotateEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           MainCamera,
           usingMouseSimulation,
           RotateSpeed,
           BeginTouch
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            MainCamera = serializedObject.FindProperty("MainCamera");
            usingMouseSimulation = serializedObject.FindProperty("usingMouseSimulation");
            RotateSpeed = serializedObject.FindProperty("RotateSpeed");
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
                    EditorGUILayout.PropertyField(RotateSpeed, true);
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