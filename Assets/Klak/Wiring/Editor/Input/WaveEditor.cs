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
