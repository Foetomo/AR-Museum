using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(MobileCameraScale)), CanEditMultipleObjects]
    public class MobileCameraScaleEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           MainCamera,
           usingMouseSimulation,
           perspectiveZoomSpeed,
           orthoZoomSpeed,
           floatSpeed,
           BeginTouch
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            MainCamera = serializedObject.FindProperty("MainCamera");
            usingMouseSimulation = serializedObject.FindProperty("usingMouseSimulation");
            perspectiveZoomSpeed = serializedObject.FindProperty("perspectiveZoomSpeed");
            orthoZoomSpeed = serializedObject.FindProperty("orthoZoomSpeed");
            floatSpeed = serializedObject.FindProperty("floatSpeed");
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
                    EditorGUILayout.PropertyField(perspectiveZoomSpeed, true);
                    EditorGUILayout.PropertyField(orthoZoomSpeed, true);
                    EditorGUILayout.PropertyField(floatSpeed, true);
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