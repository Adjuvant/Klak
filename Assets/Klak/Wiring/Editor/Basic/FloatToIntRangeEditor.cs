using UnityEditor;
using UnityEngine;

namespace Klak.Wiring
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(FloatToIntRange))]
    public class FloatToIntEditor : Editor
    {
        SerializedProperty _intOutput0;
        SerializedProperty _intOutput1;
        SerializedProperty _intEvent;

        static GUIContent _textOutput0 = new GUIContent("Value at 0");
        static GUIContent _textOutput1 = new GUIContent("Value at 1");

        void OnEnable()
        {
            _intOutput0 = serializedObject.FindProperty("_intOutput0");
            _intOutput1 = serializedObject.FindProperty("_intOutput1");
            _intEvent = serializedObject.FindProperty("_intEvent");
        }

        void OnDisable()
        {
            _intEvent = null;
            _intOutput0 = null;
            _intOutput1 = null;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_intOutput0, _textOutput0);
            EditorGUILayout.PropertyField(_intOutput1, _textOutput1);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_intEvent);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
