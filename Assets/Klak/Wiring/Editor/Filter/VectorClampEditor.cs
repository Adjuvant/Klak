using UnityEngine;
using UnityEditor;

namespace Klak.Wiring
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(VectorClamp))]
    public class VectorClampEditor : Editor
    {
        SerializedProperty _low;
        SerializedProperty _high;
        SerializedProperty _outputEvent;

        void OnEnable()
        {
            _low = serializedObject.FindProperty("low");
            _high = serializedObject.FindProperty("high");
            _outputEvent = serializedObject.FindProperty("_outputEvent");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_low);
            EditorGUILayout.PropertyField(_high);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_outputEvent);

            serializedObject.ApplyModifiedProperties();
        }
    }
}