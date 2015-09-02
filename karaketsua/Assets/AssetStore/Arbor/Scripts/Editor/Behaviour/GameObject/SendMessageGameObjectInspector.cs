﻿using UnityEngine;
using UnityEditor;
using System.Collections;

using Arbor;

namespace ArborEditor
{
	[CustomEditor(typeof(SendMessageGameObject))]
	public class SendMessageGameObjectInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			serializedObject.Update ();

			EditorGUILayout.PropertyField( serializedObject.FindProperty( "_Target" ) );
			EditorGUILayout.PropertyField( serializedObject.FindProperty( "_MethodName" ) );

			SerializedProperty typeProperty = serializedObject.FindProperty("_Type");
			EditorGUILayout.PropertyField(typeProperty);

			SendMessageGameObject.Type type = (SendMessageGameObject.Type)typeProperty.enumValueIndex;

			switch (type)
			{
				case SendMessageGameObject.Type.Int:
					EditorGUILayout.PropertyField(serializedObject.FindProperty("_IntValue"));
					break;
				case SendMessageGameObject.Type.Float:
					EditorGUILayout.PropertyField(serializedObject.FindProperty("_FloatValue"));
					break;
				case SendMessageGameObject.Type.Bool:
					EditorGUILayout.PropertyField(serializedObject.FindProperty("_BoolValue"));
					break;
				case SendMessageGameObject.Type.String:
					EditorGUILayout.PropertyField(serializedObject.FindProperty("_StringValue"));
					break;
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}