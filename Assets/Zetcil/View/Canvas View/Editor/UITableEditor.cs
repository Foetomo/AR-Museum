using UnityEditor;
using UnityEngine;

namespace Zetcil
{
	[CustomEditor(typeof(UITable)), CanEditMultipleObjects]
	public class UITableEditor : Editor
	{
		public SerializedProperty
		   isEnabled,
		   InvokeType,
		   JSONData,
		   usingDelay,
		   Delay,
		   usingInterval,
		   Interval,
		   TableViewport,
		   TableContent,
		   TableRow,
		   TableRowSize,
		   TotalColumns
		;

		void OnEnable()

		{
			isEnabled = serializedObject.FindProperty("isEnabled");
			InvokeType = serializedObject.FindProperty("InvokeType");
			JSONData = serializedObject.FindProperty("JSONData");
			usingDelay = serializedObject.FindProperty("usingDelay");
			Delay = serializedObject.FindProperty("Delay");
			usingInterval = serializedObject.FindProperty("usingInterval");
			Interval = serializedObject.FindProperty("Interval");
			TableViewport = serializedObject.FindProperty("TableViewport");
			TableContent = serializedObject.FindProperty("TableContent");
			TableRow = serializedObject.FindProperty("TableRow");
			TableRowSize = serializedObject.FindProperty("TableRowSize");
			TotalColumns = serializedObject.FindProperty("TotalColumns");
		}
		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			EditorGUILayout.PropertyField(isEnabled);
			if (isEnabled.boolValue)
			{
				EditorGUILayout.PropertyField(InvokeType, true);
				EditorGUILayout.PropertyField(JSONData, true);
				if (JSONData.objectReferenceValue == null)
				{
					EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
				}
				EditorGUILayout.PropertyField(usingDelay, true);
				if (usingDelay.boolValue)
				{
					EditorGUILayout.PropertyField(Delay, true);
				}

				EditorGUILayout.PropertyField(usingInterval, true);
				if (usingInterval.boolValue)
				{
					EditorGUILayout.PropertyField(Interval, true);
				}

				EditorGUILayout.PropertyField(TableViewport, true);
				EditorGUILayout.PropertyField(TableContent, true);
				EditorGUILayout.PropertyField(TableRow, true);
				EditorGUILayout.PropertyField(TableRowSize, true);
				EditorGUILayout.PropertyField(TotalColumns, true);
			}
			else
			{
				EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
			}
			serializedObject.ApplyModifiedProperties();
		}

	}
}