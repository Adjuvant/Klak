using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Klak.Wiring
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(StringData))]
    public class StringDataEditor : Editor
    {
        SerializedProperty _stringData;
        SerializedProperty _stringEvent;

        void OnEnable(){
            _stringData = serializedObject.FindProperty("_stringData");
            _stringEvent = serializedObject.FindProperty("_stringEvent");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_stringData);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_stringEvent);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
