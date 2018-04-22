//
// Klak - Utilities for creative coding with Unity
//
// Copyright (C) 2016 Keijiro Takahashi
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
using UnityEngine;
using UnityEditor;

namespace Klak.Wiring
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(TransformInput))]
    public class TransformInputEditor : Editor
    {
        SerializedProperty _local;
        SerializedProperty _diff;
        SerializedProperty _target;

        SerializedProperty _posEvent;
        SerializedProperty _rotEvent;
        SerializedProperty _scaleEvent;

        void OnEnable()
        {
            _local = serializedObject.FindProperty("local");
            _diff = serializedObject.FindProperty("difference");
            _target = serializedObject.FindProperty("target");

            _posEvent = serializedObject.FindProperty("_positionEvent");
            _rotEvent = serializedObject.FindProperty("_rotationEvent");
            _scaleEvent = serializedObject.FindProperty("_scaleEvent");
        }

        public override void OnInspectorGUI()
        {
            var myScript = target as TransformInput;

            serializedObject.Update();

            EditorGUILayout.PropertyField(_target, new GUIContent("Input Object"));

            EditorGUILayout.PropertyField(_local, new GUIContent("Local space"));
            EditorGUILayout.PropertyField(_diff, new GUIContent("Send difference"));

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_posEvent);
            EditorGUILayout.PropertyField(_rotEvent);
            EditorGUILayout.PropertyField(_scaleEvent);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
