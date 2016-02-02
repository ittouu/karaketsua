﻿using UnityEngine;
using UnityEditor;
using System.Collections;

using Arbor;

namespace ArborEditor
{
	[CustomEditor(typeof(TweenTextureOffset))]
	public class TweenTextureOffsetInspector : TweenBaseInspector
	{
		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			
			DrawBase();
			
			EditorGUILayout.Space();
			
			EditorGUILayout.PropertyField( serializedObject.FindProperty( "_Target" ) );
			EditorGUILayout.PropertyField( serializedObject.FindProperty("_PropertyName") );
            EditorGUILayout.PropertyField( serializedObject.FindProperty("_Relative") );
			EditorGUILayout.PropertyField( serializedObject.FindProperty( "_From" ) );
			EditorGUILayout.PropertyField( serializedObject.FindProperty( "_To" ) );

			serializedObject.ApplyModifiedProperties();
		}
	}
}