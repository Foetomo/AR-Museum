using UnityEditor;
using UnityEngine;

namespace Zetcil
{
    [CustomEditor(typeof(MobileSwipe)), CanEditMultipleObjects]
    public class MobileSwipeEditor : Editor
    {
        public SerializedProperty
           isEnabled,
           swipeStatus,
           LeftSwipeFlag,
           RightSwipeFlag,
           UpSwipeFlag,
           DownSwipeFlag,
           BeginTouch,
           usingSwipeLeft,
           SwipeLeftEvent,
           usingSwipeRight,
           SwipeRightEvent,
           usingSwipeUp,
           SwipeUpEvent,
           usingSwipeDown,
           SwipeDownEvent
        ;

        void OnEnable()

        {
            isEnabled = serializedObject.FindProperty("isEnabled");
            swipeStatus = serializedObject.FindProperty("swipeStatus");
            LeftSwipeFlag = serializedObject.FindProperty("LeftSwipeFlag");
            RightSwipeFlag = serializedObject.FindProperty("RightSwipeFlag");
            UpSwipeFlag = serializedObject.FindProperty("UpSwipeFlag");
            DownSwipeFlag = serializedObject.FindProperty("DownSwipeFlag");
            BeginTouch = serializedObject.FindProperty("BeginTouch");
            usingSwipeLeft = serializedObject.FindProperty("usingSwipeLeft");
            SwipeLeftEvent = serializedObject.FindProperty("SwipeLeftEvent");
            usingSwipeRight = serializedObject.FindProperty("usingSwipeRight");
            SwipeRightEvent = serializedObject.FindProperty("SwipeRightEvent");
            usingSwipeUp = serializedObject.FindProperty("usingSwipeUp");
            SwipeUpEvent = serializedObject.FindProperty("SwipeUpEvent");
            usingSwipeDown = serializedObject.FindProperty("usingSwipeDown");
            SwipeDownEvent = serializedObject.FindProperty("SwipeDownEvent");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isEnabled);
            if (isEnabled.boolValue)
            {
                EditorGUILayout.PropertyField(swipeStatus, true);
                if (swipeStatus.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                }
                EditorGUILayout.PropertyField(LeftSwipeFlag, true);
                EditorGUILayout.PropertyField(RightSwipeFlag, true);
                EditorGUILayout.PropertyField(UpSwipeFlag, true);
                EditorGUILayout.PropertyField(DownSwipeFlag, true);
                EditorGUILayout.PropertyField(BeginTouch, true);

                EditorGUILayout.PropertyField(usingSwipeLeft);
                if (usingSwipeLeft.boolValue)
                {
                    EditorGUILayout.PropertyField(SwipeLeftEvent);
                }
                EditorGUILayout.PropertyField(usingSwipeRight);
                if (usingSwipeRight.boolValue)
                {
                    EditorGUILayout.PropertyField(SwipeRightEvent);
                }
                EditorGUILayout.PropertyField(usingSwipeUp);
                if (usingSwipeUp.boolValue)
                {
                    EditorGUILayout.PropertyField(SwipeUpEvent);
                }
                EditorGUILayout.PropertyField(usingSwipeDown);
                if (usingSwipeDown.boolValue)
                {
                    EditorGUILayout.PropertyField(SwipeDownEvent);
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