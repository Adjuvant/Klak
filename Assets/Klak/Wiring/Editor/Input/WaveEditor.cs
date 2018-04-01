//
// Klak - Utilities for creative coding with Unity
//
// Copyright (C) 2016 Keijiro Takahashi
//
// Klak Waves - Audio and control rate wave functions for Klak
// 
// Copyright (C) 2018, Thomas Deacon
//
// Klak Waves is a port of toxiclibs, Copyright (c) 2006-2011 Karsten Schmidt
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
using Klak.Math.Waves;

namespace Klak.Wiring
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Wave))]
    public class WaveEditor : Editor
    {
        SerializedProperty _wave;
        SerializedProperty _frequency;
        SerializedProperty _amplitude;
        SerializedProperty _offset;
        SerializedProperty _outputEvent;

        SerializedProperty _fmWave;
        SerializedProperty _fmFrequency;
        SerializedProperty _fmAmplitude;
        SerializedProperty _fmOffset;

        void OnEnable()
        {
            _wave = serializedObject.FindProperty("_wave");
            _frequency = serializedObject.FindProperty("_frequency");
            _amplitude = serializedObject.FindProperty("_amplitude");
            _offset = serializedObject.FindProperty("_offset");
            _outputEvent = serializedObject.FindProperty("_outputEvent");

            // FM
            _fmWave = serializedObject.FindProperty("_fmWave");
            _fmFrequency = serializedObject.FindProperty("_fmFrequency");
            _fmAmplitude = serializedObject.FindProperty("_fmAmplitude");
            _fmOffset = serializedObject.FindProperty("_fmOffset");

        }

        public override void OnInspectorGUI()
        {
            var script = target as Wave;

            serializedObject.Update();

            EditorGUILayout.PropertyField(_amplitude);
            EditorGUILayout.PropertyField(_frequency);
            EditorGUILayout.PropertyField(_offset);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_wave);

            EditorGUILayout.Space();

            // FM Wave Details
            var s = script.WaveSettings.waveType.ToString();
            if(s.Contains("FM")){
                EditorGUILayout.LabelField("FM Wave Settings");
                EditorGUILayout.PropertyField(_fmWave);
                EditorGUILayout.PropertyField(_fmAmplitude);
                EditorGUILayout.PropertyField(_fmFrequency);
                EditorGUILayout.PropertyField(_fmOffset);
            }

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_outputEvent);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
