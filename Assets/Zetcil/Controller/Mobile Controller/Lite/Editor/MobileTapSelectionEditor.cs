using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(MobileTapSelection)), CanEditMultipleObjects]
    public class MobileTapSelectionEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            MainCamera,
            SelectedObject,
            ObjectSelectionType,
            ObjectTag,
            ObjectName,
            usingMouseSimulation,
            ShowDebug,
            BeginTouch
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");

            MainCamera = serializedObject.FindProperty("MainCamera");
            SelectedObject = serializedObject.FindProperty("SelectedObject");
            ObjectSelectionType = serializedObject.FindProperty("ObjectSelectionType");
            ObjectTag = serializedObject.FindProperty("ObjectTag");
            ObjectName = serializedObject.FindProperty("ObjectName");
            usingMouseSimulation = serializedObject.FindProperty("usingMouseSimulation");
            ShowDebug = serializedObject.FindProperty("ShowDebug");
            BeginTouch = serializedObject.FindProperty("BeginTouch");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(isEnabled, true);

            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(MainCamera, true);
                if (MainCamera.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }

                EditorGUILayout.PropertyField(SelectedObject, true);
                if (SelectedObject.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(ObjectSelectionType, true);

                MobileTapRotate.CObjectSelectionType st = (MobileTapRotate.CObjectSelectionType)ObjectSelectionType.enumValueIndex;

                switch (st)
                {
                    case MobileTapRotate.CObjectSelectionType.Everything:
                        {

                        }
                        break;
                    case MobileTapRotate.CObjectSelectionType.ByName:
                        {
                            EditorGUILayout.PropertyField(ObjectName, true);
                        }
                        break;
                    case MobileTapRotate.CObjectSelectionType.ByTag:
                        {
                            EditorGUILayout.PropertyField(ObjectTag, true);
                        }
                        break;
                }
                EditorGUILayout.PropertyField(usingMouseSimulation, true);
                EditorGUILayout.PropertyField(ShowDebug, true);
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
