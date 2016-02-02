using UnityEngine;
using UnityEditor;
using System.Collections;

using Arbor;
namespace ArborEditor
{
	[CustomEditor(typeof(SetVelocityRigidbody))]
	public class SetVelocityRigidbodyInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUILayout.PropertyField(serializedObject.FindProperty("_Target"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("_Angle"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("_Speed"));

			serializedObject.ApplyModifiedProperties();

		}
	}
}
